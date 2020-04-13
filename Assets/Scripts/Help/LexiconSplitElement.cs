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
        
        public LexiconSplitElement(BaseData data) : this(data, data.name, data.Sprite())
        {
        }
        
        public LexiconSplitElement(BaseData data, string name, Sprite icon) : base(name, icon)
        {
            _data = data;
            disabled = "It is only the help, so it is disabled.";
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