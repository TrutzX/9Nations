using System;
using Game;
using InputActions;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Libraries.Inputs
{
    [Serializable]
    public class InputKey : BaseData
    {
        [NonSerialized] private KeyCode _keyCode = UnityEngine.KeyCode.F15;
        public string key;
        public string usedKey;
        public string type;

        [NonSerialized] private Button activeButton;

        public KeyCode KeyCode()
        {
            if (_keyCode == UnityEngine.KeyCode.F15)
            {
                usedKey = PlayerPrefs.GetString("input."+id,key);
                Enum.TryParse<KeyCode>(usedKey, true, out _keyCode);
            }

            return _keyCode;
        }
        
        public void BuildPanel(PanelBuilder panel)
        {
            if (IsGameButton())
            {
                activeButton = panel.AddImageTextButton(Name(), L.b.gameButtons[id].Icon, (() => { new InputKeyChange(this); }), Sound);
                return;
            }
            
            if (IsAction())
            {
                activeButton = panel.AddImageTextButton(Name(), L.b.actions[id].Icon, (() => { new InputKeyChange(this); }), Sound);
                return;
            }

            activeButton = panel.AddImageTextButton(Name(), Sprite(), (() => { new InputKeyChange(this); }), Sound);
        }
        
        public void SetNewKey(string usedKey)
        {
            this.usedKey = usedKey;
            S.InputAction().findKey = null;
            Enum.TryParse(usedKey, true, out _keyCode);
            UIHelper.UpdateButtonText(activeButton, Name());
            PlayerPrefs.SetString("input."+id,usedKey);
            PlayerPrefs.Save();
        }
        
        public override string Name()
        {
            if (IsGameButton())
            {
                name = L.b.gameButtons[id].Name();
            } else if (IsAction())
            {
                name = L.b.actions[id].Name();
            }

            return $"{name} ({usedKey})";
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