using Audio;
using Libraries;
using UnityEngine;

namespace Classes.Options
{
    public class OptionFullscreen : ScriptableObject, IRun
    {
        public void Run()
        {
            Screen.fullScreen = LSys.tem.options[ID()].Bool();
        }

        public string ID()
        {
            return "fullscreen";
        }
    }
}