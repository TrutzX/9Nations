using UI;
using UI.Show;
using UnityEngine;

namespace Libraries.Campaigns
{
    public class CampaignSplitElement : SplitElement
    {
        private Campaign campaign;
        public CampaignSplitElement(Campaign campaign) : base(campaign.Name(), SpriteHelper.Load(campaign.Icon))
        {
            this.campaign = campaign;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            campaign.ShowDetail(panel);
        }

        public override void Perform()
        {
            Debug.Log("start2 "+campaign.Name());
            Debug.Log(campaign.Scenarios());
            LSys.tem.campaigns.ShowScenarios(campaign);
        }
    }
}