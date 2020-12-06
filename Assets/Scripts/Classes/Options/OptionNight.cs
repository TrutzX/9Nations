using Audio;
using Game;
using Libraries;
using UnityEngine;

namespace Classes.Options
{
    public class OptionNight : ScriptableObject, IRun
    {
        public void Run()
        {
            if (S.Game())
            {
                //FindObjectOfType<LightingManager2D>().enabled = LSys.tem.options[ID()].Bool();
            }
        }

        public string ID()
        {
            return "night";
        }
    }
}