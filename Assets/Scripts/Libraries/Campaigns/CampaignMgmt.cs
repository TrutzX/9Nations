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
                    Debug.LogWarning($"{name} missing {header} for data {data}");
                    break;
            }
        }

        protected override Campaign Create()
        {
            return new Campaign();
        }
        
        public void ShowCampaigns()
        {
            //load buildings
            WindowBuilderSplit b = WindowBuilderSplit.Create("Select your campaign","Play");

            foreach(Campaign c in Data.Values)
            {
                if (!c.Hidden)
                    b.AddElement(new CampaignSplitElement(c));
            }
            
            b.Finish();
        }

        public void ShowScenarios(Campaign campaign)
        {
            //load buildings
            WindowBuilderSplit b = WindowBuilderSplit.Create("Select your scenario","Play");

            Debug.Log("s "+campaign.Scenarios());
            Debug.Log("s "+campaign.Scenarios().Count);
            foreach(Scenario s in campaign.Scenarios())
            {
                Debug.Log("add "+s);
                b.AddElement(new ScenarioSplitElement(s));
            }
            
            b.Finish();
        }
    }
}