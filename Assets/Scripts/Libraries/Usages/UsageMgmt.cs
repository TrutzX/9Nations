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
                default:
                    Debug.LogWarning($"{name} missing {header} for data {data}");
                    break;
            }
        }

        protected override Usage Create()
        {
            return new Usage();
        }
    }
}