using UI;
using UI.Show;
using UnityEngine;

namespace Libraries.Campaigns
{
    public class CampaignSplitElement : SplitElement
    {
        private Campaign campaign;
        public CampaignSplitElement(Campaign campaign) : base(campaign.name, SpriteHelper.Load(campaign.Icon))
        {
            this.campaign = campaign;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            campaign.ShowDetail(panel);
        }

        public override void Perform()
        {
            Debug.Log("start2 "+campaign.name);
            Debug.Log(campaign.Scenarios());
            L.b.campaigns.ShowScenarios(campaign);
        }
    }
}