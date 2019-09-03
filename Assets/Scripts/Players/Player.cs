using System;
using System.Linq;
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
            
            quests = new QuestMgmt();
            fog = new PlayerFog();
            fog.Init(id);
        }

        public void StartRound()
        {
            OnMapUI.Get().SetRessRoundMessage(RoundMgmt.Get().GetRoundString());
            fog.StartRound();
            
            //win?
            if (status == "win")
            {
                WindowPanelBuilder w = WindowPanelBuilder.Create("You won");
                w.panel.AddButton("End Game", () => { SceneManager.LoadScene(0); });
                w.panel.AddButton("Play a little more", () => { });
                w.Finish();
                status = null;
            } else if (status == "lose")
            {
                WindowPanelBuilder w = WindowPanelBuilder.Create("You lose");
                w.panel.AddButton("End Game", () => { 
                    //TODO delete player and move units to gaia player
                    SceneManager.LoadScene(0);
                });
                w.Finish();
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
        }

        public void NextRound()
        {
            quests.NextRound(this);
        }

        /// <summary>
        /// Get the ress from all player towns
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetRessTotal(string key)
        {
            return TownMgmt.Get().GetByPlayer(id).Sum(t => t.GetRess(key));
        }
    }
}
