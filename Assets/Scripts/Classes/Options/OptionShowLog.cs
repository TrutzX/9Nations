using Audio;
using Game;
using IngameDebugConsole;
using Libraries;
using UnityEngine;

namespace Classes.Options
{
    public class OptionShowLog : ScriptableObject, IRun
    {
        public void Run()
        {
            //FindObjectOfType<DebugLogManager>().
            GameObject.Find("MainCamera").GetComponentInChildren<DebugLogManager>(true).gameObject.SetActive(S.Debug() && LSys.tem.options[ID()].Bool());
        }

        public string ID()
        {
            return "showLog";
        }
    }
}