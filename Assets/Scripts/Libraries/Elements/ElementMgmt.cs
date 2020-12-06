using System;
using UnityEngine;

namespace Libraries.Elements
{
    [Serializable]
    public class ElementMgmt : BaseMgmt<Element>
    {
        public ElementMgmt() : base("element") { }

        protected override void ParseElement(Element ele, string header, string data)
        {
            switch (header)
            {
                case "townhall":
                    ele.townHall = data;
                    break;
                default:
                    base.ParseElement(ele, header, data);
                    break;
            }
        }
    }
}