using System.IO;
using Libraries;
using ModIO;
using UI;
using UI.Show;
using UnityEngine;

namespace Options
{
    public class StatisticSplitElement : SplitElement
    {
        public StatisticSplitElement() : base("Statistic", "Icons/base:statistic") { }

        public override void ShowDetail(PanelBuilder panel)
        {
            panel.AddHeaderLabel("General");
            panel.AddSubLabel("Version",Application.version);
            panel.AddSubLabel("System",Application.platform.ToString());

            panel.AddHeaderLabel("Library");
            foreach (var m in L.b.mgmts.Values)
            {
                panel.AddSubLabel(m.Name(),$"{m.Length} elements",m.Sprite());
            }

            panel.AddHeaderLabel("Mods");
            foreach (var mod in ModManager.GetInstalledModDirectories(true))
            {
                panel.AddLabel(new DirectoryInfo(mod).Name);
            }
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}