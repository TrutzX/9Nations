using System;
using System.Linq;
using UI;
using UI.Show;
using UnityEngine;

namespace Libraries.Inputs
{
    public class InputOptionSplitElement: SplitElement
    {
        public InputOptionSplitElement() : base("Keys", "key")
        {
        }

        public override void ShowDetail(PanelBuilder panel)
        {

            panel.AddHeaderLabel("General");
            LSys.tem.inputs.GetAllByCategory("general").ForEach(i => i.BuildPanel(panel));

            panel.AddHeaderLabel("Actions");
            LSys.tem.inputs.GetAllByCategory("unit").ForEach(i => i.BuildPanel(panel));

            panel.AddHeaderLabel("Camera");
            LSys.tem.inputs.GetAllByCategory("camera").ForEach(i => i.BuildPanel(panel));
                
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}