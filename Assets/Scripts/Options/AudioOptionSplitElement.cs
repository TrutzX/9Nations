using UI;
using UnityEngine;

namespace Options
{
    public class AudioOptionSplitElement : SplitElement
    {
        public AudioOptionSplitElement() : base("Audio", SpriteHelper.Load("base:audio"))
        {
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            panel.AddHeaderLabel("Background music");
            panel.AddSlider(0, 100, PlayerPrefs.GetInt("audio.music",50), s =>
            {
                PlayerPrefs.SetInt("audio.music", s); 
                PlayerPrefs.Save(); 
                NAudio.Get().UpdateAudio(); 
            });
                
            panel.AddHeaderLabel("Sounds");
            panel.AddSlider(0, 100, PlayerPrefs.GetInt("audio.sound",75), s =>
            {
                PlayerPrefs.SetInt("audio.sound", s); 
                PlayerPrefs.Save();
                NAudio.Get().UpdateAudio(); 
            });
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}