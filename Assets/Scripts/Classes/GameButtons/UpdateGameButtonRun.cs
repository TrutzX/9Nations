using Players;
using Tools;
using UI;
using UnityEngine;

namespace Classes.GameButtons
{
    public class UpdateGameButtonRun : BaseGameButtonRun
    {
        public UpdateGameButtonRun() : base ("update") { }

        protected override void Run(Player player)
        {
            string[] s = SplitHelper.Separator(PlayerPrefs.GetString("update.txt", "false"));

            if (s[0].Equals("false")) return;
            
            WindowPanelBuilder w = WindowPanelBuilder.Create($"Update to {s[1]}");
            w.panel.AddLabel(s[2]);
            w.panel.AddButton($"Download {s[1]}", (() => Application.OpenURL("http://9nations.de/download")));
            w.AddClose();
            w.Finish();
        }
    }
}