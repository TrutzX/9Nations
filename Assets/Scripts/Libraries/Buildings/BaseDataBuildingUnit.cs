using System;
using System.Collections.Generic;
using Buildings;
using Libraries.FActions;
using Libraries.FActions.General;
using Players;
using reqs;
using Tools;
using UI;

namespace Libraries.Buildings
{
    [Serializable]
    public class BaseDataBuildingUnit : BaseData
    {
        public int buildTime;
        public int visibilityRange;
        public string category;
        public int hp;
        public int ap;
        public int atk;
        public int def;
        public int damMin;
        public int damMax;
        public Dictionary<string, int> cost;
        public Dictionary<string, string> modi;
        [Obsolete] public Dictionary<string, string> oActions;
        public ActionHolders action;

        public BaseDataBuildingUnit()
        {
            cost = new Dictionary<string, int>();
            modi = new Dictionary<string, string>();
            action = new ActionHolders();
            
            oActions = new Dictionary<string, string>();

            visibilityRange = 1;
            hp = 5;
        }

        public override void ShowLexicon(PanelBuilder panel)
        {
            base.ShowLexicon(panel);
            panel.AddSubLabel("Build time",$"{buildTime} rounds","round");
            panel.AddSubLabel("View Radius",$"{visibilityRange} fields","view");
            panel.AddRes("Cost for construction",cost);
            req.BuildPanel(panel, "Requirement for construction");
            
            ActionDisplaySettings sett = new ActionDisplaySettings(panel,null);
            sett.compact = true;
            action.BuildPanel(panel, "Actions", sett);
        }

        public void ShowBuild(PanelBuilder panel, NVector pos)
        {
            base.ShowLexicon(panel);
            panel.AddSubLabel("Build time",L.b.modifiers["build"].CalcText(visibilityRange, PlayerMgmt.ActPlayer(), pos, "rounds"),"round");
            panel.AddSubLabel("View Radius",L.b.modifiers["view"].CalcText(visibilityRange, PlayerMgmt.ActPlayer(), pos, "fields"),"view");
            panel.AddRes("Cost for construction",cost);
            req.BuildPanel(panel, "Requirement for construction", null, pos);
            
            ActionDisplaySettings sett = new ActionDisplaySettings(panel, PlayerMgmt.ActPlayer(), null, pos,null);
            sett.compact = true;
            action.BuildPanel(panel, "Actions", sett);
        }
        
        public void ShowOwn(PanelBuilder panel, MapElementInfo onMap)
        {
            if (!onMap.Owner(PlayerMgmt.ActPlayerID()))
            {
                ShowLexicon(panel);
                return;
            }
            
            NVector pos = onMap.Pos();

            base.ShowLexicon(panel);
            panel.AddSubLabel("Build time",L.b.modifiers["build"].CalcText(visibilityRange, PlayerMgmt.ActPlayer(), pos, "rounds"),"round");
            panel.AddSubLabel("View Radius",L.b.modifiers["view"].CalcText(visibilityRange, PlayerMgmt.ActPlayer(), pos, "fields"),"view");
            panel.AddRes("Cost for construction",cost);
            req.BuildPanel(panel,"Requirement for construction", onMap, pos);

            onMap.data.action.BuildPanel(panel, "Actions", new ActionDisplaySettings(panel, onMap.Player(), onMap, pos, null));

        }
    }
}
