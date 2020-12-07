using System;
using UI;
using UnityEngine;

namespace Libraries.Campaigns
{
    [Serializable]
    public class CampaignMgmt : BaseMgmt<Campaign>
    {
        public CampaignMgmt() : base("campaign") { }

        protected override void ParseElement(Campaign ele, string header, string data)
        {
            switch (header)
            {
                case "progress":
                    ele.progress = Bool(data);
                    break;
                default:
                    base.ParseElement(ele, header, data);
                    break;
            }
        }
        
        public void ShowCampaigns()
        {
            //load buildings
            WindowBuilderSplit b = WindowBuilderSplit.Create("Select your campaign","Play");

            foreach(Campaign c in data.Values)
            {
                if (!c.Hidden)
                    b.Add(new CampaignSplitElement(c));
            }
            
            b.Finish();
        }

        public void ShowScenarios(Campaign campaign)
        {
            //load buildings
            WindowBuilderSplit b = WindowBuilderSplit.Create("Select your scenario","Play");

            foreach(Scenario s in campaign.Scenarios())
            {
                if (s.req.Check(null))
                    b.Add(new ScenarioSplitElement(s));
            }
            
            b.Finish();
        }
    }
}