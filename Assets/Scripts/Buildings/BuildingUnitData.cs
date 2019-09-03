using System;
using System.Collections.Generic;

namespace Buildings
{
    [Serializable]
    public class BuildingUnitData
    {
        public int ap;
        public int hp;
        public int town;
        public int x;
        public int y;
        public string type;
        public string lastError;
        public Dictionary<string, int> construction;
        public int buildTime;
    }
}