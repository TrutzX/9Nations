using Libraries;
using LoadSave;
using Players;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Classes.GameButtons
{
    public class TitleGameButtonRun : BaseGameButtonRun
    {
        public TitleGameButtonRun() : base ("endgame") { }

        protected override void Run(Player player)
        {
            
            WindowPanelBuilder wpb = WindowPanelBuilder.Create("Exit game");
            var saveonexit = LSys.tem.options["saveonexit"];
            saveonexit.AddOption(wpb.panel);
            wpb.panel.AddButton("to main menu", () =>
            {
                if (saveonexit.Bool())
                    LoadSaveMgmt.UpdateSave("quicksave", "quick save");
                SceneManager.LoadScene(0, LoadSceneMode.Single);
            });
            
            wpb.panel.AddButton("exit game", () =>
            {
                if (saveonexit.Bool())
                    LoadSaveMgmt.UpdateSave("quicksave", "quick save");
                Application.Quit();
            });
            wpb.AddClose();
            wpb.Finish();
            
        }
    }
}