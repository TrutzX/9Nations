using System;
using System.Collections.Generic;
using IniParser.Model;
using Maps.GameMaps;
using Tools;

namespace Maps
{
    [Serializable]
    public class GameMapData
    {
        public string id;
        public int standard;
        public int width, height;
        
        public List<GameMapDataLevel> levels;

        public GameMapData()
        {
            levels = new List<GameMapDataLevel>();
        }

        public int ResGen(NVector pos, string type)
        {
            return levels[pos.level].ResGen(pos.x, pos.y, type);
        }

        public void ResGenAdd(NVector pos, string type, int amount)
        {
            levels[pos.level].ResGenAdd(pos.x, pos.y, type, amount);
        }

        public bool ResGenContains(NVector pos, string type)
        {
            return levels[pos.level].ResGenContains(pos.x, pos.y, type);
        }
    }
}