using Libraries;
using Tools;
using UI;
using UI.Show;
using UnityEngine;

namespace Help
{
    public class LexiconSplitElement : SplitElement
    {
        private readonly BaseData _data;
        
        public LexiconSplitElement(BaseData data) : base(data.name, data.Sprite())
        {
            _data = data;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            _data.ShowLexicon(panel);
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}