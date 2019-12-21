using UnityEngine;

namespace UI
{
    public abstract class Tab
    {

        protected string name;
        protected Sprite icon;
        public WindowTabBuilder window;
        
        
        protected Tab(string name, string icon) : this(name, SpriteHelper.Load(icon))
        {
        }
        
        protected Tab(string name, Sprite icon)
        {
            this.name = name;
            this.icon = icon;
        }

        public string Name()
        {
            return name;
        }

        public Sprite Icon()
        {
            return icon;
        }
        public abstract void Show(Transform parent);

    }
}