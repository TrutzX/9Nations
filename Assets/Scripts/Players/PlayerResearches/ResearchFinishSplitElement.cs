using Libraries;
using Libraries.Researches;
using UI;
using UI.Show;
using UnityEngine;

namespace Players.PlayerResearches
{
    public class ResearchFinishSplitElement : SplitElement
    {
        
        public ResearchFinishSplitElement() : base("Finish", SpriteHelper.Load("yes"))
        {
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            panel.AddHeaderLabel("Finish researches");
            foreach (Research r in L.b.researches.Values())
            {
                if (PlayerMgmt.ActPlayer().research.IsFinish(r.id))
                    panel.AddImageLabel(r.name, r.Icon);
            }

            if (panel.Count() == 1)
            {
                panel.AddImageLabel("No finish research.", "no");
            }
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}