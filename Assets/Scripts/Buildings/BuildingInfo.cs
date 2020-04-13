using System;
using System.Collections.Generic;
using System.Linq;

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
            data.ap = dataBuilding.ap;
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
                S.Building().At(Pos().DiffY(1))?.SetConnectedImage();
                S.Building().At(Pos().DiffY(-1))?.SetConnectedImage();
                S.Building().At(Pos().DiffX(1))?.SetConnectedImage();
                S.Building().At(Pos().DiffX(-1))?.SetConnectedImage();
                
                return;
            }

            SetSprite(isWinter&&!string.IsNullOrEmpty(dataBuilding.winter)?dataBuilding.winter:dataBuilding.Icon);
        }

        public void SetConnectedImage()
        {
            //TODO dynmaic
            if (dataBuilding.connected == "wall")
            {
                bool north = S.Building().At(Pos().DiffY(1))?.dataBuilding.connected == dataBuilding.connected;
                bool east = S.Building().At(Pos().DiffX(1))?.dataBuilding.connected == dataBuilding.connected;
                bool south = S.Building().At(Pos().DiffY(-1))?.dataBuilding.connected == dataBuilding.connected;
                bool west = S.Building().At(Pos().DiffX(-1))?.dataBuilding.connected == dataBuilding.connected;

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
            WindowBuilderSplit wbs = base.ShowInfoWindow();
            wbs.AddElement(new BuildingLexiconInfo(this), true);
            wbs.AddElement(new BuildingSplitInfo(this), true);
            LSys.tem.helps.AddHelp("building", wbs);
            wbs.Finish();
            return wbs;
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
                    panel.AddImageLabel($"HP: ??/{_building.dataBuilding.hp}","hp");
                    panel.AddImageLabel($"AP: ??/{_building.dataBuilding.ap}","ap");
                    return;
                }
            
                panel.AddImageLabel($"HP: {_building.data.hp}/{_building.data.hpMax}","hp");
                panel.AddImageLabel($"AP: {_building.data.ap}/{_building.data.apMax}","ap");
            
                Construction con = _building.GetComponent<Construction>();
                if (con != null)
                {
                    panel.AddRes("Under construction",_building.data.construction.ToDictionary(entry => entry.Key,entry => entry.Value));
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
        
            public BuildingLexiconInfo(BuildingInfo unit) : base("Lexicon","lexicon")
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
