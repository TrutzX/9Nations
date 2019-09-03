using System;
using System.Collections;
using System.Collections.Generic;
using Actions;
using DataTypes;
using Game;
using Players;
using reqs;
using UnityEngine;
using UnityEngine.UI;

public class UnitUI : MonoBehaviour
{
    public OnMapUI onmapui;

    private UnitInfo activeUnit;

    public GameObject unitinfo;
    public GameObject actions;
    public GameObject infotext;
    // Start is called before the first frame update
    void Start()
    {
        UpdateUnitPanel(null);
    }
    
    /// <summary>
    /// Show this unit
    /// </summary>
    /// <param name="unit"></param>
    public void UpdateUnitPanel(UnitInfo unit)
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
        unitinfo.GetComponent<Button>().onClick.AddListener(() =>
        {
            unit.ShowInfoWindow();
        });
        UpdatePanelMessage();
        
        //remove actions
        Transform[] old = actions.GetComponentsInChildren<Transform>();
        for (int i = 1; i < old.Length; i++)
        {
            Destroy(old[i].gameObject);
        }
        
        //add actions
        foreach (KeyValuePair<string, string> action in unit.config.GetActions())
        {
            //can add?
            if (ReqHelper.CheckOnlyFinal(PlayerMgmt.ActPlayer(),ActionHelper.GenReq(Data.nAction[action.Key]), unit.gameObject,(int) unit.transform.position.x, (int) unit.transform.position.y))
                AddActionButton(action.Key, action.Value,  unit.gameObject);
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
        SetPanelMessage(activeUnit.GetComponent<UnitInfo>().GetStatus());
    }
    
    void AddActionButton(string action, string sett, GameObject unit)
    {
        
        NAction act = Data.nAction[action];
        
        //can add?
        if (unit.GetComponent<UnitInfo>().IsUnderConstrution() && !act.useUnderConstruction)
        {
            return;
        }
        
        GameObject button = UIElements.CreateImageButton(act.icon, actions.transform);
        button.GetComponent<Button>().onClick.AddListener(() =>
        {
            NLib.GetAction(act.id).ButtonUnitRun(unit, (int) unit.transform.position.x, (int) unit.transform.position.y, 
                sett);
        });
        UIHelper.HoverEnter(infotext.GetComponent<Text>(),act.name,button,
            () => { SetPanelMessage(unit.GetComponent<UnitInfo>().GetStatus()); });
    }
    
    public void SetPanelMessage(string text)
    {
        infotext.GetComponent<Text>().text = text;
    }
}
