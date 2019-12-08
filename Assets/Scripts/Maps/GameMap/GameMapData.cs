using System;
using System.Collections.Generic;
using IniParser.Model;

namespace Maps
{
    [Serializable]
    public class GameMapData
    {
        public string id;
        public int standard;
        public int width, height;
        public bool IsWinter;
        
        public List<GameMapDataLevel> levels;

        public GameMapData()
        {
            levels = new List<GameMapDataLevel>();
        }
    }
}