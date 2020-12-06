using Audio;
using UnityEngine;

namespace Classes.Options
{
    public class OptionAudioMusic : ScriptableObject, IRun
    {
        public void Run()
        {
            NAudio.Get().UpdateAudio();
        }

        public string ID()
        {
            return "audioMusic";
        }
    }
}