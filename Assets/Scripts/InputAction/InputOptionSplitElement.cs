using System;
using DataTypes;
using UI;
using UI.Show;
using UnityEngine;

namespace InputAction
{
    public class InputOptionSplitElement: SplitElement
    {
        public InputOptionSplitElement() : base("Keys", SpriteHelper.Load("ui:key"))
        {
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            string lastinput = null;
            //show all Keys
            foreach (InputKey key in Data.inputKey)
            {
                //show key?
                if (key.hidden)
                {
                    continue;
                }
                
                //show header?
                if (key.type != lastinput)
                {
                    lastinput = key.type;
                    switch (key.type)
                    {
                        case "gameButton":
                            panel.AddHeaderLabel("Game Button");
                            break;
                        case "action":
                            panel.AddHeaderLabel("Action");
                            break;
                        case "hidden":
                            panel.AddHeaderLabel("Another");
                            break;
                        default:
                            panel.AddHeaderLabel("Unknown");
                            break;
                    }
                }
                
                //show key
                try
                {
                    key.BuildPanel(panel);
                }
                catch (Exception e)
                {
                    Debug.Log(key);
                    Debug.Log(key.id);
                    Debug.Log(key.Name());
                    Debug.LogError(e);
                }
                
            }
                
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}