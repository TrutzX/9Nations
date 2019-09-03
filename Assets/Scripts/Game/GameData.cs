using System;
using System.Collections.Generic;
using Buildings;
using Players;
using Units;

namespace Game
{
    [Serializable]
    public class GameData
    {
        public int round;
        public string mapfile;
        public int mapwidth, mapheight;

        public PlayerMgmt players;

        public TownMgmt towns;
        
        public List<UnitData> units;
        public List<BuildingData> buildings;
    }
}