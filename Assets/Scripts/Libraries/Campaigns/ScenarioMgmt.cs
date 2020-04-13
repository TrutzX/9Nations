using System;
using UnityEngine;

namespace Libraries.Campaigns
{
    [Serializable]
    public class ScenarioMgmt : BaseMgmt<Scenario>
    {
        public ScenarioMgmt() : base("scenario") { }

        protected override void ParseElement(Scenario ele, string header, string data)
        {
            switch (header)
            {
                case "map":
                    ele.map = data;
                    break;
                case "campaign":
                    ele.campaign = data;
                    break;
                default:
                    base.ParseElement(ele, header, data);
                    break;
            }
        }
    }
}