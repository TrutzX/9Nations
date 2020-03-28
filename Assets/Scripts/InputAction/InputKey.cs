using System;
using Libraries;
using UI;
using UnityEngine;

namespace DataTypes
{
    public partial class InputKey
    {
        private KeyCode _keyCode = UnityEngine.KeyCode.F15;
        public void BuildPanel(PanelBuilder panel)
        {
            if (IsGameButton())
            {
                panel.AddImageLabel(Name(), L.b.gameButtons[id].Icon);
                return;
            }
            
            if (IsAction())
            {
                panel.AddImageLabel(Name(), L.b.actions[id].Icon);
                return;
            }
            
            panel.AddLabel(Name());
        }

        public string Name()
        {
            string name = id;
            if (IsGameButton())
            {
                name = L.b.gameButtons[id].name;
            } else if (IsAction())
            {
                name = L.b.actions[id].name;
            }

            return $"{name} ({key})";
        }

        public KeyCode KeyCode()
        {
            if (_keyCode == UnityEngine.KeyCode.F15)
            {
                Enum.TryParse(key, true, out _keyCode);
                //Debug.LogWarning("Keycode for "+id+" is "+_keyCode);
            }

            return _keyCode;
        }

        public bool IsGameButton()
        {
            return type == "gameButton";
        }

        public bool IsAction()
        {
            return type == "action";
        }
    }
}