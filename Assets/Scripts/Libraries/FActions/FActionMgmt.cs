using System;
using UnityEngine;

namespace Libraries.FActions
{
    [Serializable]
    public class FActionMgmt : BaseMgmt<FDataAction>
    {
        public FActionMgmt() : base("action") { }

        protected override void ParseElement(FDataAction ele, string header, string data)
        {
            switch (header)
            {
                case "cost":
                    ele.cost = Int(data);
                    break;
                case "useunderconstruction":
                    ele.useUnderConstruction = Bool(data);
                    break;
                case "onlyowner":
                    ele.onlyOwner = Bool(data);
                    break;
                case "sound":
                    ele.sound = data;
                    break;
                case "mapelement":
                    ele.mapElement = Bool(data);
                    break;
                case "field":
                    ele.field = data;
                    break;
                default:
                    Debug.LogWarning($"{name} missing {header} for data {data}");
                    break;
            }
        }

        protected override FDataAction Create()
        {
            return new FDataAction();
        }
    }
}