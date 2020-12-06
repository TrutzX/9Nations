using System;
using UnityEngine;

namespace Libraries.Res
{
    [Serializable]
    public class ResourceMgmt : BaseMgmt<Resource>
    {
        public ResourceMgmt() : base("resource","res") { }

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
                case "hp":
                    ele.hp = Float(data);
                    break;
                case "ap":
                    ele.ap = Float(data);
                    break;
                case "modi":
                    Delimiter(ele.modi, data);
                    break;
                case "special":
                    ele.special = Bool(data);
                    break;
                case "combine":
                    ele.combine = data;
                    break;
                case "overlay":
                    ele.overlay.Add(data);
                    break;
                default:
                    base.ParseElement(ele, header, data);
                    break;
            }
        }
    }
}