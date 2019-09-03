using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Actions;
using DataTypes;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class OnMapUI : MonoBehaviour
{
    private static OnMapUI self;

    public UnitUI unitUI;
    public BuildingUI buildingUI;

    public static OnMapUI Get()
    {
        return self;
    }

    public TextMeshProUGUI ressround;
    public Text menudesc;
    
    // Start is called before the first frame update
    void Start()
    {
        self = this;
    }

    public void SetRessRoundMessage(string text)
    {
        ressround.text = text;
    }

    public void SetMenuMessage(string text)
    {
        menudesc.text = text;
    }
    

    public void UpdatePanelXY(int x, int y)
    {
        unitUI.UpdateUnitPanel(UnitMgmt.At(x,y));
        buildingUI.UpdatePanel(BuildingMgmt.At(x,y));
    }

    public void UpdatePanelOnMouse()
    {
        Vector2 p = MapMgmt.GetMouseMapXY();
        UpdatePanelXY((int) p.x, (int) p.y);
    }
}
