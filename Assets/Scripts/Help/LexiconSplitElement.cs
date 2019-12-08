using Libraries;
using UI;
using UnityEngine;

namespace Help
{
    public class LexiconSplitElement : SplitElement
    {
        private BaseData data;
        
        public LexiconSplitElement(BaseData data) : base(data.Name, data.Sprite())
        {
            this.data = data;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            data.ShowDetail(panel);
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}