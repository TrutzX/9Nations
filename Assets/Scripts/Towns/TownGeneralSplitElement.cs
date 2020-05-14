using UI;
using UI.Show;
using UnityEngine;

namespace Towns
{
    public class TownGeneralSplitElement : SplitElement
    {
        protected Town town;
        public TownGeneralSplitElement(Town town) : base(town.name, town.GetIcon())
        {
            this.town = town;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            UIHelper.UpdateButtonImageColor(button, town.Coat().color);
            town.ShowInfo(panel);
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}