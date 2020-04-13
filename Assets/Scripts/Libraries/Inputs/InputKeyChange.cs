using Game;
using UI;
using UnityEngine.UI;

namespace Libraries.Inputs
{
    public class InputKeyChange
    {
        private InputKey _key;
        private string _newKey;
        
        public InputKeyChange(InputKey key)
        {
            this._key = key;
            
            WindowPanelBuilder wpb = WindowPanelBuilder.Create("Set new Key");
            wpb.panel.AddLabel($"Press a new key for {key.Name()}");
            Button change = wpb.panel.AddButton("Press any new key", () =>
            {
                key.SetNewKey(_newKey);
                S.InputAction().findKey = null;
                wpb.Close();
            });
            change.enabled = false;
            wpb.panel.AddButton($"Reset to {key.key}", () =>
            {
                key.SetNewKey(key.key);
                S.InputAction().findKey = null;
                wpb.Close();
            });
            wpb.AddClose();
            wpb.Finish();
            
            S.InputAction().findKey = code => { _newKey = code.ToString(); change.enabled = true; UIHelper.UpdateButtonText(change, $"Set new key to {_newKey}"); };
        }
        
    }
}