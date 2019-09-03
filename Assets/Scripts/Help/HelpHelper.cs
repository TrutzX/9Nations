using DataTypes;
using UI;
using UnityEngine;

namespace DefaultNamespace
{
    public class HelpHelper
    {
        public static void ShowHelpWindow()
        {
            
            //load buildings
            WindowBuilderSplit b = WindowBuilderSplit.Create("Help window",null);

            foreach(Help h in Data.help)
            {
                b.AddElement(new HelpSplitElement(h));
            }

            b.Finish();
        }

        public class HelpSplitElement : WindowBuilderSplit.SplitElement
        {
            protected Help help;
            public HelpSplitElement(Help help) : base(help.name, SpriteHelper.LoadIcon(help.icon))
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
}