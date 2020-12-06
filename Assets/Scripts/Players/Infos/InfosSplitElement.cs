
using Game;
using Libraries.Rounds;
using UI;
using UI.Show;
using UnityEngine;

namespace Players.Infos
{
    public class InfosSplitElement: SplitElement
    {
        private BaseInfoMgmt _infoMgmt;
        
        public InfosSplitElement(BaseInfoMgmt infoMgmt) : base(S.T("notifications"), "info")
        {
            _infoMgmt = infoMgmt;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            int lastRound = -1;
            //show all
            foreach (Info key in _infoMgmt.infos)
            {
                //new round?
                if (key.round != lastRound)
                {
                    lastRound = key.round;
                    panel.AddHeaderLabel(S.Round().GetRoundString(key.round));
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