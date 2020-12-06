using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Show
{
    public abstract class SplitElement
    {
        public string title;
        public string audioSwitch;
        public string audioPerform;
        public Sprite icon;
        public Button button;
        public WindowBuilderSplit window;
        public Tab tab;
        
        /// <summary>
        /// content = error message to display
        /// </summary>
        public string disabled;


        protected SplitElement(string title, Sprite icon)
        {
            this.title = TextHelper.Cap(title);
            this.icon = icon;
            audioSwitch = "switch";
            audioPerform = "click";
        }
            
        protected SplitElement(string title, string icon) : this (title, SpriteHelper.Load(icon)){}

        public abstract void ShowDetail(PanelBuilder panel);

        public abstract void Perform();
    }
}