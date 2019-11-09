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
            GameMgmt.startConfig = new Dictionary<string, string>();
            GameMgmt.startConfig["type"] = "scenario";
            GameMgmt.startConfig["scenario"] = id;
            GameMgmt.startConfig["name"] = name+" scenario";
            GameMgmt.Init();
        }
    }
}
