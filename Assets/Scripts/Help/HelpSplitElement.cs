using UI;
using UnityEngine;

namespace Help
{
    public class HelpSplitElement : WindowBuilderSplit.SplitElement
    {
        private DataTypes.Help help;
        
        public HelpSplitElement(string help) : base("Help", SpriteHelper.LoadIcon("magic:lexicon"))
        {
            this.help = Data.help[help];
        }
        public HelpSplitElement(DataTypes.Help help) : base(help.name, SpriteHelper.LoadIcon(help.icon))
        {
            this.help = help;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            panel.AddHeaderLabel(help.name);
            panel.AddDesc(help.text);
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}