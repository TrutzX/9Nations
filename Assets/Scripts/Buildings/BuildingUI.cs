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

public class BuildingUI : MapElementUI
{
    public OnMapUI onmapui;

    private BuildingInfo active;

    public GameObject info;
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
        SetPanelMessage(building.Status(PlayerMgmt.ActPlayerID()));
        
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
            AddActionButton(action.Key, action.Value,  building, actions);
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
    

    
    
    
}
