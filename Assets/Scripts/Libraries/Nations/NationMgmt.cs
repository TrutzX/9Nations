using System;
using UnityEngine;

namespace Libraries.Nations
{
    [Serializable]
    public class NationMgmt : BaseMgmt<Nation>
    {
        public NationMgmt() : base("nation") { }
        
        protected override void ParseElement(Nation ele, string header, string data)
        {
            switch (header)
            {
                case "terrain":
                    ele.Terrain = data;
                    break;
                case "townNameGenerator":
                    ele.TownNameGenerator = data;
                    break;
                case "townNameLevel":
                    ele.TownNameLevel.Add(data);
                    break;
                case "ethos":
                    ele.Ethos = data;
                    break;
                case "modi":
                    Delimiter(ele.Modi, data);
                    break;
                case "element":
                    ele.elements.Add(data);
                    break;
                case "maxelement":
                    ele.maxElement = Int(data);
                    break;
                default:
                    base.ParseElement(ele, header, data);
                    break;
            }
        }
    }
}