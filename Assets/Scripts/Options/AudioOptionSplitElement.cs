using Libraries;
using UI;
using UI.Show;
using UnityEngine;

namespace Options
{
    public class AudioOptionSplitElement : SplitElement
    {
        public AudioOptionSplitElement() : base("Audio", "base:audio") { }

        public override void ShowDetail(PanelBuilder panel)
        {
            LSys.tem.options.GetAllByCategory("audio").ForEach(o => o.AddOption(panel));
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}