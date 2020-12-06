using UI;
using UI.Show;
using UnityEngine;

namespace Towns
{
    public class DebugTownSplitElement : SplitElement
    {
        private Town town;
        public DebugTownSplitElement(Town town) : base("Debug "+town.name,"debug")
        {
            this.town = town;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            panel.AddHeaderLabel("Level");
            panel.AddInput("Level", town.level, (s => { town.level = s; }));
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}