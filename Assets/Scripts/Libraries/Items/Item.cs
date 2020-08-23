using System;
using System.Collections.Generic;
using Buildings;
using Game;
using JetBrains.Annotations;
using Libraries.FActions;
using Tools;
using UI;

namespace Libraries.Items
{

    [Serializable]
    public class Item : BaseData
    {
        public ActionHolders action;

        public Item()
        {
            action = new ActionHolders();
        }
        
        public override void ShowLexicon(PanelBuilder panel)
        {
            base.ShowLexicon(panel);
            req.BuildPanel(panel,"Requirements");
            action.BuildPanelT(new ActionDisplaySettings(panel, null));
        }
        
        public void ShowOwn(PanelBuilder panel, MapElementInfo info)
        {
            base.ShowLexicon(panel);
            req.BuildPanel(panel,"Requirements", info, info.Pos());
            action.BuildPanelT(new ActionDisplaySettings(panel, S.ActPlayer(), info, info.Pos(), null));
        }
    }
}