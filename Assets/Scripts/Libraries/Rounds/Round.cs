using System;
using System.Collections.Generic;
using Game;
using Tools;
using UI;

namespace Libraries.Rounds
{
    [Serializable]
    public class Round : BaseData
    {
        public Dictionary<string, string> modi;
        public string season;
        public string daytime;

        public Round()
        {
            modi = new Dictionary<string, string>();
        }
        
        public override void ShowLexicon(PanelBuilder panel)
        {
            base.ShowLexicon(panel);
            panel.AddSubLabel("Daytime",daytime);
            panel.AddSubLabel("Season",season);
            panel.AddModi("Modifiers",modi);
            if (GameHelper.IsGame())
            {
                panel.AddHeaderLabel("Actual");
                panel.AddLabel(GameMgmt.Get().gameRound.GetRoundString());
            }
        }
    }
}
