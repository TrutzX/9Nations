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
        public bool special;
        public List<string> overlay;

        public Resource()
        {
            overlay = new List<string>();
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
    }
}
