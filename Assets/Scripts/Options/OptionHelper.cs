using System;
using DataTypes;
using InputAction;
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
            if (Data.features.debug.Bool())
            {
                b.AddElement(new DebugOptionSplitElement());
            }
            b.AddElement(new StatisticSplitElement());

            b.Finish();
        }
    }
}