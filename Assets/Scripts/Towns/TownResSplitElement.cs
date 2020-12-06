using System;
using Game;
using Libraries;
using Libraries.Usages;
using UI;
using UI.Show;
using UnityEngine;

namespace Towns
{
    public class TownResSplitElement : SplitElement
    {
        protected Town town;
        public TownResSplitElement(Town town) : base("Resources", "res")
        {
            this.town = town;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            //res
            town.ShowCombineRes(panel);
            
            //res
            town.ShowRes(panel);
            
            //show grow last round
            bool found = false;
            foreach (var r in town.resStatistic.GetLast(ResType.Produce))
            {
                var i = L.b.res[r.Key];
                if (i.special || i.Hidden) continue;
                
                if (!found)
                {
                    found = true;
                    panel.AddHeaderLabelT("townProduceLastRound");
                }
                i.AddImageLabel(panel, (int) r.Value);
            }
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}