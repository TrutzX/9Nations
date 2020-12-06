using System;
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
            float scale;
            if (LSys.tem.options[ID()].Exist())
            {
                scale = LSys.tem.options[ID()].Int() / 10f;
            }
            else
            {
                scale = Screen.dpi > 160 || Screen.width >= 1920 ? 2 : 1;
                LSys.tem.options[ID()].SetValue(((int) (scale * 10)).ToString());
            }

            if (scale <= 0)
            {
                scale = 1;
                Debug.LogWarning($"ScaleFactor {scale} has the wrong value.");
            }
            
            GameObject.Find("UICanvas").GetComponent<CanvasScaler>().scaleFactor = scale;
            GameObject.Find("MainCamera").GetComponentInChildren<DebugLogManager>(true).GetComponent<CanvasScaler>().scaleFactor = scale;
        }

        public string ID()
        {
            return "uiScale";
        }
    }
}