using System;
using Classes;
using Game;
using Libraries;
using Libraries.Inputs;
using Libraries.Translations;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Options
{
    public static class OptionHelper
    {
        public static void ShowOptionWindow()
        {
            
            //load buildings
            WindowBuilderSplit b = WindowBuilderSplit.Create("Options",null);

            b.Add(new BaseOptionSplitElement("game","game", "logo"));
            b.Add(new BaseOptionSplitElement("audio", "audio", "audio"));
            b.Add(new BaseOptionSplitElement("graphics", "graphics", "graphics"));
            b.Add(new LanguageOptionSplitElement());
            b.Add(new NetworkOptionSplitElement());
            
            //todo dyn
            if (Application.platform != RuntimePlatform.Android)
                b.Add(new InputOptionSplitElement());
            
            if (S.Debug())
            {
                b.Add(new BaseOptionSplitElement("debug","debug", "debug"));
            }

            b.Finish();
        }

        public static void RunStartOptions()
        {
            foreach (var o in LSys.tem.options.GetAllByCategory("start"))
            {
                if (LClass.s.optionRuns.ContainsKey(o.id))
                {
                    LClass.s.optionRuns[o.id].Run();
                }
            }
        }
    }
}