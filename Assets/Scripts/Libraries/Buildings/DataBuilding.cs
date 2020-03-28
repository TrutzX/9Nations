using System;

namespace Libraries.Buildings
{
    [Serializable]
    public class DataBuilding : BaseDataBuildingUnit
    {
        public int width;
        public int height;
        public int worker;
        public string connected;
        public string winter;
    }
}
