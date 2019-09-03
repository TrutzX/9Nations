using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Game;
using LoadSave;
using Players;
using Towns;
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

    public bool TryDestroyWindow(string title)
    {
        GameObject go = GameObject.Find(title);
        if (go != null)
        {
            Destroy(go);
            return true;
        }

        return false;
    }

    public void SwitchMainMenu()
    {
        //is open?
        if (TryDestroyWindow("Main menu"))
        {
            return;
        }
        
        //create it
        WindowPanelBuilder win = WindowPanelBuilder.Create("Main menu");
        win.panel.AddButton("To Main menu", () =>
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        });
        win.panel.AddButton("Save game", SaveWindow.Show);
        win.panel.AddButton("Load game", LoadWindow.Show);
        win.Finish();
    }

    public void SwitchDebugMenu()
    {
        //is open?
        if (TryDestroyWindow("Debug Window"))
        {
            return;
        }
        
        //create it
        WindowPanelBuilder p = WindowPanelBuilder.Create("Debug Window");
        p.panel.AddButton("Give ress", () =>
        {
            Town t = TownMgmt.Get().GetByActPlayer()[0];
            
            t.AddRess("wood",15);
            t.AddRess("stone",150);
            t.AddRess("food",12);
            t.AddRess("tool",14);
        });
        p.Finish();
    }

    public void ShowTownMenu()
    {
        string n = "Town Window";
        //is open?
        if (TryDestroyWindow(n))
        {
            return;
        }
        
        //create it
        if (TownMgmt.Get().GetByActPlayer().Count == 0)
        {
            OnMapUI.Get().SetMenuMessage("No town found. Please found one before.");
            return;
        }

        TownHelper.ShowTownWindow();
    }

    public void ShowQuestMenu()
    {
        string n = "Quest window";
        //is open?
        if (TryDestroyWindow(n))
        {
            return;
        }
        
        //create it
        if (PlayerMgmt.ActPlayer().quests.quests.Count == 0)
        {
            OnMapUI.Get().SetMenuMessage("No quests found.");
            return;
        }

        QuestHelper.ShowQuestWindow();
    }

    public void ShowHelpMenu()
    {
        string n = "Help window";
        //is open?
        if (TryDestroyWindow(n))
        {
            return;
        }

        HelpHelper.ShowHelpWindow();
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
