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
using UnityEngine;
using UnityEngine.UI;

public class BuildingUI : MapElementUI<BuildingInfo>
{
    // Start is called before the first frame update
    void Start()
    {
        UpdatePanel(null);
    }
    
    /// <summary>
    /// Show this unit
    /// </summary>
    /// <param name="building"></param>
    public override void UpdatePanel(BuildingInfo building)
    {
        GameObject.Find("InputAction").GetComponent<InputAction.InputAction>().aBuilding = building;
        active = building;
        
        //no unit?
        if (building == null)
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
        //add actions
        foreach (KeyValuePair<string, string> action in active.config.GetActions())
        {
            //can add?
            if (ReqHelper.CheckOnlyFinal(PlayerMgmt.ActPlayer(),ActionHelper.GenReq(Data.nAction[action.Key]), active,active.data.x, active.data.y))
                AddActionButton(action.Key, action.Value, active, actions);
        }
    }
}
