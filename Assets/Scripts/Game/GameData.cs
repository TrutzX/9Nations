using System;
using System.Collections.Generic;
using Buildings;
using Maps;
using Players;
using Towns;
using Units;

namespace Game
{
    [Serializable]
    public class GameData
    {
        public int round;
        public string name;
        public GameMapData map;

        public PlayerMgmt players;

        public TownMgmt towns;
        
        public List<BuildingUnitData> units;
        public List<BuildingUnitData> buildings;

        public Dictionary<string, string> features;
        
        public Dictionary<string, string> modi;
        
    }
}