using System;
using System.Collections.Generic;
using Game;
using UI;

namespace Libraries.Res
{
    [Serializable]
    public class Resource : BaseData
    {
        public float price;
        public float weight;
        public float hp;
        public float ap;
        public string combine;
        public bool special;
        public List<string> overlay;
        public Dictionary<string, string> modi;

        public Resource()
        {
            overlay = new List<string>();
            modi = new Dictionary<string, string>();
        }

        public override void AddImageLabel(PanelBuilder panel, int count)
        {
            panel.AddImageLabel(Text(count),Sprite());
        }

        public void AddImageLabel(PanelBuilder panel, string count)
        {
            panel.AddImageLabel(S.T("resourceCount", S.T(id,2), count),Sprite());
        }

        public string Text(int count)
        {
            return S.T("resourceCount", S.T(id,count), count);
        }

        public override void ShowLexicon(PanelBuilder panel)
        {
            base.ShowLexicon(panel);

            if (category == "construction")
            {
                panel.AddHeaderLabelT("construction");
                panel.AddSubLabelT("hp",hp,"hp");
                panel.AddSubLabelT("ap",ap,"ap");
                panel.AddModi(modi);
            }

            if (category == "food")
            {
                panel.AddHeaderLabelT("food");
                panel.AddSubLabelT("hp",hp,"hp");
                panel.AddSubLabelT("ap",ap,"ap");
                panel.AddModi(modi);
            }
        }
    }
}
