using System;

using Game;
using Libraries.Inputs;
using UI;
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
            if (S.Debug())
            {
                b.AddElement(new DebugOptionSplitElement());
            }

            b.Finish();
        }
    }
}