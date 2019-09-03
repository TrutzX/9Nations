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

public class BuildingUI : MonoBehaviour
{
    public OnMapUI onmapui;

    private BuildingInfo active;

    public GameObject info;
    public GameObject actions;
    public GameObject infotext;
    // Start is called before the first frame update
    void Start()
    {
        UpdatePanel(null);
    }
    /// <summary>
    /// Show this unit
    /// </summary>
    /// <param name="building"></param>
    public void UpdatePanel(BuildingInfo building)
    {
        active = building;
        
        //no unit?
        if (building == null)
        {
            //hide it
            gameObject.SetActive(false);
            return;
        }
        
        //show unit
        gameObject.SetActive(true);
        
        //show icon
        info.GetComponentsInChildren<Image>()[1].sprite = building.GetComponent<SpriteRenderer>().sprite;
        SetPanelMessage(building.GetStatus());
        
        info.GetComponent<Button>().onClick.RemoveAllListeners();
        info.GetComponent<Button>().onClick.AddListener(() =>
        {
            building.ShowInfoWindow();
        });
        
        //remove actions
        Transform[] old = actions.GetComponentsInChildren<Transform>();
        for (int i = 1; i < old.Length; i++)
        {
            Destroy(old[i].gameObject);
        }

        //add actions
        foreach (KeyValuePair<string, string> action in building.config.GetActions())
        {
            //can add?
            AddActionButton(action.Key, action.Value,  building.gameObject);
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
    

    void AddActionButton(string action, string sett, GameObject building)
    {
        
        NAction act = Data.nAction[action];

        if (act == null)
        {
            throw new MissingMemberException($"Action {action} with setting {sett} is missing.");
        }
        
        //can add under construction?
        if (building.GetComponent<BuildingInfo>().IsUnderConstrution() && !act.useUnderConstruction)
        {
            return;
        }
        
        //can add final?
        if (!ReqHelper.CheckOnlyFinal(PlayerMgmt.ActPlayer(), ActionHelper.GenReq(act), building.gameObject,
            (int) building.transform.position.x, (int) building.transform.position.y))
        {
            return;
        }

        
        GameObject button = UIElements.CreateImageButton(act.icon, actions.transform);
        button.GetComponent<Button>().onClick.AddListener(() =>
        {
            NLib.GetAction(act.id).ButtonBuildingRun(building, (int) building.transform.position.x, (int) building.transform.position.y, 
                sett);
        });
        UIHelper.HoverEnter(infotext.GetComponent<Text>(),act.name,button,
            () => { SetPanelMessage(building.GetComponent<BuildingInfo>().GetStatus()); });
    }
    
    public void SetPanelMessage(string text)
    {
        infotext.GetComponent<Text>().text = text;
    }
}
