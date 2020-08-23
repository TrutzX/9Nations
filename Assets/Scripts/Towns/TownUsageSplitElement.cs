using System;
using Game;
using Libraries;
using Libraries.Usages;
using UI;
using UI.Show;
using UnityEngine;

namespace Towns
{
    public class TownUsageSplitElement : SplitElement
    {
        protected Town town;
        public TownUsageSplitElement(Town town) : base(S.T("usage"), "usage")
        {
            this.town = town;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
           ShowInhabitants(panel);

            //usage?
            if (!L.b.gameOptions["usageTown"].Bool())
            {
                return;
            }
            
            var inh = town.GetRes(C.Inhabitant);

            panel.AddSubLabel("Productivity",town.modi["produce"],"res");
            if (town.usageMess != null) panel.AddSubLabel("Status",town.usageMess);

            int usages = 0;
            panel.AddHeaderLabelT("usageRound");
            
            //find usage count
            foreach (Usage usage in L.b.usages.Values())
            {
                //can use?
                if (!usage.req.Check(town.Player()))
                {
                    continue;
                }

                usages += usage.factor;
            }
            int usageMax = usages;
            
            
            //use usage
            foreach (Usage usage in L.b.usages.Values())
            {
                //can use?
                if (!usage.req.Check(town.Player())) continue;

                var r = L.b.res[usage.id];
                
                int amount = (int) Math.Round(inh * usage.rate);
                bool comb = string.IsNullOrEmpty(r.combine);
                int hasAmount = comb?town.GetCombineRes(usage.id):town.GetRes(usage.id);
                
                panel.AddSubLabel(usage.Name(),S.T("usageRoundRes",amount, hasAmount), usage.Icon);
                //need res?
                if (amount >= 0 || amount * -1 <= hasAmount) usages -= 1;
            }
            
            //worker
            //res

            panel.AddHeaderLabelT("usageRoundResult");
            panel.AddImageLabel(S.T("usageRoundResultStatus", usageMax-usages, usageMax-town.level), "usage");
            panel.AddImageLabel(town.level >= usages?S.T("usageRoundResultStatusFulfilled"):S.T("usageRoundResultStatusNotFulfilled"), town.level >= usages);
        }

        private void ShowInhabitants(PanelBuilder panel)
        {
            var n = town.MaxInhabitantsAndWorker();
            int inh = town.GetRes(C.Inhabitant);

            panel.AddHeaderLabelT("general");
            L.b.res[C.Worker].AddSubLabel(panel, town.GetRes(C.Worker), "free worker");
            if (!L.b.gameOptions["inhabitantGrow"].Bool())
            {
                L.b.res[C.Inhabitant].AddImageLabel(panel, inh);
            }
            else
            {
                L.b.res[C.Inhabitant].AddImageLabel(panel, $"{inh}/{n.maxInhabitants}");
                panel.AddHeaderLabel("Growth");
                L.b.res[C.Worker].AddSubLabel(panel, town.level, "per round");
            }
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}