using UnityEngine;

namespace UI.Show
{
    public abstract class PanelBuilderTab : Tab
    {
        public PanelBuilderTab(string name, string icon) : base(name, icon)
        {
        }

        public override void Show(Transform parent)
        {
            BuildPanel(PanelBuilder.Create(parent));
        }

        public abstract void BuildPanel(PanelBuilder panel);
    }
}