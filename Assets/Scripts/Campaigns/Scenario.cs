using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

namespace DataTypes
{
    public partial class Scenario
    {
        public void ShowDetail(PanelBuilder panel)
        {
            panel.AddHeaderLabel(name);
            panel.AddLabel(desc);
        }

        public void Start()
        {
            GameMgmt.StartConfig = new Dictionary<string, string>();
            GameMgmt.StartConfig["type"] = "scenario";
            GameMgmt.StartConfig["scenario"] = id;
            GameMgmt.StartConfig["name"] = name+" scenario";
            GameMgmt.Init();
        }
    }
}
