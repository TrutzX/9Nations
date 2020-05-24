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
        public TownResSplitElement(Town town = null) : base("Resources", "res")
        {
            this.town = town;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            var n = town.MaxInhabitantsAndWorker();
            int i = town.GetRes(C.Inhabitant);
            
            panel.AddHeaderLabelT("general");
            L.b.res[C.Inhabitant].AddImageLabel(panel, $"{i}/{n.maxInhabitants}");
            L.b.res[C.Worker].AddImageLabel(panel, $"{town.GetRes("worker")} free worker");
            panel.AddSubLabel("Productivity",town.modi["produce"],"res");
            if (town.usageMess != null) panel.AddSubLabel("Status",town.usageMess);
            
            panel.AddHeaderLabel("Usages per round");
            //use usage
            foreach (Usage usage in L.b.usages.Values())
            {
                //can use?
                if (!usage.req.Check(town.Player())) continue;

                int a = (int) Math.Round(i * usage.rate);
                
                if (a == 0) continue;

                panel.AddSubLabel(usage.Name(),$"{a} from {town.GetRes(usage.id)}", usage.Icon);
            }
            
            town.ShowRes(panel);
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}