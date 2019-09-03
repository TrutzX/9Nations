using System;
using System.Collections.Generic;
using DataTypes;
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
            }

            b.Finish();
        }

        public class TownSplitElement : WindowBuilderSplit.SplitElement
        {
            protected Town town;
            public TownSplitElement(Town town) : base(town.name, town.GetIcon())
            {
                this.town = town;
            }

            public override void ShowDetail(PanelBuilder panel)
            {
                town.ShowInfo(panel);
            }

            public override void Perform()
            {
                Debug.LogWarning("Not implemented");
            }
        }
    }
}