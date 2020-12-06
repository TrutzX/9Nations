using System;
using System.Collections.Generic;
using System.Linq;
using Libraries.Inputs;
using UnityEngine;

namespace Libraries.Options
{
    [Serializable]
    public class OptionMgmt<T> : BaseMgmt<T> where T : Option, new()
    {
        public OptionMgmt() : base("option") { }
        public OptionMgmt(string id) : base(id) { }

        protected override void ParseElement(T ele, string header, string data)
        {		
            switch (header)
            {
                case "standard":
                    ele.standard = data;
                    break;
                case "type":
                    ele.type = data;
                    break;
                default:
                    base.ParseElement(ele, header, data);
                    break;
            }
        }
    }
}