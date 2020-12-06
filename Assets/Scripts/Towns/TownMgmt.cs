using System;
using System.Collections.Generic;
using System.Linq;
using Buildings;
using Game;
using Libraries;
using MapElements.Buildings;
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
            return Create(name, S.ActPlayer().id,  pos);
        }
    
        /// <summary>
        /// Create the town on this place and build the town hall
        /// </summary>
        /// <param name="name"></param>
        /// <param name="playerID"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public int Create(string name, int playerID, NVector pos, bool townHall = true)
        {
            Town town = new Town(++createTownCounter,playerID,name, pos);
            town.coat = L.b.coats.Random("town").id;
            towns.Add(town);
            Debug.Log($"Create town {name} ({createTownCounter}) for {playerID}");
            //create townhall
            if (townHall)
                GameMgmt.Get().building.Create(createTownCounter, S.Player(playerID).elements.TownHall(),pos);
            return createTownCounter;
        }

        public void Kill(int tid)
        {
            //remove it
            Town t = Get(tid);
            Debug.Log($"Destroy town {t.name} ({tid})");
            towns.Remove(t);

            //reset all units
            foreach (var unit in S.Unit().GetByTown(tid))
            {
                unit.data.townId = -1;
            }
            
            //destroy buildings
            foreach (var build in S.Building().GetByTown(tid))
            {
                build.Kill();
            }
            
            //has the player towns left?
            Town nt = NearestTown(t.Player(), t.pos, false);

            if (nt == null)
            {
                //no towns
                return;
            }
            
            //TODO transfer res?
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
            return GetByPlayer(S.ActPlayer().id);
        }

        public Town OverlayHighest(string id, NVector pos)
        {
            int found=0;
            Town town = null;
            foreach (var t in towns)
            {
                int f = (t.overlay.Get(id, pos));
                if (f > found)
                {
                    found = f;
                    town = t;
                }
            }

            return town;
        }

        public Town OverlayHighestPlayer(string id, NVector pos, Player p)
        {
            int found=0;
            Town town = null;
            foreach (var t in towns)
            {
                //own town?
                if (t.playerId != p.id)
                    continue;
                
                int f = (t.overlay.Get(id, pos));
                if (f > found)
                {
                    found = f;
                    town = t;
                }
            }

            return town;
        }
    
    
        /// <summary>
        /// Get the next town
        /// </summary>
        /// <param name="player"></param>
        /// <param name="pos"></param>
        /// <param name="nextField"></param>
        /// <returns></returns>
        public Town NearestTown(Player player, NVector pos, bool nextField)
        {
            int x = pos.x;
            int y = pos.y;
            
            //is on this field from a own town`
            var town = S.Towns().OverlayHighestPlayer("boundary", pos, player);
            if (town != null) return town;
        
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
