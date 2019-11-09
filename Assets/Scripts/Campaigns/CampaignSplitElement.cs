using MapActions;
using UI;

namespace Campaigns
{
    public class CampaignSplitElement : SplitElement
    {
        private DataTypes.Campaign campaign;
        public CampaignSplitElement(DataTypes.Campaign campaign) : base(campaign.name, SpriteHelper.LoadIcon(campaign.icon))
        {
            this.campaign = campaign;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            campaign.ShowDetail(panel);
        }

        public override void Perform()
        {
            CampaignHelper.ShowScenarios(campaign);
        }
    }
}