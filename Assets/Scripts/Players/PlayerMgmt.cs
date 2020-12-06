using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Audio;
using Game;
using Libraries.Rounds;
using Players.PlayerTypes;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Players
{
    [Serializable]
    public class PlayerMgmt
    {
        [SerializeField] private List<Player> players;
        [SerializeField] private int createPlayerCounter;
        [SerializeField] private int actPlayer;

        // Start is called before the first frame update
        public PlayerMgmt()
        {
            players = new List<Player>();
            createPlayerCounter = -1;
            actPlayer = -1;
        }

        public int ActPlayer => actPlayer;

        public int CreatePlayer(string name, string nation, PlayerType type = PlayerType.Human)
        {
            players.Add(new Player(++createPlayerCounter,name,nation, type));
            Debug.Log($"Create Player {name} ({createPlayerCounter}/{type}) for nation {nation}");
            return createPlayerCounter;
        }

        public void KillPlayer(int pid)
        {
            //set all units & towns to gaia
            foreach (var unit in S.Unit().GetByPlayer(pid))
            {
                unit.data.playerId = 0; //gaia
            }
            foreach (var town in S.Towns().GetByPlayer(pid))
            {
                town.playerId = 0;
            }

            int act=1;
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].id != pid) continue;
                act = (actPlayer == i) ? 0 : actPlayer > i ? -1 : 1;
            }

            //remove from array
            players.Remove(Get(pid));
            
            //remove before
            if (act == -1)
            {
                actPlayer--;
            }
            
            //remove self?
            if (act == 0)
            {
                actPlayer--;
                S.Game().NextPlayer();
            }
        }

        public IEnumerator NextRound()
        {
            int c = 0;
            int m = players.Count;
            foreach (Player p in players)
            {
                yield return GameMgmt.Get().load.ShowSubMessage($"Updating players ({c}/{m})");
                yield return p.NextRound();
                yield return p.Type().NextRound(p);
                c++;
            }
        }

        public IEnumerator GameStart()
        {
            Debug.Log("GameStart "+players.Count);
            foreach (Player p in players)
            {
                yield return p.FirstRound();
                yield return p.Type().Start(p);
            }
        }

        public IEnumerator GameLoaded()
        {
            Debug.Log("GameLoaded "+players.Count);
            foreach (Player p in players)
            {
                p.AfterLoad();
                yield return p.Type().Loaded(p);
            }
        }

        public IEnumerator GameBegin()
        {
            Debug.Log("GameBegin "+players.Count);
            foreach (Player p in players)
            {
                yield return p.FirstRound();
                yield return p.Type().Begin(p);
            }
        }

        public void ResetRound()
        {
            actPlayer = 1;
            S.ActPlayer().StartRound();
        }
        
        public IEnumerator NextPlayer()
        {
            actPlayer++;
            //next round?
            if (actPlayer >= players.Count)
            {
                actPlayer = -1;
                yield return S.Round().NextRound();
                actPlayer = 0;
            }

            //a human player left?
            if (CountHumanPlayer() == 0)
            {
                WindowPanelBuilder w = WindowPanelBuilder.Create(S.T("endGame"));
                w.panel.AddLabelT("endGamePlayer");
                w.AddClose();
                w.onClose = () =>
                {
                    SceneManager.LoadScene(0);
                };
                w.Finish();
                yield break;
            }

            //is a human?
            if (players[actPlayer].Type().id == PlayerType.Human)
            {
                yield return GameMgmt.Get().load.ShowMessage($"Start turn for {S.ActPlayer().name}");
                S.ActPlayer().StartRound();
            }
            else
            {
                yield return NextPlayer();
            }
        }

        public Player OverlayHighest(string id, NVector pos)
        {
            int found=0;
            Player player = null;
            foreach (var p in players)
            {
                int f = (p.overlay.Get(id, pos));
                if (f > found)
                {
                    found = f;
                    player = p;
                }
            }

            return player;
        }

        public Player Get(int id)
        {
            return players.Single(p => id == p.id);
        }

        public int CountHumanPlayer()
        {
            return players.Count(player => player.Type().id == PlayerType.Human);
        }

        public void Init()
        {
            CreatePlayer(S.T("nature"), "nature", PlayerType.Nature);
        }
    }
}
