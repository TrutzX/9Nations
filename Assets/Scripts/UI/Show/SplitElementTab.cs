

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Show
{
    public class SplitElementTab : Tab, ISplitManager
    {
        private List<SplitElement> elements;
        public string selectButtonText;
        public PanelBuilderSplit pbs;

        public SplitElementTab(string name, string icon, string button = null) : this(name, SpriteHelper.Load(icon), button)
        {
        }
        
        public SplitElementTab(string name, Sprite icon, string button = null) : base(name, icon)
        {
            elements = new List<SplitElement>();
            selectButtonText = button;
        }

        public void Add(SplitElement ele, bool first = false)
        {
            if (first)
                elements.Insert(0, ele);
            else
                elements.Add(ele);
            
            ele.tab = this;
            if (pbs != null) pbs.AddElement(ele);
        }

        public int Count()
        {
            return elements.Count;
        }

        public void Close()
        {
            window.CloseWindow();
        }

        public override void Show(Transform parent)
        {
            //build panel
            pbs = PanelBuilderSplit.Create(parent, window, elements, selectButtonText);
        }
    }
}