using System;
using UnityEngine;

namespace Libraries.Researches
{
    [Serializable]
    public class ResearchMgmt : BaseMgmt<Research>
    {
        public ResearchMgmt() : base("research") { }

        protected override void ParseElement(Research ele, string header, string data)
        {
            switch (header)
            {
                case "element":
                    ele.elements.Add(data);
                    break;
                default:
                    base.ParseElement(ele, header, data);
                    break;
            }
        }
    }
}