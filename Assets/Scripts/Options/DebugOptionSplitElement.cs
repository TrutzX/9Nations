using UI;
using UI.Show;
using UnityEngine;

namespace Options
{
    public class DebugOptionSplitElement : SplitElement
    {
        public DebugOptionSplitElement() : base("Debug", SpriteHelper.Load("debug"))
        {
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            panel.AddHeaderLabel("Additional mod folder");
            panel.AddInput("mod.folder", PlayerPrefs.GetString("mod.folder",""), s =>
            {
                PlayerPrefs.SetString("mod.folder",s);
                PlayerPrefs.Save();
            });
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}