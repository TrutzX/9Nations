using System;
using UnityEngine;

namespace Libraries.Usages
{
    [Serializable]
    public class UsageMgmt : BaseMgmt<Usage>
    {
        public UsageMgmt() : base("usage") { }

        protected override void ParseElement(Usage ele, string header, string data)
        {
            switch (header)
            {
                case "rate":
                    ele.rate = Float(data);
                    break;
                case "factor":
                    ele.factor = Int(data);
                    break;
                default:
                    base.ParseElement(ele, header, data);
                    break;
            }
        }
    }
}