using UI;
using UI.Show;
using UnityEngine;

namespace Libraries.Campaigns
{
    public class ScenarioSplitElement : SplitElement
    {
        private Scenario scenario;
        public ScenarioSplitElement(Scenario scenario) : base(scenario.name, scenario.Icon)
        {
            this.scenario = scenario;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            scenario.ShowDetail(panel);
        }

        public override void Perform()
        {
            Debug.Log("start "+scenario.name);
            scenario.Start();
        }
    }
}