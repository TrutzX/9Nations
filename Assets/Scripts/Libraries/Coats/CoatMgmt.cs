using System;
using System.Collections.Generic;
using Libraries.Crafts;
using Tools;
using UnityEngine;

namespace Libraries.Coats
{
    [Serializable]
    public class CoatMgmt : BaseMgmt<Coat>
    {
        public CoatMgmt() : base("coat") { }

        protected override void ParseElement(Coat ele, string header, string data)
        {
            switch (header)
            {
                case "icon16":
                    ele.icon16 = data;
                    break;
                case "flag":
                    ele.flag = data;
                    break;
                case "flag16":
                    ele.flag16 = data;
                    break;
                case "color":
                    ColorUtility.TryParseHtmlString(data, out ele.color);
                    break;
                default:
                    base.ParseElement(ele, header, data);
                    break;
            }
        }

        public string Auto(int id)
        {
            return (id % GetAllByCategory("kingdom").Count).ToString();
        }
    }
}