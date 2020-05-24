using Buildings;
using Libraries;
using Libraries.FActions;
using Libraries.FActions.General;
using UI;
using UI.Show;
using UnityEngine;

namespace GameButtons
{
    public class DebugMapElementSplitElement : SplitElement
    {
        private MapElementInfo mapElementInfo;
        public DebugMapElementSplitElement(MapElementInfo mapElementInfo) : base("Debug","debug")
        {
            this.mapElementInfo = mapElementInfo;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            panel.AddSubLabel("Position",mapElementInfo.Pos().ToString());
            panel.AddHeaderLabel("HP");
            panel.AddInput("HP", mapElementInfo.data.hp, (s => { mapElementInfo.data.hp = s; }));
            panel.AddHeaderLabel("AP");
            panel.AddInput("AP", mapElementInfo.data.ap, (s => { mapElementInfo.data.ap = s; }));
            panel.AddButton("Set Finish", mapElementInfo.FinishConstruct);

            //display all actions
            foreach (var act in mapElementInfo.data.action.actions)
            {
                act.PerformAction().BuildPanel(new ActionDisplaySettings(panel, mapElementInfo.Player(), mapElementInfo, mapElementInfo.Pos(), act));
            }
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}