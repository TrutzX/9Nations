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

    public static void DebugMenu()
    {
        //create it
        WindowPanelBuilder p = WindowPanelBuilder.Create("Debug Window");
        p.panel.AddButton("Give 500 gold", () =>
        {
            Town t = TownMgmt.Get().GetByActPlayer().First();
            
            t.AddRes("gold",500);
        });
        p.panel.AddButton("Trade", () =>
        {
            Town t = TownMgmt.Get().GetByActPlayer()[0];

            NLib.GetAction("trade").QuestRun(PlayerMgmt.ActPlayer(), null);
        });
        p.panel.AddButton("Give research", () =>
        {
            Town t = TownMgmt.Get().GetByActPlayer()[0];
            
            t.AddRes("research",100);
        });
        p.panel.AddButton("Switch Fog", () =>
        {
            PlayerMgmt.ActPlayer().fog.tilemap.gameObject.SetActive(!PlayerMgmt.ActPlayer().fog.tilemap.gameObject.activeSelf);
        });
        p.panel.AddButton("Player features", () =>
        {
            WindowPanelBuilder wp = WindowPanelBuilder.Create("features");
            foreach (FeaturePlayer fp in Data.featurePlayer)
            {
                wp.panel.AddLabel(fp.name+": "+PlayerMgmt.ActPlayer().GetFeature(fp.id)+" ("+fp.standard+")");
            }
            wp.Finish();
        });
        p.panel.AddButton("Set first player active", () =>
        {
            PlayerMgmt.Get().ResetRound();
        });
        p.Finish();
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
