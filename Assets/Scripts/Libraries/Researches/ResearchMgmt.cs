using System;
using UnityEngine;

namespace Libraries.Researches
{
    [Serializable]
    public class ResearchMgmt : BaseMgmt<Researches.Research>
    {
        public ResearchMgmt() : base("research") { }

        protected override void ParseElement(Researches.Research ele, string header, string data)
        {
            switch (header)
            {
                case "element":
                    ele.elements.Add(data);
                    break;
                default:
                    Debug.LogWarning($"{name} missing {header} for data {data}");
                    break;
            }
        }

        protected override Researches.Research Create()
        {
            return new Researches.Research();
        }
    }
}