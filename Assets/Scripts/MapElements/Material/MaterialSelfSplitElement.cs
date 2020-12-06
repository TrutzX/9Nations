using System;
using System.Collections.Generic;
using Game;
using Libraries;
using Libraries.Buildings;
using Libraries.Res;
using Tools;
using Towns;
using UI;
using UI.Show;
using UnityEngine.UI;

namespace MapElements.Material
{
    public class MaterialSelfSplitElement : SplitElement
    {
        private readonly BaseDataBuildingUnit _build;
        private NVector pos;
        private Dictionary<string, int> _cost;
        private Dictionary<string, int> _costOrg;
        private Resource replace, selected;
        private readonly Town _town;
        private Action<Dictionary<string, int>> perform;

        private Button _one, _ten, max, total, reset;

        public MaterialSelfSplitElement(Resource replace, BaseDataBuildingUnit build, Town town, NVector pos,
            Dictionary<string, int> cost, Action<Dictionary<string, int>> perform) : base(S.T("constructionOwn"),
            build.Sprite())
        {
            _build = build;
            this.pos = pos;
            _cost = cost;
            _costOrg = new Dictionary<string, int>(cost);
            this.replace = replace;
            _town = town;
            this.perform = perform;

            disabled = S.T("constructionOwnReplace", replace.Text(cost[replace.id]));
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            _build.AddImageLabel(panel);
            panel.AddResT("constructionCost", _cost);
            panel.AddHeaderLabelT("general");
            var hpap = CalculatedData.Calc(_build, _cost);
            panel.AddSubLabelT("hp", hpap.hp, "hp");
            panel.AddSubLabelT("ap", hpap.ap, "ap");
            var b = L.b.modifiers[C.BuildRes].CalcText(hpap.buildTime, S.ActPlayer(), pos);
            L.b.res[C.BuildRes].AddSubLabel(panel, b.value, "round", b.display);
            
            panel.AddButtonT("reset", (() =>
            {
                _cost = new Dictionary<string, int>(_costOrg);
                ReCalc(0);
            }));
            
            //what replace?
            if (!_cost.ContainsKey(replace.id))
            {
                panel.AddLabel(S.T("constructionOwnReplaceNothing", _build.Name()));
                return;
            }

            panel.AddHeaderLabel(S.T("constructionOwnReplace", replace.Text(_cost[replace.id])));

            //build dropdown
            List<string> values = new List<string>();
            List<string> titles = new List<string>();
            foreach (Resource r in L.b.res.GetAllByCategory(replace.id))
            {
                if (r.special == false && _town.KnowRes(r.id))
                {
                    values.Add(r.id);
                    titles.Add(r.Text(_town.GetRes(r.id)));
                }
            }

            panel.AddDropdown(values.ToArray(), values[0], titles.ToArray(), s =>
            {
                selected = L.b.res[s];
                UpdateButton();
            });

            _one = panel.AddImageTextButton("", "logo", () => { ReCalc(1); });

            _ten = panel.AddImageTextButton("", "logo", () => { ReCalc(10); });

            max = panel.AddImageTextButton("", "logo", () => { ReCalc(Math.Min(_cost[replace.id],_town.GetRes(selected.id))); });

            total = panel.AddImageTextButton("", "logo", () => { ReCalc(_cost[replace.id]); });

            selected = L.b.res[values[0]];
            UpdateButton();
        }

        private void ReCalc(int amount)
        {
            _cost[replace.id] -= amount;
            disabled = S.T("constructionOwnReplace", replace.Text(_cost[replace.id]));
            if (_cost[replace.id] == 0)
            {
                _cost.Remove(replace.id);
                disabled = null;
            }

            _cost[selected.id] = (_cost.ContainsKey(selected.id) ? _cost[selected.id] : 0) + amount;

            window.Reload();
        }

        private void UpdateButton()
        {
            int all = _cost[replace.id];
            UIHelper.UpdateButtonImage(_one, selected.Sprite());
            UIHelper.UpdateButtonText(_one, S.T("constructionOwnUseRes", selected.Text(1)));
            _one.gameObject.SetActive(all >= 1);
            //one.gameObject.SetActive(true);

            UIHelper.UpdateButtonImage(_ten, selected.Sprite());
            UIHelper.UpdateButtonText(_ten, S.T("constructionOwnUseRes", selected.Text(10)));
            _ten.gameObject.SetActive(all >= 10);

            int has = Math.Min(_cost[replace.id],_town.GetRes(selected.id));

            UIHelper.UpdateButtonImage(max, selected.Sprite());
            UIHelper.UpdateButtonText(max, S.T("constructionOwnUseRes", selected.Text(has)));
            max.gameObject.SetActive(all > 1 && has > 1 && has != 10);

            UIHelper.UpdateButtonImage(total, selected.Sprite());
            UIHelper.UpdateButtonText(total, S.T("constructionOwnUseRes", selected.Text(all)));
            total.gameObject.SetActive(all > 1 && all != 10 && all != has);
        }

        public override void Perform()
        {
            perform.Invoke(_cost);
            OnMapUI.Get().UpdatePanel(pos);
        }
    }
}