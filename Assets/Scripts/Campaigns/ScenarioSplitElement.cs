using DataTypes;
using UI;

namespace Campaigns
{
    public class ScenarioSplitElement : SplitElement
    {
        private DataTypes.Scenario scenario;
        public ScenarioSplitElement(DataTypes.Scenario scenario) : base(scenario.name, SpriteHelper.LoadIcon(scenario.icon))
        {
            this.scenario = scenario;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            scenario.ShowDetail(panel);
        }

        public override void Perform()
        {
            scenario.Start();
        }
    }
}