using Buildings;
using DataTypes;
using UI;
using UnityEngine;

namespace MapActions
{
    public class DebugMapElementSplitElement : SplitElement
    {
        private MapElementInfo mapElementInfo;
        public DebugMapElementSplitElement(MapElementInfo mapElementInfo) : base("Debug","ui:debug")
        {
            this.mapElementInfo = mapElementInfo;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            panel.AddLabel($"Position: {mapElementInfo.X()}/{mapElementInfo.Y()}");
            panel.AddInput("HP", mapElementInfo.data.hp, (s => { mapElementInfo.data.hp = s; }));
            panel.AddInput("AP", mapElementInfo.data.ap, (s => { mapElementInfo.data.ap = s; }));
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}