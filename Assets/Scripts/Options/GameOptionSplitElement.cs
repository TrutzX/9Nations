using UI;
using UnityEngine;

namespace Options
{
    public class GameOptionSplitElement : SplitElement
    {
        public GameOptionSplitElement() : base("Game", SpriteHelper.LoadIcon("logo"))
        {
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            Data.features.autosave.AddOption(panel);
            Data.features.centermouse.AddOption(panel);
            Data.features.debug.AddOption(panel);
                
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}