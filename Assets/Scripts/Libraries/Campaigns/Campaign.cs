using System;
using System.Collections.Generic;
using System.Linq;

using Tools;
using UI;
using UnityEngine;

namespace Libraries.Campaigns
{
    [Serializable]
    public class Campaign : BaseData
    {
        public bool progress;

        public void ShowDetail(PanelBuilder panel)
        {
            panel.AddHeaderLabel(name);
            panel.AddLabel(Desc);
            panel.AddHeaderLabel("Scenarios");
            foreach (Scenario s in Scenarios())
            {
                if (s.req.Check(null))
                    panel.AddImageTextButton(s.name, SpriteHelper.Load(s.Icon), (() => s.Start()));
            }
        }

        public List<Scenario> Scenarios()
        {
            return LSys.tem.scenarios.GetAllByCategory(id);
        }
    }
}