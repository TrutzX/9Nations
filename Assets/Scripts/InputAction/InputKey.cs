using System;
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
                panel.AddImageLabel(Name(), Data.gameButton[id].icon);
                return;
            }
            
            if (IsAction())
            {
                panel.AddImageLabel(Name(), Data.nAction[id].icon);
                return;
            }
            
            panel.AddLabel(Name());
        }

        public string Name()
        {
            string name = id;
            if (IsGameButton())
            {
                name = Data.gameButton[id].name;
            } else if (IsAction())
            {
                name = Data.nAction[id].name;
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