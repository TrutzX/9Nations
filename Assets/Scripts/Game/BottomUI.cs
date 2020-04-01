using Buildings;
using UnityEngine;

namespace Game
{
    public class BottomUI : IMapUI
    {
        public void ShowPanelMessage(string text)
        {
            OnMapUI.Get().bottomButtonText.color = Color.white;
            OnMapUI.Get().bottomButtonText.text = text;
        }
        
        public void ShowPanelMessageError(string text)
        {
            OnMapUI.Get().bottomButtonText.color = Color.magenta;
            OnMapUI.Get().bottomButtonText.text = text;
            NAudio.PlayBuzzer();
        }
    }
}