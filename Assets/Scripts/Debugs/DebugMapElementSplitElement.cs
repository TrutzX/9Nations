using Libraries;
using Libraries.FActions;
using MapElements;
using UI;
using UI.Show;
using UnityEngine;

namespace Debugs
{
    public class DebugMapElementSplitElement : SplitElement
    {
        private readonly MapElementInfo _mapElementInfo;
        public DebugMapElementSplitElement(MapElementInfo mapElementInfo) : base("Debug","debug")
        {
            this._mapElementInfo = mapElementInfo;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            panel.AddSubLabel("Position",_mapElementInfo.Pos().ToString());
            panel.AddHeaderLabel("HP");
            panel.AddInput("HP", _mapElementInfo.data.hp, (s => { _mapElementInfo.data.hp = s; }));
            panel.AddHeaderLabel("AP");
            panel.AddInput("AP", _mapElementInfo.data.ap, (s => { _mapElementInfo.data.ap = s; }));
            panel.AddButton("Set Finish", _mapElementInfo.FinishConstruct);

            //display all actions
            foreach (var act in _mapElementInfo.data.action.actions)
            {
                act.PerformAction().BuildPanel(new ActionDisplaySettings(panel, _mapElementInfo.Player(), _mapElementInfo, _mapElementInfo.Pos(), act));
            }
            
            //display modis
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}