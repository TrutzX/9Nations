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

public class UnitUI : MapElementUI
{
    public OnMapUI onmapui;

    public UnitInfo activeUnit;

    public GameObject unitinfo;
    // Start is called before the first frame update
    void Start()
    {
        UpdatePanel(null);
    }
    
    /// <summary>
    /// Show this unit
    /// </summary>
    /// <param name="unit"></param>
    public void UpdatePanel(UnitInfo unit)
    {
        GameObject.Find("InputAction").GetComponent<InputAction>().aUnit = unit==null?null:unit.gameObject;
        activeUnit = unit;
        
        //no unit?
        if (unit == null)
        {
            //hide it
            gameObject.SetActive(false);
            return;
        }
        
        //show unit
        gameObject.SetActive(true);
        
        //show icon
        unitinfo.GetComponentsInChildren<Image>()[1].sprite = unit.config.GetIcon();
        unitinfo.GetComponent<Button>().onClick.RemoveAllListeners();
        unitinfo.GetComponent<Button>().onClick.AddListener(() => { unit.ShowInfoWindow(); });
        UpdatePanelMessage();
        
        //remove actions
        UIHelper.ClearChild(actions);
        
        //add actions
        foreach (KeyValuePair<string, string> action in unit.config.GetActions())
        {
            //can add?
            if (ReqHelper.CheckOnlyFinal(PlayerMgmt.ActPlayer(),ActionHelper.GenReq(Data.nAction[action.Key]), unit.gameObject,unit.data.x, unit.data.y))
                AddActionButton(action.Key, action.Value,  unit, actions);
        }
        
        //remove old
        //GameObject[] old = unitPanel.GetComponentsInChildren<GameObject>();
        //foreach(GameObject o in old)
        {
            //Destroy(o);
        }
        
        //add self
        //CreateImageButton("Icons_"+Random.Range(0,244), unitPanel.transform);
        //CreateImageButton("Icons_"+Random.Range(0,244), unitPanel.transform);
    }

    public void UpdatePanelMessage()
    {
        SetPanelMessage(activeUnit.GetComponent<UnitInfo>().Status(PlayerMgmt.ActPlayerID()));
    }
}
