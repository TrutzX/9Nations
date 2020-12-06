using System;
using System.Collections.Generic;
using Game;
using Libraries;
using Libraries.Buildings;
using Libraries.FActions;
using Libraries.Units;
using MapElements;
using MapElements.Spells;
using Players;
using Players.Infos;
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
        public Dictionary<string, string> data;
        public Dictionary<string, int> construction;
        public Dictionary<string, int> constructionOrg;
        public ActionHolders action;
        public ActionWaiting waiting;
        public Dictionary<string, string> items;
        public Dictionary<string, string> modi;
        public MapElementSpells spells;
        public MapElementInfoInfoMgmt info;
        
        public int buildTime;
        
        public void BuildingInit(MapElementInfo mei, string type, int town, CalculatedData calc)
        {
            this.type = type;
            DataBuilding b = L.b.buildings[type];
            BaseInit(mei, b, calc);
            townId = town;
            BuildingUpdate();
        }
        
        public void BuildingUpdate()
        {
            playerId = S.Towns().Get(townId).playerId;
        }

        private void BaseInit(MapElementInfo mei, BaseDataBuildingUnit d, CalculatedData calc)
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
            data = new Dictionary<string, string>();
            items = new Dictionary<string, string>();
            modi = new Dictionary<string, string>();
            spells = new MapElementSpells();
            info = new MapElementInfoInfoMgmt();
            info.mapElementInfo = mei;
        }
        
        public void UnitInit(MapElementInfo mei, string type, int town, int player, CalculatedData calc)
        {
            this.type = type;
            DataUnit u = L.b.units[type];
            BaseInit(mei, u, calc);
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