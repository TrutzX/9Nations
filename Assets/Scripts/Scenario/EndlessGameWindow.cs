using System;
using System.Collections;
using System.Collections.Generic;
using DataTypes;
using Game;
using Players;
using Scenario;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EndlessGameWindow : MonoBehaviour
{
    public Map selectedMap;
    public GameObject startButton;
    public Dictionary<string, string> startConfig;
    private WindowBuilderSplit window;

    public static EndlessGameWindow Get()
    {
        return GameObject.Find("EndlessGame").GetComponent<EndlessGameWindow>();
    }
    
    // Start is called before the first frame update
    public void Show()
    {
        startConfig = new Dictionary<string, string>();
        
        window = WindowBuilderSplit.Create("Endless game",null);
        //add start button
        startButton = UIHelper.CreateButton("",window.buttonPanel.transform,()=>
        {
            GameMgmt.startConfig = startConfig;
            GameMgmt.startConfig["type"] = "endless";
            
            GameMgmt.Init();
        });
        UpdateButtonText();

        window.AddElement(new MapSelectSplitElement(this));
        
        window.Finish();
        
    }

    public void UpdateButtonText()
    {
        startButton.GetComponent<Button>().enabled = false;
        
        //has a map?
        if (selectedMap == null)
        {
            UIHelper.UpdateButtonText(startButton,$"Select a map first");
            return;
        }
        
        //has a player?
        if (!startConfig.ContainsKey("0name"))
        {
            UIHelper.UpdateButtonText(startButton,$"Add a player first");
            return;
        }
        
        UIHelper.UpdateButtonText(startButton,$"Play the game");
        
        startButton.GetComponent<Button>().enabled = true;
    }

    private class MapSelectSplitElement : WindowBuilderSplit.SplitElement
    {
        private EndlessGameWindow endless;
        private int id;
        
        public MapSelectSplitElement(EndlessGameWindow endless) : base("General", SpriteHelper.LoadIcon("base:map"))
        {
            this.endless = endless;
            id = 0;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            panel.AddButton("Add a new player", () =>
            {
                endless.startConfig[id + "name"] = "Player";
                endless.startConfig[id + "nation"] = "north";
                endless.startConfig[id + "winGold"] = "true";
                endless.startConfig[id + "loseKing"] = "true";
                endless.window.AddElement(new PlayerSelectSplitElement(endless,id++));
                endless.UpdateButtonText();
            });
            panel.AddHeaderLabel("Select a map");
            foreach (Map map in ScenarioMgmt.GetAllMaps())
            {
                panel.AddButton($"Select {map.name}", () =>
                {
                    endless.selectedMap = map;
                    endless.startConfig["map"] = map.name;
                    endless.UpdateButtonText();
                    UIHelper.UpdateButtonText(button,$"Play map {map.name}");
                });
            }
        }

        public override void Perform()
        {
            throw new System.NotImplementedException();
        }
    }

    private class PlayerSelectSplitElement : WindowBuilderSplit.SplitElement
    {
        private EndlessGameWindow endless;
        private int id;
        
        public PlayerSelectSplitElement(EndlessGameWindow endless, int id) : base("Player", SpriteHelper.LoadIcon("base:map"))
        {
            this.endless = endless;
            this.id = id;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            panel.AddHeaderLabel("Name");
            panel.AddInput("Name",endless.startConfig[id + "name"], s => { 
                endless.startConfig[id + "name"] = s;
                UIHelper.UpdateButtonText(button,s); 
            });

            List<string> ids = new List<string>();
            List<string> titles = new List<string>();
            foreach (Nation nation in Data.nation)
            {
                if (nation.hidden)
                    continue;
                
                ids.Add(nation.id);
                titles.Add(nation.name);
            }
            
            panel.AddHeaderLabel("Nation");
            panel.AddDropdown(ids.ToArray(), endless.startConfig[id + "nation"], titles.ToArray(),
                s => { endless.startConfig[id + "nation"] = s; });

            
            panel.AddHeaderLabel("How to win");
            panel.AddCheckbox(Boolean.Parse(endless.startConfig[id +"winGold"]), "Collect 1000 Gold", b =>
            {
                endless.startConfig[id +"winGold"] = b.ToString();
            });
            
            panel.AddHeaderLabel("How to lose");
            panel.AddCheckbox(Boolean.Parse(endless.startConfig[id +"loseKing"]), "Lose your king", b =>
            {
                endless.startConfig[id +"loseKing"] = b.ToString();
            });
        }

        public override void Perform()
        {
            throw new System.NotImplementedException();
        }
    }
}
