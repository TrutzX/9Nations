using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Game;
using Libraries;
using Players;
using UnityEngine;
using UnityEngine.UI;
using Towns;

[Serializable]
public class TownMgmt
{
    [SerializeField] private List<Town> towns; 
    [SerializeField] private int createTownCounter;
    
    /// <summary>
    /// Get it
    /// </summary>
    /// <returns></returns>
    public static TownMgmt Get()
    {
        return GameMgmt.Get().data.towns;
    }
    
    // Start is called before the first frame update
    public TownMgmt()
    {
        towns = new List<Town>();
        createTownCounter = -1;
    }

    public int Create(string name, int x, int y)
    {
        return Create(name, PlayerMgmt.ActPlayer().id,  x,  y);
    }
    
    /// <summary>
    /// Create the town on this place and build the town hall
    /// </summary>
    /// <param name="name"></param>
    /// <param name="playerID"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public int Create(string name, int playerID, int x, int y)
    {
        towns.Add(new Town(++createTownCounter,playerID,name, x, y));
        Debug.Log($"Create town {name} ({createTownCounter}) for {playerID}");
        //create townhall
        BuildingMgmt.Get().Create(createTownCounter, PlayerMgmt.Get(playerID).Nation().Townhall,x,y);
        return createTownCounter;
    }

    public static Town Get(int id)
    {
        return Get().towns.Single(p => id == p.id);
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
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="nextField"></param>
    /// <returns></returns>
    public Town NearstTown(Player player, int x, int y, bool nextField) {

        // on the next field and part of the player?
        foreach (BuildingInfo b in new [] { BuildingMgmt.At(x, y), BuildingMgmt.At(x-1, y),
            BuildingMgmt.At(x, y+1), BuildingMgmt.At(x+1, y),
            BuildingMgmt.At(x, y-1) }) {
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

        Town t = null;
        // find nearst town
        int diff = 9999999;
        foreach (Town pT in towns) {
            int d = Math.Abs(pT.x - x) + Math.Abs(pT.y - y);
            // Town is near?
            if (d < diff) {
                diff = d;
                t = pT;
            }
        }

        return t;
    }

    
}
