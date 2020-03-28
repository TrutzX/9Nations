using System;
using Buildings;
using UI;
using UI.Show;
using UnityEngine;

namespace Libraries.FActions.General
{
    public class ActionDisplaySplitElement : SplitElement
    {
        private MapElementInfo info;
        
        public ActionDisplaySplitElement(MapElementInfo info) : base("Actions", L.b.actions.Sprite())
        {
            this.info = info;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            foreach (var action in info.data.action.actions)
            {
                action.PerformAction().BuildPanel(new ActionDisplaySettings(panel, info.Player(),info,info.Pos(),action));
            }
        }

        public override void Perform()
        {
            throw new System.NotImplementedException();
        }
    }
}