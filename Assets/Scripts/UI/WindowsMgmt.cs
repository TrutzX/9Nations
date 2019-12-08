using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataTypes;
using Help;
using Game;
using LoadSave;
using Options;
using Players;
using Towns;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WindowsMgmt : MonoBehaviour
{
    private static WindowsMgmt self;
    
    /// <summary>
    /// Get it
    /// </summary>
    /// <returns></returns>
    public static WindowsMgmt Get()
    {
        //return GameObject.Find("WindowsMgmt").GetComponentInChildren<WindowsMgmt>();
        return self;
    }

    private void Start()
    {
        self = this;
    }

    public static void GameMainMenu()
    {
        
        //create it
        WindowPanelBuilder win = WindowPanelBuilder.Create("Main menu");
        GameButtonHelper.BuildMenu(PlayerMgmt.ActPlayer(), "game", null, true, win.panel.panel.transform);
        win.Finish();
    }

    private bool IsOpen(string text)
    {
        return GameObject.Find(text) != null;
    }
    
    public GameObject GetOpenWindow()
    {
        foreach(DestroyGameObject d in transform.GetComponentsInChildren<DestroyGameObject>())
        {
            if (d.IsWindow)
                return d.gameObject;
        }
        return null;
    }
    
    public bool AnyOpenWindow()
    {
        return GetOpenWindow()!=null;
    }
}
