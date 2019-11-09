using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game;
using LoadSave;
using Players;
using Units;
using UnityEngine;

public class RoundMgmt : ScriptableObject
{

    private int _round;

    private string[] season = {"spring", "summer", "autumn", "winter"};
    private string[] day = {"morning", "afternoon", "night"};

    public int Round => _round;

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
        _round = GameMgmt.Get().data.round;
    }
    
    public void NextRound()
    {
        _round++;
        GameMgmt.Get().data.round = _round;
        
        foreach (BuildingInfo u in BuildingMgmt.Get().GetAll())
        {
            u.NextRound();
        }
        foreach (UnitInfo u in UnitMgmt.Get().GetAll())
        {
            u.NextRound();
        }
        PlayerMgmt.Get().NextRound();
        
        //save game
        if (Data.features.autosave.Bool())
        {
            LoadSaveMgmt.UpdateSave("autosave.9n");
        }
        
    }

    public string Icon()
    {
        return Icon(_round);
    }

    public string Icon(int round)
    {
        int[] i = {110, 12, 22, 108, 8, 20, 106, 4, 18, 104, 0, 16};
        return "DayNightClock:DayNightClock_" + i[round % 12];
    }
    
    public string GetRoundString()
    {
        return GetRoundString(_round);
    }

    public string GetRoundString(int round)
    {
        return $"{GetSeasonString()} {day[round%3]}, year {round/12+1}";
    }

    public string GetSeasonString()
    {
        return season[_round%12/3];
    }

    public bool IsSeason(String s)
    {
        for(int i=0;i<4;i++)
            if (s == season[i] && _round % 12 / 3 == i)
            {
                return true;
            }

        return false;
    }

    public bool IsDayTime(String d)
    {
        for(int i=0;i<3;i++)
            if (d == day[i] && _round % 3 == i)
            {
                return true;
            }

        return false;
    }
}
