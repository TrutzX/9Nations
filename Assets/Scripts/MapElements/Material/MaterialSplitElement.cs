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
using UI;
using UI.Show;
using UnityEngine;

namespace Classes.Actions.Addons
{
    public class MaterialSplitElement : SplitElement
    {
        private BaseDataBuildingUnit build;
        private NVector pos;
        private Dictionary<string, int> cost;
        private Action<Dictionary<string, int>> perform;
        
        public MaterialSplitElement(Resource res, BaseDataBuildingUnit build, NVector pos, Dictionary<string, int> cost, Action<Dictionary<string, int>> perform) : base(res.Name(), res.Sprite())
        {
            this.build = build;
            this.pos = pos;
            this.cost = cost;
            this.perform = perform;
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
        }

        public override void Perform()
        {
            window.Close();
            perform.Invoke(cost);
            
            OnMapUI.Get().UpdatePanel(pos);
        }
    }
}