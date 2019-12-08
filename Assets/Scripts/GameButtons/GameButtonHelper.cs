using DataTypes;
using DefaultNamespace;
using Endless;
using Game;
using Help;
using LoadSave;
using MapActions;
using Options;
using Players;
using Players.Quests;
using Towns;
using Units;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class GameButtonHelper
    {
        public static void Call(string id, Player player)
        {
            switch (id)
            {
                case "mainmenu":
                    WindowsMgmt.GameMainMenu();
                    return;
                case "research":
                    ResearchWindow.ShowResearchWindow();
                    return;
                case "lexicon":
                    HelpHelper.ShowLexicon();
                    return;
                case "quest":
                    QuestHelper.ShowQuestWindow();
                    return;
                case "debug":
                    DebugHelper.DebugMenu();
                    return;
                case "towns":
                    TownHelper.ShowTownWindow();
                    return;
                case "nextround":
                    GameMgmt.Get().NextPlayer();
                    return;
                case "options":
                    OptionHelper.ShowOptionWindow(); 
                    return;
                case "save":
                    SaveWindow.Show(); 
                    return;
                case "load":
                    LoadWindow.Show();
                    return;
                case "mod":
                    SceneManager.LoadScene(3, LoadSceneMode.Single);
                    return;
                case "title":
                    SceneManager.LoadScene(0, LoadSceneMode.Single);
                    return;
                case "exit":
                    Application.Quit();
                    return;
                case "endless":
                    EndlessGameWindow.Get().Show();
                    return;
                case "kingdom":
                    KingdomOverview.ShowHelpWindow();
                    return;
                case "nextUnit":
                    UnitMgmt.Get().ShowNextAvaibleUnitForPlayer();
                    return;
                case "campaign":
                    CampaignHelper.ShowCampaigns();
                    return;
                case "back":
                    UIHelper.ClearChild(GameObject.Find("MenuPanel"));
                    BuildMenu(null, "title", null, true, GameObject.Find("MenuPanel").transform);
                    return;
                case "more":
                    UIHelper.ClearChild(GameObject.Find("MenuPanel"));
                    BuildMenu(null, "more", null, true, GameObject.Find("MenuPanel").transform);
                    return;
                default:
                    Debug.LogWarning($"Game button type {id} is unknown");
                    return;
            }
        
        }

        public static void BuildMenu(Player player, string location, Text text, bool button, Transform transform)
        {
            foreach (GameButton b in Data.gameButton)
            {
                if (!b.location.Contains(location))
                {
                    continue;
                }
                
                //can use?
                if (!b.CheckReq(player))
                {
                    continue;
                }

                //create it
                if (button)
                {
                    b.CreateImageTextButton(transform, player);
                }
                else
                {
                    b.CreateImageButton(transform, player, text);
                }
            }
        }
        
    }
}