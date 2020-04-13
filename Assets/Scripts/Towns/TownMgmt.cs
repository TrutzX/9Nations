using System;
using System.Collections.Generic;
using System.Linq;
using Buildings;
using Game;
using Players;
using Tools;
using UnityEngine;

namespace Towns
{
    [Serializable]
    public class TownMgmt
    {
        [SerializeField] private List<Town> towns; 
        [SerializeField] private int createTownCounter;
    
        // Start is called before the first frame update
        public TownMgmt()
        {
            towns = new List<Town>();
            createTownCounter = -1;
        }

        public int Create(string name, NVector pos)
        {
            return Create(name, PlayerMgmt.ActPlayer().id,  pos);
        }
    
        /// <summary>
        /// Create the town on this place and build the town hall
        /// </summary>
        /// <param name="name"></param>
        /// <param name="playerID"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public int Create(string name, int playerID, NVector pos)
        {
            towns.Add(new Town(++createTownCounter,playerID,name, pos));
            Debug.Log($"Create town {name} ({createTownCounter}) for {playerID}");
            //create townhall
            GameMgmt.Get().building.Create(createTownCounter, PlayerMgmt.Get(playerID).elements.TownHall(),pos);
            return createTownCounter;
        }

        public Town Get(int id)
        {
            return S.Towns().towns.Single(p => id == p.id);
        }

        public Town[] GetAll()
        {
            return towns.ToArray();
        }
    
        public List<Town> GetByPlayer(int id)
        {
            return towns.Where(p => id == p.playerId).ToList();
        }
    
        public List<Town> GetByActPlayer()
        {
            return GetByPlayer(PlayerMgmt.ActPlayer().id);
        }
    
    
        /// <summary>
        /// Get the next town
        /// </summary>
        /// <param name="player"></param>
        /// <param name="pos"></param>
        /// <param name="nextField"></param>
        /// <returns></returns>
        public Town NearstTown(Player player, NVector pos, bool nextField)
        {
            int x = pos.x;
            int y = pos.y;
        
            // on the next field and part of the player?
            foreach (BuildingInfo b in new [] { S.Building().At(pos), 
                S.Building().At(pos.DiffX(-1)),
                S.Building().At(pos.DiffY(-1)), 
                S.Building().At(pos.DiffX(1)),
                S.Building().At(pos.DiffY(1)) }) {
                if (b != null && b.Town().playerId == player.id) {
                    return b.Town();
                }
            }

            // look only for the field?
            if (nextField) {
                return null;
            }

            // get towns
            List<Town> towns = GetByPlayer(player.id).ToList();

            // has a town?
            if (towns.Count == 0) {
                return null;
            }

            if (towns.Count == 1)
            {
                return towns[0];
            }

            //TODO support for level 
        
            Town t = null;
            // find nearst town
            int diff = 9999999;
            foreach (Town pT in towns) {
                int d = Math.Abs(pT.pos.x - x) + Math.Abs(pT.pos.y - y);
                // Town is near?
                if (d < diff) {
                    diff = d;
                    t = pT;
                }
            }

            return t;
        }

    
    }
}
