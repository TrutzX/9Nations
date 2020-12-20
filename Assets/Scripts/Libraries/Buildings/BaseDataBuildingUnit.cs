using System;
using System.Collections.Generic;
using Buildings;
using Game;
using Libraries.FActions;
using Libraries.FActions.General;
using MapElements;
using Players;
using reqs;
using Tools;
using UI;
using UnityEngine;

namespace Libraries.Buildings
{
    [Serializable]
    public class BaseDataBuildingUnit : BaseData
    {
        public int buildTime;
        public int visibilityRange;
        public int hp;
        public int ap;
        public int atk;
        public int def;
        public int damMin;
        public int damMax;
        public Dictionary<string, int> cost;
        public Dictionary<string, string> modi;
        public ActionHolders action;

        public BaseDataBuildingUnit()
        {
            cost = new Dictionary<string, int>();
            modi = new Dictionary<string, string>();
            action = new ActionHolders();

            visibilityRange = 1;
            hp = 5;
            ap = 5;
        }

        public override void ShowLexicon(PanelBuilder panel)
        {
            base.ShowLexicon(panel);
            L.b.res[C.BuildRes].AddSubLabel(panel,buildTime,"round");
            L.b.modifiers[C.ViewModi].AddSubLabel(panel,visibilityRange,"field");
            ShowWorker(panel);
            panel.AddResT("constructionCost",cost);
            req.BuildPanel(panel, S.T("constructionReq"));
            panel.AddModi(modi);
            
            ActionDisplaySettings sett = new ActionDisplaySettings(panel,null);
            sett.compact = true;
            action.BuildPanelT(sett);
        }
        
        public override void ShowLexicon(PanelBuilder panel, MapElementInfo onMap, NVector pos)
        {
            if (onMap == null)
            {
                ShowIntern(panel, null, pos);

                ActionDisplaySettings sett = new ActionDisplaySettings(panel, S.ActPlayer(), null, pos,null);
                sett.compact = true;
                action.BuildPanelT(sett);
                return;
            }
            
            if (!onMap.Owner(S.ActPlayerID()))
            {
                ShowLexicon(panel);
                return;
            }
            
            pos = onMap.Pos();
            ShowIntern(panel, onMap, pos);

            onMap.data.action.BuildPanelT(new ActionDisplaySettings(panel, onMap.Player(), onMap, pos, null));
        }

        private void ShowIntern(PanelBuilder panel, MapElementInfo onMap, NVector pos)
        {
            base.ShowLexicon(panel);
            var b = L.b.modifiers[C.BuildRes].CalcText(buildTime, S.ActPlayer(), pos);
            L.b.res[C.BuildRes].AddSubLabel(panel, b.value, "round", b.display);

            int _view = onMap == null ? visibilityRange : onMap.data.visibilityRange;
            int _atk = onMap == null ? atk : onMap.data.atk;
            int _def = onMap == null ? def : onMap.data.def;
            
            var v = L.b.modifiers[C.ViewModi].CalcText(_view, S.ActPlayer(), pos);
            L.b.modifiers[C.ViewModi].AddSubLabel(panel, v.value, "field", v.display);
            ShowWorker(panel);

            if (_atk > 0 || _def > 0)
                panel.AddHeaderLabelT("fight");
            panel.AddSubLabelT("atk", _atk, "atk");
            panel.AddSubLabelT("def", _def, "def");
            
            panel.AddResT("constructionCost", cost);
            req.BuildPanel(panel, S.T("constructionReq"), null, pos);
            panel.AddModi(modi);
        }

        protected virtual void ShowWorker(PanelBuilder panel)
        {
        }
    }
}
