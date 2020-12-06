using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using Audio;
using Buildings;
using Classes;
using Game;
using Libraries;
using Libraries.Coats;
using Libraries.Nations;
using Libraries.Overlays;
using Libraries.Rounds;
using LoadSave;
using Players.Infos;
using Players.PlayerResearches;
using Players.PlayerTypes;
using Tools;
using Towns;
using UI;
using Units;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Players
{
    [Serializable]
    public class Player : IGameRoundObject
    {
        public string name;
        public string coat;
        public string nation;
        public int id;
        public int points;
        public string status;

        public QuestMgmt quests;
        public MapOverlay overlay;

        public PlayerFog fog;
        public PlayerResearchMgmt research;
        public PlayerInfoMgmt info;

        public Dictionary<string, string> Modi;
        public PlayerDevelopmentNation elements;
        [SerializeField] private string type;
        [NonSerialized] private BasePlayerType typeClass;
        
        [SerializeField] private Dictionary<string, string> features;
        
        public Dictionary<string, string> data;

        public PlayerSpells spells;
        
        /// <summary>
        /// Only for loading
        /// </summary>
        public Player()
        {
            
        }
        
        public Player(int id, string name, string nation, PlayerType type)
        {
            this.id = id;
            this.name = name;
            coat = L.b.coats.Auto(id);
            this.nation = nation;
            
            features = new Dictionary<string, string>();
            Modi = new Dictionary<string, string>();
            
            research = new PlayerResearchMgmt();
            research.player = this;
            
            overlay = new MapOverlay();
            
            quests = new QuestMgmt();
            quests.player = this;
            
            elements = new PlayerDevelopmentNation();
            elements.player = this;
            
            fog = new PlayerFog();
            fog.Init();
            info = new PlayerInfoMgmt();

            this.type = type.ToString();
            
            data = new Dictionary<string, string>();
            
            spells = new PlayerSpells();
        }

        public BasePlayerType Type()
        {
            return typeClass ?? (typeClass = LClass.s.playerTypes[type]);
        }

        public Coat Coat()
        {
            return L.b.coats[coat];
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
            info.player = this;
            
            Debug.Log($"Start round {S.Round().GetRoundString()} for player {name}");
            info.Add(new Info($"Welcome {name}, it is "+S.Round().GetRoundString(), S.Round().Icon()));

            fog.StartRound();
            overlay.ViewOverlay(GetFeature("overlay"));

            //update buttons
            UpdateButtonMenu();
            UpdateButtonBottom();
            
            elements.StartRound();
            quests.NextRound();

            //clear panels
            OnMapUI.Get().buildingUI.UpdatePanel(null);
            OnMapUI.Get().unitUI.UpdatePanel(null);
            OnMapUI.Get().InfoUi.UpdatePanel();
            
            //win?
            if (WinLose()) return;
            
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
            
            //show noti?
            //add new
            foreach (Info i in info.infos)
            {
                if (i.round != GameMgmt.Get().gameRound.Round)
                {
                    break;
                }
                
                if (i.read) continue;

                //show?
                if (!string.IsNullOrEmpty(i.desc))
                {
                    i.ShowImportant();
                }
            }
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
                WindowPanelBuilder w = WindowPanelBuilder.Create(S.T("endGameWin"));
                w.panel.AddLabel(S.T("endGamePoints", points, S.Round().Round));
                w.panel.AddButtonT("endgame", () => { SceneManager.LoadScene(0); });
                w.panel.AddButtonT("endGameWinButton", () => { });
                w.Finish();
                NAudio.PlayMusic("win",false);
                status = null;
                return true;
            }
            
            if (status == "lose")
            {
                WindowPanelBuilder w = WindowPanelBuilder.Create(S.T("endGameLose"));
                w.panel.AddLabel(S.T("endGamePoints", points, S.Round().Round));
                w.panel.AddButtonT("endgame", () =>
                {
                    w.Close();
                });
                w.onClose = () =>
                {
                    S.Players().KillPlayer(id);
                };
                w.Finish();
                NAudio.PlayMusic("lose",false);
                status = null;
                return true;
            }

            return false;
        }

        public IEnumerator FinishRound()
        {
            yield return Type().FinishRound(this);
            fog.FinishRound();
        }

        /**
         * Call after the game is loaded
         */
        public void AfterLoad()
        {
            fog.Init();
            research.player = this;
            quests.player = this;
            elements.player = this;
            info.player = this;
        }

        public IEnumerator NextRound()
        {
            if (GetFeature("research") == "true")
                research.NextRound();
            info.NextRound();
            yield return null;
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

        public IEnumerator FirstRound()
        {
            yield return NextRound();
        }
    }
}
