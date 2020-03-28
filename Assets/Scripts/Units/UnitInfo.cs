using System;
using System.Collections.Generic;
using System.Linq;
using Buildings;
using Classes;
using DataTypes;
using DigitalRuby.Tween;
using Game;
using Help;
using Libraries;
using Libraries.Terrains;
using Libraries.Units;
using MapActions;
using Maps;
using Players;
using reqs;
using Tools;
using Towns;
using UI;
using UI.Show;
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
            data.ap = dataUnit.ap;

            if (!base.NextRound())
            {
                return false;
            }
            
            //has a town?
            return data.townId != -1;

        }

        public void Init(string configType, int player, NVector pos)
        {
            Debug.Log($"{player}: Create unit {configType} at {pos}");
        
            baseData = dataUnit = L.b.units[configType];
            data = new BuildingUnitData();
            data.UnitInit(configType, -1, player);
            data.pos = pos.Clone();
        
            //has a town?
            Town t = S.Towns().NearstTown(PlayerMgmt.Get(player), pos, false);
            if (t != null)
            {
                int buildtime = L.b.modifiers["build"].CalcModi(dataUnit.buildTime, PlayerMgmt.Get(player), pos);
                
                data.townId = t.id;
                gameObject.AddComponent<Construction>();
                gameObject.GetComponent<Construction>().Init(data,dataUnit.cost,this,buildtime);
                PlayerMgmt.Get(player).fog.Clear(pos);
            }
            else
            {
                Clear(pos);
            }
        
            NextRound();
            FinishInit();
        }

        private void FinishInit()
        {
            name = dataUnit.name;

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

        public override void Upgrade(string type)
        {
            GameMgmt.Get().data.units.Remove(data);
            Init(type,data.playerId,Pos());
            GameMgmt.Get().data.units.Add(GetComponent<UnitInfo>().data);
        }

        public void MoveTo(int x, int y)
        {
            MoveBy(x-Pos().x,y-Pos().y);
        }
        public void MoveBy(int x, int y)
        {
            //own unit?
            if (!Owner(PlayerMgmt.ActPlayerID()))
            {
                OnMapUI.Get().unitUI.ShowPanelMessageError($"{name} belongs to {Player().name}.");
                return;
            }

            int dX = (int) GetComponent<Transform>().position.x + x;
            int dY = (int) GetComponent<Transform>().position.y + y;
            NVector dPos = new NVector(dX, dY, Pos().level);
            DataTerrain land = GameMgmt.Get().newMap.Terrain(dPos);

            //check terrain
            int cost = GameMgmt.Get().newMap.PathFinding(Pos().level).Cost(PlayerMgmt.ActPlayer(),dataUnit.movement,Pos(),dPos);
            if (cost == 0)
            {
                OnMapUI.Get().unitUI.ShowPanelMessageError($"Can not move in {land.name}, because it is not passable.");
                return;
            }

            //visible?
            if (!Player().fog.Visible(dPos))
            {
                OnMapUI.Get().unitUI.ShowPanelMessageError($"Can not move, the land is not explored.");
                return;
            }

            //another unit?
            if (!S.Unit().Free(dPos))
            {
                OnMapUI.Get().unitUI.ShowPanelMessageError($"Can not move in {land.name}, because {S.Unit().At(dPos).name} standing their.");
                return;
            }

            //can walk
            if (cost > data.ap)
            {
                OnMapUI.Get().unitUI.ShowPanelMessageError($"Can not move in {land.name}, because you need {cost - data.ap} more ap.");
                return;
            }

            //move it
            Action<ITween<Vector3>> update = (t) => { gameObject.transform.position = t.CurrentValue; };

            Action<ITween<Vector3>> completed = (t) =>
            {
                OnMapUI.Get().UpdatePanel(dPos);
                //show it
                Clear(dPos);
            };

            //rotate
            if (x > 0)
            {
                GetComponent<SpriteRenderer>().sprite = dataUnit.Sprite(7);
            }
            else if (x < 0)
            {
                GetComponent<SpriteRenderer>().sprite = dataUnit.Sprite(4);
            } 
            else if (y < 0)
            {
                GetComponent<SpriteRenderer>().sprite = dataUnit.Sprite();
            } 
            else 
            {
                GetComponent<SpriteRenderer>().sprite = dataUnit.Sprite(11);
            }

            // completion defaults to null if not passed in
            gameObject.Tween("MoveUnit", gameObject.transform.position, new Vector2(dX+0.5f,dY), 1, TweenScaleFunctions.Linear, update, completed);
            data.ap -= cost;
            data.pos.x += x;
            data.pos.y += y;
            NAudio.Play("moveUnit");
        }

        public void Teleport(NVector pos, bool moveCamera=true)
        {
            //change level?
            if (data.pos.level != pos.level)
            {
                transform.SetParent(GameMgmt.Get().newMap[pos.level].units.transform);
            }
            
            data.pos = pos;
            transform.position = new Vector2(Pos().x+0.5f,Pos().y);
            
            Clear(pos);

            if (moveCamera)
            {
                CameraMove.Get().MoveTo(pos);
                OnMapUI.Get().UpdatePanel(pos);
            }
            
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
            WindowBuilderSplit win = base.ShowInfoWindow();
            win.AddElement(new HelpSplitElement("unit"), true);
            win.AddElement(new UnitLexiconInfo(this), true);
            win.AddElement(new UnitSplitInfo(this), true);
            
            win.Finish();
            return win;
        }

        class UnitLexiconInfo : SplitElement
        {
            private readonly UnitInfo _unit;
        
            public UnitLexiconInfo(UnitInfo unit) : base("Lexicon",SpriteHelper.Load("magic:lexicon"))
            {
                this._unit = unit;
            }

            public override void ShowDetail(PanelBuilder panel)
            {
                _unit.dataUnit.ShowOwn(panel, _unit);
            }

            public override void Perform()
            {
            }
        }
    }

    class UnitSplitInfo : SplitElement
    {
        private readonly UnitInfo _unit;
        
        public UnitSplitInfo(UnitInfo unit) : base(unit.gameObject.name,unit.dataUnit.Sprite())
        {
            this._unit = unit;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            //diff unit?
            if (!_unit.Owner(PlayerMgmt.ActPlayerID()))
            {
                _unit.dataUnit.AddImageLabel(panel);
                panel.AddHeaderLabel("Information");
                panel.AddImageLabel($"Owner: {_unit.Player().name}",_unit.Player().icon);
                panel.AddImageLabel($"HP: ??/{_unit.dataUnit.hp}","hp");
                panel.AddImageLabel($"AP: ??/{_unit.dataUnit.ap}","ap");
                return;
            }

            panel.AddInputRandom("name", _unit.name,
                val => _unit.name = val,
                () => LClass.s.nameGenerators["unit"].Gen()+" "+_unit.dataUnit.name);
            
            panel.AddHeaderLabel("Information");
            panel.AddImageLabel($"HP: {_unit.data.hp}/{_unit.data.hpMax}","hp");
            panel.AddImageLabel($"AP: {_unit.data.ap}/{_unit.data.apMax}","ap");
            
            Construction con = _unit.GetComponent<Construction>();
            if (con != null)
            {
                panel.AddRes("Under Construction",_unit.data.construction.ToDictionary(entry => entry.Key,entry => entry.Value));
                panel.AddLabel("Missing resources");
            }
        }

        public override void Perform()
        {
        }
    }
}
