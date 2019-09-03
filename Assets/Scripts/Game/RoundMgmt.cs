using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game;
using Players;
using UnityEngine;

public class RoundMgmt : ScriptableObject
{

    private int round;

    private string[] season = {"spring", "summer", "autumn", "winter"};
    private string[] day = {"morning", "afternoon", "night"};
    
    /// <summary>
    /// Get it
    /// </summary>
    /// <returns></returns>
    public static RoundMgmt Get()
    {
        return GameMgmt.Get().round;
    }

    public void Load()
    {
        round = GameMgmt.Get().data.round;
    }
    
    public void NextRound()
    {
        round++;
        GameMgmt.Get().data.round = round;
        
        foreach (GameObject u in BuildingMgmt.Get().GetAll())
        {
            u.GetComponent<BuildingInfo>().NextRound();
        }
        foreach (GameObject u in UnitMgmt.Get().GetAll())
        {
            u.GetComponent<UnitInfo>().NextRound();
        }
        PlayerMgmt.Get().NextRound();
        
    }

    public string GetRoundString()
    {
        return $"{GetSeasonString()} {day[round%3]}, year {round/12+1}";
    }

    public string GetSeasonString()
    {
        return season[round%12/3];
    }

    public bool IsSeason(String s)
    {
        for(int i=0;i<4;i++)
            if (s == season[i] && round % 12 / 3 == i)
            {
                return true;
            }

        return false;
    }

    public bool IsDayTime(String d)
    {
        for(int i=0;i<3;i++)
            if (d == day[i] && round % 3 == i)
            {
                return true;
            }

        return false;
    }
}
