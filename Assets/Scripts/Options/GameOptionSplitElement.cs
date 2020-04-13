using Libraries;
using UI;
using UI.Show;
using UnityEngine;

namespace Options
{
    public class GameOptionSplitElement : SplitElement
    {
        public GameOptionSplitElement() : base("Game", "logo")
        {
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            LSys.tem.options.GetAllByCategory("game").ForEach(o => o.AddOption(panel));
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}