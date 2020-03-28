using System;
using Libraries.Buildings;

namespace Libraries.Units
{
    [Serializable]
    public class DataUnitMgmt : BaseDataBuildingUnitMgmt<DataUnit>
    {
        public DataUnitMgmt() : base("unit", "Units", "train") { }

        protected override void ParseElement(DataUnit ele, string header, string data)
        {
            switch (header)
            {
                case "movement":
                    ele.movement = data;
                    break;
                default:
                    base.ParseElement(ele, header, data);
                    break;
            }
        }

        protected override DataUnit Create()
        {
            return new DataUnit();
        }
    }
}