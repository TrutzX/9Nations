using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using Audio;
using Buildings;

using Game;
using GameButtons;
using Libraries;
using Libraries.Nations;
using Libraries.Rounds;
using LoadSave;
using Players.Infos;
using Players.PlayerResearches;
using Towns;
using UI;
using Units;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Players
{
    [Serializable]
    public class Player
    {
        public string name;
        public string icon;
        public string nation;
        public int id;
        public int points;
        public string status;

        public QuestMgmt quests;

        public PlayerFog fog;
        public PlayerResearchMgmt research;
        public InfoMgmt info;

        public Dictionary<string, string> Modi;
        public PlayerDevelopmentNation elements;
        
        [SerializeField] private Dictionary<string, string> features;
        
        
        /// <summary>
        /// Only for loading
        /// </summary>
        public Player()
        {
            
        }
        
        //TODO add icon selection
        public Player(int id, string name, string nation)
        {
            this.id = id;
            this.name = name;
            this.icon = "coat"+id;
            this.nation = nation;
            
            features = new Dictionary<string, string>();
            Modi = new Dictionary<string, string>();
            
            research = new PlayerResearchMgmt();
            research.player = this;
            
            quests = new QuestMgmt();
            quests.player = this;
            
            elements = new PlayerDevelopmentNation();
            elements.player = this;
            
            fog = new PlayerFog();
            info = new InfoMgmt();
            
        }

        public string GetFeature(string key)
        {
            if (!features.ContainsKey(key))
                return L.b.playerOptions[key].standard;
            return features[key];
        }

        public void SetFeature(string key, string value)
        {
            features[key] = value;
        }
        
        public void StartRound()
        {
            Debug.Log($"Start round {S.Round().GetRoundString()} for player {name}");
            info.Add(new Info($"Welcome {name}, it is "+S.Round().GetRoundString(), S.Round().Icon()));

            fog.StartRound();

            //update buttons
            UpdateButtonMenu();
            UpdateButtonBottom();

            //clear panels
            OnMapUI.Get().buildingUI.UpdatePanel(null);
            OnMapUI.Get().unitUI.UpdatePanel(null);
            OnMapUI.Get().InfoUi.UpdatePanel();
            
            //win?
            if (WinLose()) return;
            
            elements.StartRound();
            
            //prepare units
            foreach (var b in S.Building().GetByPlayer(id))
            {
                b.StartPlayerRound();
            }
            foreach (var b in S.Unit().GetByPlayer(id))
            {
                b.StartPlayerRound();
            }
            
            //show unit
            GameMgmt.Get().unit.ShowNextAvailableUnitForPlayer();
        }

        public void UpdateButtonBottom()
        {
            UIHelper.ClearChild(OnMapUI.Get().bottomButton);
            L.b.gameButtons.BuildMenu(this, "bottom", OnMapUI.Get().bottomUI, false,
                OnMapUI.Get().bottomButton.transform);
        }

        public void UpdateButtonMenu()
        {
            UIHelper.ClearChild(OnMapUI.Get().menuButton);
            L.b.gameButtons.BuildMenu(this, "top", OnMapUI.Get(), false, OnMapUI.Get().menuButton.transform);
        }

        private bool WinLose()
        {
            if (status == "win")
            {
                WindowPanelBuilder w = WindowPanelBuilder.Create("You won");
                w.panel.AddButton("End Game", () => { SceneManager.LoadScene(0); });
                w.panel.AddButton("Play a little more", () => { });
                w.Finish();
                NAudio.PlayMusic("win",false);
                status = null;
                return true;
            }
            
            if (status == "lose")
            {
                WindowPanelBuilder w = WindowPanelBuilder.Create("You lose");
                w.panel.AddButton("End Game", () =>
                {
                    //TODO delete player and move units to gaia player
                    SceneManager.LoadScene(0);
                });
                w.Finish();
                NAudio.PlayMusic("lose",false);
                status = null;
                return true;
            }

            return false;
        }

        public IEnumerator FinishRound()
        {
            //TODO Autosave only for human player
            //save
            if (LSys.tem.options["autosave"].Bool())
            {
                yield return GameMgmt.Get().load.ShowSubMessage($"Save auto save");
                LoadSaveMgmt.UpdateSave($"autosave{id}",$"Auto save {name}");
            }
            
            fog.FinishRound();
            
        }

        public void AfterLoad()
        {
            research.player = this;
            quests.player = this;
            elements.player = this;
        }

        public void NextRound()
        {
            quests.NextRound();
            if (GetFeature("research") == "true")
                research.NextRound();
            info.NextRound();
        }

        /// <summary>
        /// Get the ress from all player towns
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetResTotal(string key)
        {
            return S.Towns().GetByPlayer(id).Sum(t => t.GetRes(key));
        }

        /// <summary>
        /// Add the ress to all player towns
        /// </summary>
        /// <param name="key"></param>
        /// <param name="amount"></param>
        /// <param name="res"></param>
        public void AddResTotal(string key, int amount, ResType res)
        {
            //has a town?
            if (S.Towns().GetByPlayer(id).Count == 0)
                return;
            
            //add?
            if (amount >= 0)
            {
                S.Towns().GetByPlayer(id)[0].AddRes(key, amount, res);
                return;
            }
            
            //remove
            foreach (Town town in S.Towns().GetByPlayer(id))
            {
                int act = town.GetRes(key);
                if (act >= -amount)
                {
                    town.AddRes(key, amount, res);
                    return;
                }
                
                town.AddRes(key, -act, res);
                amount += act;
            }

            if (amount != 0)
            {
                Debug.LogWarning($"Can not add all res {amount} {key}");
            }
        }

        public Nation Nation()
        {
            return L.b.nations[nation];
        }

        public void FirstRound()
        {
            NextRound();
        }
    }
}
