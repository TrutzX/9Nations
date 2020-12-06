using Game;
using Libraries;
using LoadSave;
using Options;
using Players;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Classes.GameButtons
{
    public class MainMenuGameButtonRun : BaseGameButtonRun
    {
        public MainMenuGameButtonRun() : base ("mainmenu") { }

        protected override void Run(Player player)
        {
            //create it
            WindowPanelBuilder win = WindowPanelBuilder.Create("Main menu");
            L.b.gameButtons.BuildMenu(S.ActPlayer(), "game", null, true, win.panel.panel.transform);
            win.Finish();
        }
    }

    public class NextRoundGameButtonRun : BaseGameButtonRun
    {
        public NextRoundGameButtonRun() : base ("nextround") { }

        protected override void Run(Player player)
        {
            GameMgmt.Get().NextPlayer();
        }
    }
    
    public class OptionsGameButtonRun : BaseGameButtonRun
    {
        public OptionsGameButtonRun() : base ("options") { }

        protected override void Run(Player player)
        {
            OptionHelper.ShowOptionWindow(); 
        }
    }
    
    public class SaveGameButtonRun : BaseGameButtonRun
    {
        public SaveGameButtonRun() : base ("save") { }

        protected override void Run(Player player)
        {
            SaveWindow.Show(); 
        }
    }
    
    public class LoadGameButtonRun : BaseGameButtonRun
    {
        public LoadGameButtonRun() : base ("load") { }

        protected override void Run(Player player)
        {
            LoadWindow.Show();
        }
    }
    
    public class ModButtonRun : BaseGameButtonRun
    {
        public ModButtonRun() : base ("mod") { }

        protected override void Run(Player player)
        {
            SceneManager.LoadScene(3, LoadSceneMode.Single);
        }
    }

    public class ExitGameButtonRun : BaseGameButtonRun
    {
        public ExitGameButtonRun() : base ("exit") { }

        protected override void Run(Player player)
        {
            Application.Quit();
        }
    }

    public class NextUnitGameButtonRun : BaseGameButtonRun
    {
        public NextUnitGameButtonRun() : base ("nextUnit") { }

        protected override void Run(Player player)
        {
            S.Unit().ShowNextAvailableUnitForPlayer();
        }
    }
    
    public class CampaignGameButtonRun : BaseGameButtonRun
    {
        public CampaignGameButtonRun() : base ("campaign") { }

        protected override void Run(Player player)
        {
            LSys.tem.campaigns.ShowCampaigns();
        }
    }
    
}