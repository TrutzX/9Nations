using UI;
using UI.Show;
using UnityEngine;

namespace Help
{
    public class HelpSplitElement : SplitElement
    {
        private DataTypes.Help help;
        
        public HelpSplitElement(string help) : base("Help", SpriteHelper.Load("magic:lexicon"))
        {
            this.help = Data.help[help];
            disabled = "It is only the help, so it is disabled.";
        }
        public HelpSplitElement(DataTypes.Help help) : base(help.name, SpriteHelper.Load(help.icon))
        {
            this.help = help;
            disabled = "It is only the help, so it is disabled.";
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