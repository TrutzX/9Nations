using System;
using System.Collections.Generic;
using Game;
using Libraries;
using Libraries.Buildings;
using Libraries.FActions;
using Libraries.Units;
using Players;
using Tools;
using Towns;

namespace Buildings
{
    [Serializable]
    public class BuildingUnitData
    {
        public int ap;
        public int apMax;
        public int hp;
        public int hpMax;
        public int townId;
        public int playerId;
        public NVector pos;
        public string type;
        public string name;
        public string sprite;
        public string lastInfo;
        public Dictionary<string, string> data;
        public Dictionary<string, int> construction;
        public ActionHolders action;
        public int ActionWaitingAp, actionWaitingActionPos;
        public NVector actionWaitingPos;
        
        public int buildTime;
        
        public void BuildingInit(string type, int town)
        {
            this.type = type;
            DataBuilding b = L.b.buildings[type];
            BaseInit(b);
            townId = town;
            BuildingUpdate();
        }
        
        public void BuildingUpdate()
        {
            playerId = S.Towns().Get(townId).playerId;
        }

        private void BaseInit(BaseDataBuildingUnit d)
        {
            action = new ActionHolders(d.action);
            sprite = d.Icon;
            name = d.Name();
            hp = d.hp;
            hpMax = d.hp;
            ap = d.ap;
            apMax = d.ap;
            actionWaitingActionPos = -1;
            data = new Dictionary<string, string>();
        }
        
        public void UnitInit(string type, int town, int player)
        {
            this.type = type;
            DataUnit u = L.b.units[type];
            BaseInit(u);
            townId = town;
            playerId = player;
        }
        
        public void UnitUpdate()
        {
            //has a town?
            Town t = S.Towns().NearestTown(S.Player(playerId), pos, false);
            townId = t?.id ?? -1;
        }
    }
}