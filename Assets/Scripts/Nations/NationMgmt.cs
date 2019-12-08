using System;
using Libraries;
using Modifiers;

namespace Nations
{
    [Serializable]
    public class NationMgmt : BaseMgmt<Nation>
    {
        public NationMgmt()
        {
            name = "Nation";
        }
        
        protected override void ParseElement(Nation ele, string header, string data)
        {
            switch (header)
            {
                case "leader":
                    ele.Leader = data;
                    break;
                case "townhall":
                    ele.Townhall = data;
                    break;
                case "researchElement":
                    ele.ResearchElement = data;
                    break;
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
                    Ref(ele.Modi, data);
                    break;
                default:
                    //Debug.LogWarning($"{name} missing {header} for data {data}");
                    break;
            }
        }
        
        protected override Nation Create()
        {
            return new Nation();
        }
    }
}