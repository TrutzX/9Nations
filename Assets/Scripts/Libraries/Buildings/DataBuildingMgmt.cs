using System;

namespace Libraries.Buildings
{
    [Serializable]
    public class DataBuildingMgmt : BaseDataBuildingUnitMgmt<DataBuilding>
    {
        public DataBuildingMgmt() : base("building","buildings","build") { }

        protected override void ParseElement(DataBuilding ele, string header, string data)
        {
            switch (header)
            {
                case "width":
                    ele.width = Int(data);
                    break;
                case "height":
                    ele.height = Int(data);
                    break;
                case "worker":
                    ele.worker = Int(data);
                    break;
                case "connected":
                    ele.connected = data;
                    break;
                case "winter":
                    ele.winter = data;
                    break;
                default:
                    base.ParseElement(ele, header, data);
                    break;
            }
        }
    }
}