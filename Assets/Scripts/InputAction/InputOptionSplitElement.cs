using DataTypes;
using UI;
using UnityEngine;

namespace InputAction
{
    public class InputOptionSplitElement: SplitElement
    {
        public InputOptionSplitElement() : base("Keys", SpriteHelper.LoadIcon("ui:key"))
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
                key.BuildPanel(panel);
            }
                
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}