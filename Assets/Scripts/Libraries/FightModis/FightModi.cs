using System;
using Buildings;
using Classes;
using Players;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Libraries.FightModis
{
    [Serializable]
    public class FightModi : BaseData
    {
        public int modi;

        public override void ShowLexicon(PanelBuilder panel)
        {
            base.ShowLexicon(panel);
            req.BuildPanel(panel);
        }
    }
}