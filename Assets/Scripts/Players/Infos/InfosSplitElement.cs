using DataTypes;
using UI;
using UnityEngine;

namespace Players.Infos
{
    public class InfosSplitElement: SplitElement
    {
        public InfosSplitElement() : base("Notifications", SpriteHelper.Load("base:info"))
        {
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            int lastRound = -1;
            //show all
            foreach (Info key in PlayerMgmt.ActPlayer().info.infos)
            {
                //new round?
                if (key.round != lastRound)
                {
                    lastRound = key.round;
                    panel.AddHeaderLabel(RoundMgmt.Get().GetRoundString(key.round));
                }
                
                key.AddToPanel(panel);
            }
            
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}