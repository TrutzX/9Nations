using System;
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
            int i = town.GetRes("inhabitant");
            
            panel.AddHeaderLabel("General");
            panel.AddRes("inhabitant",$"{i}/{n.maxInhabitants}");
            panel.AddImageLabel($"{town.GetRes("worker")} free worker", L.b.res["worker"].Icon);
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

                panel.AddSubLabel(usage.name,$"{a} from {town.GetRes(usage.id)}", usage.Icon);
            }
            
            town.ShowRes(panel);
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}