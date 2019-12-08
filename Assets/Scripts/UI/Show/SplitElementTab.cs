

using System.Collections.Generic;
using UnityEngine;

namespace UI.Show
{
    public class SplitElementTab : Tab
    {
        private List<SplitElement> elements;

        public SplitElementTab(string name, string icon) : base(name, icon)
        {
            elements = new List<SplitElement>();
        }
        
        public SplitElementTab(string name, Sprite icon) : base(name, icon)
        {
            elements = new List<SplitElement>();
        }

        public void Add(SplitElement ele)
        {
            elements.Add(ele);
        }

        public override void Show(Transform parent)
        {
            //build panel
            PanelBuilderSplit.Create(parent, elements);
        }
    }
}