using UI;
using UI.Show;
using UnityEngine;

namespace Options
{
    public class GameOptionSplitElement : SplitElement
    {
        public GameOptionSplitElement() : base("Game", SpriteHelper.Load("logo"))
        {
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            Data.features.autosave.AddOption(panel);
            Data.features.centermouse.AddOption(panel);
            Data.features.debug.AddOption(panel);
            Data.features.showAction.AddOption(panel);
                
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}