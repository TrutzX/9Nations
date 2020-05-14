using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game;
using Libraries.Rounds;
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
    
        /// <summary>
        /// Get it
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public static PlayerMgmt Get()
        {
            return GameMgmt.Get().data.players;
        }
    
        // Start is called before the first frame update
        public PlayerMgmt()
        {
            players = new List<Player>();
            createPlayerCounter = -1;
            actPlayer = -1;
        }

        public int CreatePlayer(string name, string nation)
        {
            players.Add(new Player(++createPlayerCounter,name,nation));
            Debug.Log($"Create Player {name} ({createPlayerCounter}) for nation {nation}");
            return createPlayerCounter;
        }

        public static Player Get(int id)
        {
            return Get().players.Single(p => id == p.id);
        }

        public static Player ActPlayer()
        {
            return Get().players[ActPlayerID()];
        }

        public static int ActPlayerID()
        {
            return Get().actPlayer;
        }
    
        public IEnumerator NextRound()
        {
            int c = 0;
            int m = players.Count;
            foreach (Player p in players)
            {
                p.NextRound();
                yield return GameMgmt.Get().load.ShowSubMessage($"Updating players ({c}/{m})");
                c++;
            }
        }
    
        public void FirstRound()
        {
            players.ForEach(p => p.FirstRound());
        }

        public void AfterLoad()
        {
            Debug.Log("players "+players.Count);
            players.ForEach(p => p.AfterLoad());
        }

        public void ResetRound()
        {
            actPlayer = 0;
            ActPlayer().StartRound();
        }
        
        public IEnumerator NextPlayer()
        {
            actPlayer++;
            //next round?
            if (actPlayer >= players.Count)
            {
                yield return S.Round().NextRound();
                actPlayer = 0;
            }

            yield return GameMgmt.Get().load.ShowMessage($"Start turn for {ActPlayer().name}");
            ActPlayer().StartRound();
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
        
        public IEnumerator CreatingFog()
        {
            foreach (Player p in players)
            {
                yield return p.fog.CreatingFog(p.id);
            }
        }
    }
}
