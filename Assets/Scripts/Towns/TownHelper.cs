using System;
using System.Collections.Generic;
using DataTypes;
using Help;
using reqs;
using UI;
using UnityEngine;

namespace Towns
{
    public class TownHelper
    {

        public static void ShowTownWindow()
        {
            
            //load buildings
            WindowBuilderSplit b = WindowBuilderSplit.Create("Town window",null);

            foreach (Town t in TownMgmt.Get().GetByActPlayer())
            {
                b.AddElement(new TownSplitElement(t));

                if (Data.features.debug.Bool())
                {
                    b.AddElement(new DebugTownSplitElement(t));
                }
            }
            
            b.AddElement(new HelpSplitElement("town"));
            b.Finish();
        }
    }
}