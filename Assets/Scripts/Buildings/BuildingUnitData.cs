using System;
using System.Collections.Generic;
using Players;
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
        public int x;
        public int y;
        public string type;
        public string name;
        public string lastInfo;
        public Dictionary<string, int> construction;
        public int buildTime;
        
        public void BuildingInit(string type, int town)
        {
            this.type = type;
            name = Data.building[type].name;
            hp = Data.building[type].hp;
            hpMax = Data.building[type].hp;
            ap = Data.building[type].ap;
            apMax = Data.building[type].ap;
            this.townId = town;
            BuildingUpdate();
        }
        
        public void BuildingUpdate()
        {
            playerId = TownMgmt.Get(townId).playerId;
        }
        
        public void UnitInit(string type, int town, int player)
        {
            this.type = type;
            name = Data.unit[type].name;
            hp = Data.unit[type].hp;
            hpMax = Data.unit[type].hp;
            ap = Data.unit[type].ap;
            apMax = Data.unit[type].ap;
            townId = town;
            playerId = player;
        }
        
        public void UnitUpdate()
        {
            //has a town?
            Town t = TownMgmt.Get().NearstTown(PlayerMgmt.Get(playerId), x, y, false);
            townId = t?.id ?? -1;
        }
    }
}