using Players;
using UI;
using UnityEngine;

namespace Classes.Elements
{
    public abstract class BaseElementRun : ScriptableObject
    {
        public string id;

        protected BaseElementRun(string id)
        {
            this.id = id;
        }

        public abstract void Develop(Player player);
        
        public virtual void ShowDetail(PanelBuilder panel) { }
    }
}