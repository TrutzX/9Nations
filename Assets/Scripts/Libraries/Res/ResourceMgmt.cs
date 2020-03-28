using System;
using UnityEngine;

namespace Libraries.Res
{
    [Serializable]
    public class ResourceMgmt : BaseMgmt<Resource>
    {
        public ResourceMgmt() : base("resource","resources","res") { }

        protected override void ParseElement(Resource ele, string header, string data)
        {
            switch (header)
            {
                case "price":
                    ele.price = Float(data);
                    break;
                case "weight":
                    ele.weight = Float(data);
                    break;
                case "special":
                    ele.special = Bool(data);
                    break;
                default:
                    Debug.LogWarning($"{name} missing {header} for data {data}");
                    break;
            }
        }

        protected override Resource Create()
        {
            return new Resource();
        }
    }
}