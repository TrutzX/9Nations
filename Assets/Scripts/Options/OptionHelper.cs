using System;
using DataTypes;
using InputAction;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Options
{
    public class OptionHelper
    {
        public static void ShowOptionWindow()
        {
            
            //load buildings
            WindowBuilderSplit b = WindowBuilderSplit.Create("Options",null);

            b.AddElement(new AudioOptionSplitElement());
            b.AddElement(new GameOptionSplitElement());
            b.AddElement(new NetworkOptionSplitElement());
            b.AddElement(new InputOptionSplitElement());

            b.Finish();
        }

        private class AudioOptionSplitElement : SplitElement
        {
            public AudioOptionSplitElement() : base("Audio", SpriteHelper.LoadIcon("base:audio"))
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
        
        private class NetworkOptionSplitElement : SplitElement
        {
            public NetworkOptionSplitElement() : base("Network", SpriteHelper.LoadIcon("magic:privacy"))
            {
            }

            public override void ShowDetail(PanelBuilder panel)
            {
                panel.AddCheckbox(true, "Send errors automatically", s => { });
                panel.AddCheckbox(true, "Send diagnostics", s => { }).interactable = false;
                panel.AddLabel(
                    "If you do not want, to send the infos, please disable the network");
                panel.AddLabel(
                    "or use a firewall. See also the privacy statement in the help menu");

            }

            public override void Perform()
            {
                Debug.LogWarning("Not implemented");
            }
        }
        
        private class GameOptionSplitElement : SplitElement
        {
            public GameOptionSplitElement() : base("Game", SpriteHelper.LoadIcon("logo"))
            {
            }

            public override void ShowDetail(PanelBuilder panel)
            {
                Data.features.autosave.AddOption(panel);
                Data.features.centermouse.AddOption(panel);
                Data.features.debug.AddOption(panel);
                
            }

            public override void Perform()
            {
                Debug.LogWarning("Not implemented");
            }
        }
    }
}