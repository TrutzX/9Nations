using System;
using System.Collections.Generic;
using System.Linq;
using Buildings;
using DataTypes;
using DigitalRuby.Tween;
using Game;
using Help;
using Libraries;
using MapActions;
using Maps;
using Players;
using reqs;
using Terrains;
using Towns;
using UI;
using UnityEngine;

namespace Units
{
    public class UnitInfo : MapElementInfo
    {
        public Unit config;

        public void NextRound()
        {
            data.lastInfo = null;
            data.UnitUpdate();
        
            //under construction?
            if (IsUnderConstrution() && GetComponent<Construction>().RoundConstruct())
            {
                return;
            }
        
            //has a town?
            if (data.townId == -1)
            {
                data.ap = config.ap;
                return;
            }
            

            int x = X();
            int y = Y();
            Town t = Town();
        
            //produce?
            if (!ReqHelper.Check(PlayerMgmt.Get(t.playerId),config.GenProduceReq(),this,x,y))
            {
                SetLastInfo(ReqHelper.Desc(PlayerMgmt.Get(t.playerId), config.GenProduceReq(), this, x, y));
                return;
            }
        
            foreach (KeyValuePair<string, int> ress in config.GenProduce())
            {
                //give ressources
                t.AddRes(ress.Key,ress.Value);
            }
        
            data.ap = config.ap;
        
        }

        public void Init(string configType, int player, int x, int y)
        {
            Debug.Log($"Create unit {configType} at {x},{y}");
        
            data = new BuildingUnitData();
            data.UnitInit(configType, -1, player);
            data.x = x;
            data.y = y;
            config = Data.unit[configType];
        
            //has a town?
            Town t = TownMgmt.Get().NearstTown(PlayerMgmt.Get(player), x, y, false);
            if (t != null)
            {
                //TODO Vector3Int Z
                int buildtime = L.b.modifiers["build"].CalcModi(config.buildtime, PlayerMgmt.Get(player), new Vector3Int(x, y, 0));
                
                data.townId = t.id;
                gameObject.AddComponent<Construction>();
                gameObject.GetComponent<Construction>().Init(data,config.GenCost(),this,buildtime);
                PlayerMgmt.Get(player).fog.Clear(x,y);
            }
            else
            {
                PlayerMgmt.Get(player).fog.Clear(x,y, L.b.modifiers["view"].CalcModiNotNull(config.visible,Player(),Pos()));
            }
        

            FinishInit();
        }

        private void FinishInit()
        {
            NextRound();
            gameObject.name = config.name;

            //show it
            GetComponent<SpriteRenderer>().sprite = config.GetIcon();
            GetComponent<Transform>().position = new Vector2(data.x+0.5f,data.y);
        }

        public override void Load(BuildingUnitData data)
        {
            config = Data.unit[data.type];
            base.Load(data);
        
            FinishInit();
        }

        public override void Upgrade(string type)
        {
            GameMgmt.Get().data.units.Remove(data);
            Init(type,data.playerId,X(),Y());
            GameMgmt.Get().data.units.Add(GetComponent<UnitInfo>().data);
        }

        public void MoveTo(int x, int y)
        {
            MoveBy(x-X(),y-Y());
        }
        public void MoveBy(int x, int y)
        {
            //own unit?
            if (!Owner(PlayerMgmt.ActPlayerID()))
            {
                OnMapUI.Get().unitUI.SetPanelMessageError($"{name} belongs to {Player().name}.");
                return;
            }

            int dX = (int) GetComponent<Transform>().position.x + x;
            int dY = (int) GetComponent<Transform>().position.y + y;
            BTerrain land = GameMgmt.Get().map.GetTerrain(dX, dY);

            //check terrain
            int cost = GameMgmt.Get().map.Levels[Pos().z].PathFinding().Cost(PlayerMgmt.ActPlayer(),config.movetyp,Pos(),dX,dY);
            if (cost == 0)
            {
                OnMapUI.Get().unitUI.SetPanelMessageError($"Can not move in {land.Name}, because it is not passable.");
                return;
            }

            //visible?
            if (!Player().fog.visible[dX, dY])
            {
                OnMapUI.Get().unitUI.SetPanelMessageError($"Can not move, the land is not explored.");
                return;
            }

            //another unit?
            if (!UnitMgmt.Get().IsFree(dX,dY))
            {
                OnMapUI.Get().unitUI.SetPanelMessageError($"Can not move in {land.Name}, because {UnitMgmt.At(dX, dY).name} standing their.");
                return;
            }

            //can walk
            if (cost > data.ap)
            {
                OnMapUI.Get().unitUI.SetPanelMessageError($"Can not move in {land.Name}, because you need {cost - data.ap} more ap.");
                return;
            }

            //move it
            Action<ITween<Vector3>> update = (t) => { gameObject.transform.position = t.CurrentValue; };

            Action<ITween<Vector3>> completed = (t) =>
            {
                OnMapUI.Get().UpdatePanelXY(dX, dY);
                //show it
                Player().fog.Clear(dX, dY, L.b.modifiers["view"].CalcModiNotNull(config.visible,Player(),new Vector3Int(X(),Y(),data.z)));
            };

            //rotate
            if (x > 0)
            {
                GetComponent<SpriteRenderer>().sprite = config.GetIcon(7);
            }
            else if (x < 0)
            {
                GetComponent<SpriteRenderer>().sprite = config.GetIcon(4);
            } 
            else if (y < 0)
            {
                GetComponent<SpriteRenderer>().sprite = config.GetIcon();
            } 
            else 
            {
                GetComponent<SpriteRenderer>().sprite = config.GetIcon(11);
            }
    

            // completion defaults to null if not passed in
            gameObject.Tween("MoveUnit", gameObject.transform.position, new Vector2(dX+0.5f,dY), 1, TweenScaleFunctions.Linear, update, completed);
            data.ap -= cost;
            data.x += x;
            data.y += y;
            NAudio.Play("moveUnit");
        }

        public override string UniversalImage()
        {
            return "u:" + config.id;
        }

        /// <summary>
        /// Destroy this unit
        /// </summary>
        public override void Kill()
        {
            GameMgmt.Get().data.units.Remove(data);
            Destroy(gameObject);
        }

        public override void FinishConstruct()
        {
        
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

        class UnitSplitInfo : SplitElement
        {
            private readonly UnitInfo _unit;
        
            public UnitSplitInfo(UnitInfo unit) : base(unit.gameObject.name,unit.config.GetIcon())
            {
                this._unit = unit;
            }

            public override void ShowDetail(PanelBuilder panel)
            {
                panel.AddHeaderLabel("Information");
            
                //diff unit?
                if (!_unit.Owner(PlayerMgmt.ActPlayerID()))
                {
                    panel.AddLabel($"Owner: {_unit.Player().name}");
                    panel.AddImageLabel($"HP: ??/{_unit.config.hp}","stats:hp");
                    panel.AddImageLabel($"AP: ??/{_unit.config.ap}","stats:ap");
                    return;
                }
            
                panel.AddImageLabel($"HP: {_unit.data.hp}/{_unit.data.hpMax}","stats:hp");
                panel.AddImageLabel($"AP: {_unit.data.ap}/{_unit.data.apMax}","stats:ap");
            
                Construction con = _unit.GetComponent<Construction>();
                if (con != null)
                {
                    panel.AddRess("Under Construction",_unit.data.construction.ToDictionary(entry => entry.Key,entry => entry.Value));
                    panel.AddLabel("Missing resources");
                }
            }

            public override void Perform()
            {
            }
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
                _unit.config.ShowInfo(panel);
            }

            public override void Perform()
            {
            }
        }
    }
}
