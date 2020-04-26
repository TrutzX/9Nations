using Audio;
using IngameDebugConsole;
using Libraries;
using UnityEngine;
using UnityEngine.UI;

namespace Classes.Options
{
    public class OptionUiScale : ScriptableObject, IRun
    {
        public void Run()
        {
            GameObject.Find("UICanvas").GetComponent<CanvasScaler>().scaleFactor = LSys.tem.options[ID()].Int() / 10f;
        }

        public string ID()
        {
            return "uiScale";
        }
    }
}