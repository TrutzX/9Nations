using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using DataTypes;
using Game;
using Libraries;
using Nations;
using Players.Infos;
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
        public string nation;
        public int id;
        public string status;

        public QuestMgmt quests;

        public PlayerFog fog;
        public ResearchMgmt research;
        public InfoMgmt info;

        public Dictionary<string, string> Modi;
        
        [SerializeField] private Dictionary<string, string> features;
        
        
        /// <summary>
        /// Only for loading
        /// </summary>
        public Player()
        {
            
        }
        
        public Player(int id, string name, string nation)
        {
            this.id = id;
            this.name = name;
            this.nation = nation;
            
            features = new Dictionary<string, string>();
            Modi = new Dictionary<string, string>();
            
            research = new ResearchMgmt();
            research.player = this;
            
            quests = new QuestMgmt();
            quests.player = this;
            
            fog = new PlayerFog();
            fog.Init(id);
            
            info = new InfoMgmt();
        }

        public string GetFeature(string key)
        {
            if (!features.ContainsKey(key))
                return Data.featurePlayer[key].standard;
            return features[key];
        }

        public void SetFeature(string key, string value)
        {
            features[key] = value;
        }
        
        public void StartRound()
        {
            Debug.Log($"Start round {RoundMgmt.Get().GetRoundString()} for player {name}");
            info.Add(new Info($"Welcome {name}, it is "+RoundMgmt.Get().GetRoundString(), RoundMgmt.Get().Icon()));

            fog.StartRound();

            //update buttons
            UpdateButtonTop();
            UpdateButtonBottom();

            //clear panels
            OnMapUI.Get().buildingUI.UpdatePanel(null);
            OnMapUI.Get().unitUI.UpdatePanel(null);
            OnMapUI.Get().InfoUi.UpdatePanel();
            
            //win?
            WinLose();
            
            //show unit
            UnitMgmt.Get().ShowNextAvaibleUnitForPlayer();
        }

        public void UpdateButtonBottom()
        {
            UIHelper.ClearChild(OnMapUI.Get().bottomButton);
            GameButtonHelper.BuildMenu(this, "bottom", OnMapUI.Get().bottomButtonText, false,
                OnMapUI.Get().bottomButton.transform);
        }

        public void UpdateButtonTop()
        {
            UIHelper.ClearChild(OnMapUI.Get().topButton);
            GameButtonHelper.BuildMenu(this, "top", OnMapUI.Get().topButtonText, false, OnMapUI.Get().topButton.transform);
        }

        private void WinLose()
        {
            if (status == "win")
            {
                WindowPanelBuilder w = WindowPanelBuilder.Create("You won");
                w.panel.AddButton("End Game", () => { SceneManager.LoadScene(0); });
                w.panel.AddButton("Play a little more", () => { });
                w.Finish();
                NAudio.PlayMusic("win",false);
                status = null;
            }
            else if (status == "lose")
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
            }
        }

        public void FinishRound()
        {
            fog.FinishRound();
        }

        public void AfterLoad()
        {
            fog.AfterLoad(id);
            research.player = this;
            quests.player = this;
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
        public int GetRessTotal(string key)
        {
            return TownMgmt.Get().GetByPlayer(id).Sum(t => t.GetRes(key));
        }

        /// <summary>
        /// Add the ress to all player towns
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public void AddRessTotal(string key, int amount)
        {
            //has a town?
            if (TownMgmt.Get().GetByPlayer(id).Count == 0)
                return;
            
            //add?
            if (amount >= 0)
            {
                TownMgmt.Get().GetByPlayer(id)[0].AddRes(key,amount);
                return;
            }
            
            //remove
            foreach (Town town in TownMgmt.Get().GetByPlayer(id))
            {
                int act = town.GetRes(key);
                if (act >= -amount)
                {
                    town.AddRes(key,amount);
                    return;
                }
                
                town.AddRes(key,-act);
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
