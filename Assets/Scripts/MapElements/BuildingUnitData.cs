using System;
using System.Collections.Generic;
using Game;
using Libraries;
using Libraries.Buildings;
using Libraries.FActions;
using Libraries.Units;
using MapElements;
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
        public int atk;
        public int def;
        public int visibilityRange;
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
        public int actionWaitingAp, actionWaitingActionPos;
        public Dictionary<string, string> items;
        public NVector actionWaitingPos;
        public Dictionary<string, string> modi;
        
        public int buildTime;
        
        public void BuildingInit(string type, int town, CalculatedData calc)
        {
            this.type = type;
            DataBuilding b = L.b.buildings[type];
            BaseInit(b, calc);
            townId = town;
            BuildingUpdate();
        }
        
        public void BuildingUpdate()
        {
            playerId = S.Towns().Get(townId).playerId;
        }

        private void BaseInit(BaseDataBuildingUnit d, CalculatedData calc)
        {
            action = new ActionHolders(d.action);
            sprite = d.Icon;
            name = d.Name();
            hp = calc.hp;
            hpMax = calc.hp;
            ap = calc.ap;
            apMax = calc.ap;
            atk = d.atk;
            def = d.def;
            visibilityRange = d.visibilityRange;
            actionWaitingActionPos = -1;
            data = new Dictionary<string, string>();
            items = new Dictionary<string, string>();
            modi = new Dictionary<string, string>();
        }
        
        public void UnitInit(string type, int town, int player, CalculatedData calc)
        {
            this.type = type;
            DataUnit u = L.b.units[type];
            BaseInit(u, calc);
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