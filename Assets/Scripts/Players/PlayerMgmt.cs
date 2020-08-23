using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game;
using Libraries.Rounds;
using Players.PlayerTypes;
using Tools;
using UnityEngine;

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

        public IEnumerator NextRound()
        {
            int c = 0;
            int m = players.Count;
            foreach (Player p in players)
            {
                yield return GameMgmt.Get().load.ShowSubMessage($"Updating players ({c}/{m})");
                p.NextRound();
                yield return p.Type().NextRound(p);
                c++;
            }
        }

        public IEnumerator GameStart()
        {
            Debug.Log("GameStart "+players.Count);
            foreach (Player p in players)
            {
                p.FirstRound();
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
                p.FirstRound();
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
            if (players.Count(player => player.Type().id == PlayerType.Human) == 0)
            {
                //todo XXX
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

        public void Init()
        {
            CreatePlayer(S.T("nature"), "nature", PlayerType.Nature);
        }
    }
}
