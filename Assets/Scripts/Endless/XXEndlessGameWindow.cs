using System;
using System.Collections.Generic;

using Game;
using Libraries.Maps;
using Maps;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Endless
{
    [Obsolete]
    public class XXEndlessGameWindow : MonoBehaviour
    {
        public DataMap selectedMap;
        public Button startButton;
        public Dictionary<string, string> startConfig;
        public WindowBuilderSplit window;
    
        // Start is called before the first frame update
        public void Show()
        {
            startConfig = new Dictionary<string, string>();
        
            window = WindowBuilderSplit.Create("Endless game",null);
            //add start button
            startButton = UIHelper.CreateButton("",window.buttonPanel.transform,()=>
            {
                GameMgmt.StartConfig = startConfig;
                GameMgmt.StartConfig["name"] = "endless game";
                GameMgmt.StartConfig["type"] = "endless";
            
                GameMgmt.Init();
            });
            UpdateButtonText();

            //window.AddElement(new GeneralSplitElement(this));
        
            window.Finish();
        
        }

        public void SetMap(DataMap map)
        {
            selectedMap = map;
            startConfig["map"] = map.id;
            UpdateButtonText();
            //GeneralSplitElement gs = window.
            //UIHelper.UpdateButtonText(button,$"Play map {map.Name}");
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
    }
}
