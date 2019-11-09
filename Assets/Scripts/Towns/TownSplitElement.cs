using UI;
using UnityEngine;

namespace Towns
{
    public class TownSplitElement : SplitElement
    {
        protected Town town;
        public TownSplitElement(Town town) : base(town.name, town.GetIcon())
        {
            this.town = town;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            town.ShowInfo(panel);
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}