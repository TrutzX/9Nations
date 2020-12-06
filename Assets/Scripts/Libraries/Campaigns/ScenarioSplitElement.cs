using UI;
using UI.Show;
using UnityEngine;

namespace Libraries.Campaigns
{
    public class ScenarioSplitElement : SplitElement
    {
        private Scenario scenario;
        public ScenarioSplitElement(Scenario scenario) : base(scenario.Name(), scenario.Icon)
        {
            this.scenario = scenario;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            scenario.ShowLexicon(panel);
        }

        public override void Perform()
        {
            scenario.Start();
        }
    }
}