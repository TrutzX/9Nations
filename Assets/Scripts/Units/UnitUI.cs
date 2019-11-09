using System;
using System.Collections;
using System.Collections.Generic;
using Actions;
using Buildings;
using DataTypes;
using Game;
using Players;
using reqs;
using UI;
using Units;
using UnityEngine;
using UnityEngine.UI;

public class UnitUI : MapElementUI<UnitInfo>
{

    // Start is called before the first frame update
    void Start()
    {
        UpdatePanel(null);
    }
    
    /// <summary>
    /// Show this unit
    /// </summary>
    /// <param name="unit"></param>
    public override void UpdatePanel(UnitInfo unit)
    {
        GameObject.Find("InputAction").GetComponent<InputAction.InputAction>().aUnit = unit;
        active = unit;
        
        //no unit?
        if (unit == null)
        {
            //hide it
            gameObject.SetActive(false);
            return;
        }
        
        UpdateInfoButton();
        AddButtons();
    }

    public override void AddAllActionButtons()
    {
        foreach (KeyValuePair<string, string> action in active.config.GetActions())
        {
            //can add?
            if (ReqHelper.CheckOnlyFinal(PlayerMgmt.ActPlayer(),ActionHelper.GenReq(Data.nAction[action.Key]), active,active.X(), active.Y()))
                AddActionButton(action.Key, action.Value,  active, actions);
        }
    }
}
