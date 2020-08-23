using System;
using System.Collections.Generic;
using Audio;
using Buildings;
using DG.Tweening;
using DigitalRuby.Tween;
using Game;
using Help;
using InputActions;
using Libraries;
using Libraries.Buildings;
using Libraries.Terrains;
using Libraries.Units;
using MapElements;
using MapElements.Items;
using Maps;
using Players;
using reqs;
using Tools;
using Towns;
using UI;
using UnityEngine;

namespace Units
{
    public class UnitInfo : MapElementInfo
    {
        public DataUnit dataUnit;

        public override bool NextRound()
        {
            data.lastInfo = null;
            data.UnitUpdate();
            
            //no town?
            if (data.townId == -1)
                data.ap = data.apMax;

            if (!base.NextRound())
            {
                return false;
            }
            
            //reload if only have enough res
            data.ap = data.apMax;
            
            //has a town?
            return data.townId != -1;

        }

        public void Init(string configType, int player, NVector pos, Dictionary<string, int> cost)
        {
            Debug.Log($"{player}: Create unit {configType} at {pos}");
            baseData = dataUnit = L.b.units[configType];
            var calc = CalculatedData.Calc(dataUnit, cost);
            
            data = new BuildingUnitData();
            data.UnitInit(configType, -1, player, calc);
            data.pos = pos.Clone();
        
            //has a town?
            Town t = S.Towns().NearestTown(S.Player(player), pos, false);
            if (t != null)
            {
                int buildtime = L.b.modifiers[C.BuildRes].CalcModi(calc.buildTime, S.Player(player), pos);
                
                data.townId = t.id;
                gameObject.AddComponent<Construction>().Init(data,cost,this,buildtime);
                S.Player(player).fog.Clear(pos);
            }
            else
            {
                Clear(pos);
            }
        
            FinishInit();
        }

        private void FinishInit()
        {
            name = dataUnit.Name();

            //show it
            SetSprite(dataUnit.Icon);
            transform.position = new Vector2(Pos().x+0.5f,Pos().y);
        }

        public override void Load(BuildingUnitData data)
        {
            baseData = dataUnit = L.b.units[data.type];
            base.Load(data);
        
            FinishInit();
        }

        public override void Upgrade(string type, Dictionary<string, int> cost)
        {
            BaseDataBuildingUnit old = baseData;
            
            GameMgmt.Get().data.units.Remove(data);
            Init(type,data.playerId,Pos(), cost);
            GameMgmt.Get().data.units.Add(data);
            
            CalcUpgradeCost(L.b.units[type], old);
        }

        public void MoveTo(NVector pos, bool moveCamera=true)
        {
            //change level?
            if (data.pos.level != pos.level)
            {
                transform.SetParent(GameMgmt.Get().newMap[pos.level].units.transform);
                
            }

            if (moveCamera)
            {
                MoveBy(pos.x-Pos().x,pos.y-Pos().y);
                return;
            }
            data.pos = pos;
            transform.position = new Vector2(Pos().x+0.5f,Pos().y);
            Clear(pos);
            
        }

        public string Passable(NVector pos)
        {
            DataTerrain land = GameMgmt.Get().newMap.Terrain(pos);
            
            //check terrain
            int cost = GameMgmt.Get().newMap.PathFinding(Pos().level).Cost(Player(),dataUnit.movement,Pos(),pos);
            if (cost == 0)
            {
                return S.T("unitMoveErrorPassable",land.Name());
            }

            //visible?
            if (!Player().fog.Visible(pos))
            {
                return S.T("unitMoveErrorExplored",land.Name());
            }

            //another unit?
            if (!S.Unit().Free(pos))
            {
                return S.T("unitMoveErrorUnit",land.Name(), S.Unit().At(pos).name);
            }

            //can walk
            if (cost > data.ap)
            {
                return S.T("unitMoveErrorAp",land.Name(), cost - data.ap);
            }

            return null;
        }
        
        public void MoveBy(int x, int y)
        {
            //own unit?
            if (!Owner(S.ActPlayerID()))
            {
                OnMapUI.Get().unitUI.ShowPanelMessageError(S.T("unitMoveErrorBelong",name, Player().name));
                return;
            }
            
            //own unit?
            if (IsUnderConstruction())
            {
                OnMapUI.Get().unitUI.ShowPanelMessageError(S.T("unitMoveErrorConstruction",name));
                return;
            }

            int dX = data.pos.x + x;
            int dY = data.pos.y + y;
            NVector dPos = new NVector(dX, dY, Pos().level);

            //can move their?
            string erg = Passable(dPos);
            if (erg != null)
            {
                OnMapUI.Get().unitUI.ShowPanelMessageError(erg);
                return;
            }
            
            //start animation

            var pPath = GameMgmt.Get().newMap.PathFinding(Pos().level).Path(Player(), dataUnit.movement, Pos(), dPos);
            
            //rebuild path
            Vector3[] path = new Vector3[pPath.Count];
            for (int i = 0; i < pPath.Count; i++)
            {
                path[i] = new Vector2(pPath[i].x+0.5f, pPath[i].y);
            }

            GetComponent<UnitAnimator>().Calc((int) path[0].x - data.pos.x, (int) path[0].y - data.pos.y);
            
            // completion defaults to null if not passed in
            gameObject.transform.DOPath(path, pPath.Count, PathType.CatmullRom)
                .OnWaypointChange(value =>
                {
                    if (value >= path.Length) return;
                    Clear(new NVector((int) path[value].x, (int) path[value].y, Pos().level));
                    if (value >= 1)
                    {
                        GetComponent<UnitAnimator>().Calc((int) path[value].x-(int) path[value-1].x, (int) path[value].y-(int) path[value-1].y);
                    }
                    
                })
                .OnComplete(() =>
                {
                    GetComponent<UnitAnimator>().AnStop();
                    OnMapUI.Get().UpdatePanel(dPos);
                    transform.position = new Vector2(Pos().x+0.5f,Pos().y);
                });
            
            data.ap -= GameMgmt.Get().newMap.PathFinding(Pos().level).Cost(Player(),dataUnit.movement,Pos(),dPos);
            data.pos.x += x;
            data.pos.y += y;
            S.CameraMove().MoveTo(dPos);
            NAudio.Play("moveUnit");
        }

        /// <summary>
        /// Destroy this unit
        /// </summary>
        public override void Kill()
        {
            GameMgmt.Get().data.units.Remove(data);
            GameMgmt.Get().unit.units.Remove(this);
            Destroy(gameObject);
            Debug.Log($"Kill unit {name} at {Pos()}");
        }

        public BuildingUnitData GetData()
        {
            return data;
        }

        public override WindowBuilderSplit ShowInfoWindow()
        {
            WindowBuilderSplit wbs = base.ShowInfoWindow();
            wbs.Add(new UnitLexiconInfo(this), true);
            wbs.Add(new MapElementSplitInfo(this), true);
            wbs.Add(new UnitItemSplitInfo(this));
            
            LSys.tem.helps.AddHelp("unit", wbs);
            
            wbs.Finish();
            return wbs;
        }
    }
}
