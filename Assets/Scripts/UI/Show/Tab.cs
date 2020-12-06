using Tools;
using UnityEngine;

namespace UI.Show
{
    public abstract class Tab
    {
        protected readonly string name;
        protected readonly Sprite icon;
        public WindowTabBuilder window;

        protected Tab(string name, string icon) : this(name, SpriteHelper.Load(icon))
        {
        }
        
        protected Tab(string name, Sprite icon)
        {
            this.name = TextHelper.Cap(name);
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