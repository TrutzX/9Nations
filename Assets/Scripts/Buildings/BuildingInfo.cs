using System;
using System.Collections.Generic;
using Game;
using Help;
using Improvements;
using Libraries;
using Libraries.Buildings;
using Libraries.FActions;
using Libraries.Improvements;
using Players;
using reqs;
using Tools;
using Towns;
using UI;
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
            
            int buildtime = L.b.modifiers[C.BuildRes].CalcModi(dataBuilding.buildTime, S.Towns().Get(town).Player(), pos);
            gameObject.AddComponent<Construction>().Init(data,dataBuilding.cost,this, buildtime);
        
            S.Player(Town().playerId).fog.Clear(pos);

            FinishInit();
        
        }

        private void FinishInit()
        {
            gameObject.name = baseData.Name();

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
            
            //remove border
            Player().overlay.Add("frontier",CircleGenerator.Gen(Pos(),baseData.visibilityRange), -1);
            Town().overlay.Add("boundary",CircleGenerator.Gen(Pos(),baseData.visibilityRange), -1);
            
            GameMgmt.Get().data.buildings.Remove(data);
            GameMgmt.Get().building.buildings.Remove(this);
            Destroy(gameObject);
            Debug.Log($"Kill building {name} at {Pos()}");
        }
        
        public override void FinishConstruct()
        {
            base.FinishConstruct();
            UpdateConnectedImage();
            
            //add border
            Player().overlay.Add("frontier",CircleGenerator.Gen(Pos(),baseData.visibilityRange), 1);
            Town().overlay.Add("boundary",CircleGenerator.Gen(Pos(),baseData.visibilityRange), 1);
            
            //add worker
            if (dataBuilding.worker > 0)
            {
                Town().AddRes(C.Inhabitant, (int) Math.Ceiling(dataBuilding.worker/4f), ResType.Gift);
            }
            
        }

        public override WindowBuilderSplit ShowInfoWindow()
        {
            WindowBuilderSplit wbs = base.ShowInfoWindow();
            wbs.Add(new BuildingLexiconInfo(this), true);
            wbs.Add(new MapElementSplitInfo(this), true);
            LSys.tem.helps.AddHelp("building", wbs);
            wbs.Finish();
            return wbs;
        }

        public override void Upgrade(string type)
        {
            BaseDataBuildingUnit old = baseData;
            
            GameMgmt.Get().data.buildings.Remove(data);
            Init(data.townId,type,Pos());
            GameMgmt.Get().data.buildings.Add(data);
            
            CalcUpgradeCost(L.b.buildings[type], old);
        }
    }
}
