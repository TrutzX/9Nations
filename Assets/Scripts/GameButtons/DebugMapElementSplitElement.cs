using Buildings;
using UI;
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
            panel.AddLabel($"Position: {mapElementInfo.X()}/{mapElementInfo.Y()}");
            panel.AddInput("HP", mapElementInfo.data.hp, (s => { mapElementInfo.data.hp = s; }));
            panel.AddInput("AP", mapElementInfo.data.ap, (s => { mapElementInfo.data.ap = s; }));
            panel.AddButton("Set Finish", mapElementInfo.FinishConstruct);
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}