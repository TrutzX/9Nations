using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game;
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
                yield return RoundMgmt.Get().NextRound();
                actPlayer = 0;
            }

            yield return GameMgmt.Get().load.ShowMessage($"Start turn for {ActPlayer().name}");
            ActPlayer().StartRound();
        }
    }
}
