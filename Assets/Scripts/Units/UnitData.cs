using System;
using Buildings;

namespace Units
{
    [Serializable]
    public class UnitData : BuildingUnitData
    {
        public int player;
        
        public void Init(string type, int town, int player)
        {
            this.type = type;
            hp = Data.unit[type].hp;
            this.town = town;
            this.player = player;
        }
    }
}