using System;
using System.Collections.Generic;
using Buildings;
using Game;
using Libraries;
using Libraries.Buildings;
using Libraries.PlayerOptions;
using Libraries.Res;
using MapElements;
using Players;
using Tools;
using Towns;
using UI;
using UI.Show;
using UnityEngine;
using UnityEngine.UI;

namespace Classes.Actions.Addons
{
    public class MaterialSelfSplitElement : SplitElement
    {
        private BaseDataBuildingUnit build;
        private NVector pos;
        private Dictionary<string, int> cost;
        private Resource replace, selected;
        private Town town;
        private Action<Dictionary<string, int>> perform;

        private Button one, ten, max, total;
        
        public MaterialSelfSplitElement(Resource replace, BaseDataBuildingUnit build, Town town, NVector pos, Dictionary<string, int> cost, Action<Dictionary<string, int>> perform) : base(S.T("constructionOwn"), build.Sprite())
        {
            this.build = build;
            this.pos = pos;
            this.cost = cost;
            this.replace = replace;
            this.town = town;
            this.perform = perform;

            disabled = S.T("constructionOwnReplace", replace.Text(cost[replace.id]));
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            build.AddImageLabel(panel);
            panel.AddResT("constructionCost",cost);
            panel.AddHeaderLabelT("general");
            var hpap = CalculatedData.Calc(build, cost);
            panel.AddSubLabelT("hp",hpap.hp,"hp");
            panel.AddSubLabelT("ap",hpap.ap,"ap");
            var b = L.b.modifiers[C.BuildRes].CalcText(hpap.buildTime, S.ActPlayer(), pos);
            L.b.res[C.BuildRes].AddSubLabel(panel, b.value, "round", b.display);
            
            //what replace?
            if (!cost.ContainsKey(replace.id))
            {
                panel.AddLabel(S.T("constructionOwnReplaceNothing",build.Name()));
                return;
            }

            panel.AddHeaderLabel(S.T("constructionOwnReplace",replace.Text(cost[replace.id])));
            
            //build dropdown
            List<string> values = new List<string>();
            List<string> titles = new List<string>();
            foreach (Resource r in L.b.res.GetAllByCategory(replace.id))
            {
                if (r.special == false && town.KnowRes(r.id))
                {
                    values.Add(r.id);
                    titles.Add(r.Text(town.GetRes(r.id)));
                }
            }

            panel.AddDropdown(values.ToArray(), values[0], titles.ToArray(), s =>
            {
                selected = L.b.res[s];
                UpdateButton();
            });

            one = panel.AddImageTextButton("", "logo", () =>
            {
                ReCalc(1);
            });

            ten = panel.AddImageTextButton("", "logo", () =>
            {
                ReCalc(10);
            });

            max = panel.AddImageTextButton("", "logo", () =>
            {
                ReCalc(town.GetRes(selected.id));
            });

            total = panel.AddImageTextButton("", "logo", () =>
            {
                ReCalc(cost[replace.id]);
            });
            
            selected = L.b.res[values[0]];
            UpdateButton();

        }

        private void ReCalc(int amount)
        {
            cost[replace.id] -= amount;
            disabled = S.T("constructionOwnReplace", replace.Text(cost[replace.id]));
            if (cost[replace.id] == 0)
            {
                cost.Remove(replace.id);
                disabled = null;
            }

            cost[selected.id] = (cost.ContainsKey(selected.id) ? cost[selected.id] : 0) + amount;
            
            window.Reload();
        }
        
        private void UpdateButton()
        {
            int all = cost[replace.id];
            UIHelper.UpdateButtonImage(one, selected.Sprite());
            UIHelper.UpdateButtonText(one, S.T("constructionOwnUseRes",selected.Text(1)));
            //one.gameObject.SetActive(true);

                UIHelper.UpdateButtonImage(ten, selected.Sprite());
                UIHelper.UpdateButtonText(ten, S.T("constructionOwnUseRes",selected.Text(10)));
                ten.gameObject.SetActive(all >= 10);

            int has = town.GetRes(selected.id);
            
                UIHelper.UpdateButtonImage(max, selected.Sprite());
                UIHelper.UpdateButtonText(max, S.T("constructionOwnUseRes",selected.Text(has)));
                max.gameObject.SetActive(all > 1 && has > 1 && has != 10);

                UIHelper.UpdateButtonImage(total, selected.Sprite());
                UIHelper.UpdateButtonText(total, S.T("constructionOwnUseRes",selected.Text(all)));
                total.gameObject.SetActive(all > 1 && all != 10 && all != has);
        }
        
        public override void Perform()
        {
            perform.Invoke(cost);
            OnMapUI.Get().UpdatePanel(pos);
        }
    }
}