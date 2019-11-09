using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace DataTypes
{
    public partial class Campaign
    {
        public void ShowDetail(PanelBuilder panel)
        {
            panel.AddHeaderLabel(name);
            panel.AddLabel(desc);
            panel.AddHeaderLabel("Scenarios");
            foreach (Scenario s in Scenarios())
            {
                panel.AddImageTextButton(s.name, SpriteHelper.LoadIcon(s.icon), (() => s.Start()));
            }
        }

        public List<Scenario> Scenarios()
        {
            List<Scenario> list = new List<Scenario>();
            foreach (Scenario s in Data.scenario)
            {
                if (s.campaign == id)
                    list.Add(s);
            }
            
            return list;
        }
    }
}
