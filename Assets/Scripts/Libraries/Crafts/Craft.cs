using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Libraries.Crafts
{

    [Serializable]
    public class Craft : BaseData
    {
        public Dictionary<string, int> res;

        public Craft()
        {
            res = new Dictionary<string, int>();
        }
        
        public override void ShowLexicon(PanelBuilder panel)
        {
            base.ShowLexicon(panel);
            panel.AddResT("craftReceipt", res);
            req.BuildPanel(panel,"Requirements");
        }
    }
}