using System;
using System.Collections.Generic;
using System.Linq;
using DataTypes;
using Game;
using Help;
using Improvements;
using Libraries;
using Libraries.Buildings;
using Libraries.FActions;
using Players;
using reqs;
using Tools;
using Towns;
using UI;
using UI.Show;
using UnityEngine;

namespace Buildings
{
    public class BuildingInfo : MapElementInfo
    {
        public DataBuilding dataBuilding;
        private bool isWinter;

        public override bool NextRound()
        {
            data.lastInfo = null;
            data.BuildingUpdate();
            Town t = Town();

            if (!base.NextRound())
            {
                return false;
            }

            //need to change icon?
            
            //todo change dyn
            bool winter = GameMgmt.Get().gameRound.IsSeason("winter");
            //is no change
            if (isWinter == winter) { } 
            //change to summer?
            else if (isWinter && !winter)
            {
                isWinter = false;
                FinishInit();
            }
            //change to winter?
            else if (!isWinter && winter)
            {
                isWinter = true;
                FinishInit();
            }
            
            return true;
        }
    
        public void Init(int town, string configType, NVector pos)
        {
            Debug.Log($"Create building {configType} at {pos} for town {town}");
            baseData = dataBuilding = L.b.buildings[configType];
            data = new BuildingUnitData();
            data.BuildingInit(configType, town);
            data.pos = pos.Clone();
            
            int buildtime = L.b.modifiers["build"].CalcModi(dataBuilding.buildTime, S.Towns().Get(town).Player(), pos);

            gameObject.AddComponent<Construction>();
            gameObject.GetComponent<Construction>().Init(data,dataBuilding.cost,this, buildtime);
        
            PlayerMgmt.Get(Town().playerId).fog.Clear(pos);

            NextRound();
            FinishInit();
        
        }

        private void FinishInit()
        {
            gameObject.name = baseData.name;

            //show it
            GetComponent<Transform>().position = new Vector2(data.pos.x,data.pos.y);
            
            UpdateConnectedImage();
        }

        protected void UpdateConnectedImage()
        {
            if (!string.IsNullOrEmpty(dataBuilding.connected))
            {
                SetConnectedImage();
                BuildingMgmt.At(Pos().DiffY(1))?.SetConnectedImage();
                BuildingMgmt.At(Pos().DiffY(-1))?.SetConnectedImage();
                BuildingMgmt.At(Pos().DiffX(1))?.SetConnectedImage();
                BuildingMgmt.At(Pos().DiffX(-1))?.SetConnectedImage();
                
                return;
            }

            SetSprite(isWinter&&!string.IsNullOrEmpty(dataBuilding.winter)?dataBuilding.winter:dataBuilding.Icon);
        }

        public void SetConnectedImage()
        {
            //TODO dynmaic
            if (dataBuilding.connected == "wall")
            {
                bool north = BuildingMgmt.At(Pos().DiffY(1))?.dataBuilding.connected == dataBuilding.connected;
                bool east = BuildingMgmt.At(Pos().DiffX(1))?.dataBuilding.connected == dataBuilding.connected;
                bool south = BuildingMgmt.At(Pos().DiffY(-1))?.dataBuilding.connected == dataBuilding.connected;
                bool west = BuildingMgmt.At(Pos().DiffX(-1))?.dataBuilding.connected == dataBuilding.connected;

                string f = dataBuilding.Icon.Replace("14", ImprovementHelper.GetId(north, east, south, west)+"");
                SetSprite("Building/"+f);
            }
            
        }
        public override void Load(BuildingUnitData data)
        {
            baseData = dataBuilding = L.b.buildings[data.type];
            base.Load(data);
        
            FinishInit();
        }

        /// <summary>
        /// Destroy it
        /// </summary>
        public override void Kill()
        {
        
            //todo get ress back
            Destroy(gameObject);
        
            //reset images?
            UpdateConnectedImage();
            
            GameMgmt.Get().data.buildings.Remove(data);
            GameMgmt.Get().building.buildings.Remove(this);
            Destroy(gameObject);
            Debug.Log($"Kill building {name} at {Pos()}");
        }
        
        public override void FinishConstruct()
        {
            base.FinishConstruct();
            UpdateConnectedImage();
            
            //add worker
            if (dataBuilding.worker > 0)
            {
                Town().AddRes("inhabitant", (int) Math.Ceiling(dataBuilding.worker/4f), ResType.Gift);
            }
            
        }

        public override WindowBuilderSplit ShowInfoWindow()
        {
            WindowBuilderSplit win = base.ShowInfoWindow();
            win.AddElement(new HelpSplitElement("building"), true);
            win.AddElement(new BuildingLexiconInfo(this), true);
            win.AddElement(new BuildingSplitInfo(this), true);
            win.Finish();
            return win;
        }

        public override void Upgrade(string type)
        {
            GameMgmt.Get().data.buildings.Remove(data);
            Init(data.townId,type,Pos());
            GameMgmt.Get().data.buildings.Add(GetComponent<BuildingInfo>().data);
        }

        class BuildingSplitInfo : SplitElement
        {
            private readonly BuildingInfo _building;
        
            public BuildingSplitInfo(BuildingInfo unit) : base(unit.gameObject.name,unit.baseData.Sprite())
            {
                this._building = unit;
            }

            public override void ShowDetail(PanelBuilder panel)
            {
                panel.AddHeaderLabel("Information");
            
                //diff unit?
                if (_building.Town().playerId != PlayerMgmt.ActPlayerID())
                {
                    panel.AddLabel($"Owner: {_building.Town().Player().name}");
                    panel.AddImageLabel($"HP: ??/{_building.dataBuilding.hp}","stats:hp");
                    panel.AddImageLabel($"AP: ??/{_building.dataBuilding.ap}","stats:ap");
                    return;
                }
            
                panel.AddImageLabel($"HP: {_building.data.hp}/{_building.data.hpMax}","stats:hp");
                panel.AddImageLabel($"AP: {_building.data.ap}/{_building.data.apMax}","stats:ap");
            
                Construction con = _building.GetComponent<Construction>();
                if (con != null)
                {
                    panel.AddRes("Under Construction",_building.data.construction.ToDictionary(entry => entry.Key,entry => entry.Value));
                    panel.AddLabel("Missing resources");
                }
            }

            public override void Perform()
            {
            }
        }

        class BuildingLexiconInfo : SplitElement
        {
            private readonly BuildingInfo _unit;
        
            public BuildingLexiconInfo(BuildingInfo unit) : base("Lexicon",SpriteHelper.Load("magic:lexicon"))
            {
                this._unit = unit;
            }

            public override void ShowDetail(PanelBuilder panel)
            {
                _unit.baseData.ShowOwn(panel, _unit);
            
            }

            public override void Perform()
            {
            }
        }
    }
}
