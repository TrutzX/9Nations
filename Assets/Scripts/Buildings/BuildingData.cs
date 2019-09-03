using System;

namespace Buildings
{
    [Serializable]
    public class BuildingData : BuildingUnitData
    {
        public void Init(string type, int town)
        {
            this.type = type;
            hp = Data.building[type].hp;
            this.town = town;
        }
    }
}