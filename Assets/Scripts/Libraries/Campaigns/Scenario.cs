using System;
using System.Collections.Generic;
using Classes;
using DataTypes;
using Game;
using Tools;
using UI;

namespace Libraries.Campaigns
{
    [Serializable]
    public class Scenario : BaseData
    {
        public string campaign;
        public string map;

        public void ShowDetail(PanelBuilder panel)
        {
            panel.AddHeaderLabel(name);
            panel.AddLabel(Desc);
        }

        public void Start()
        {
            GameMgmt.StartConfig = new Dictionary<string, string>();
            GameMgmt.StartConfig["type"] = "scenario";
            GameMgmt.StartConfig["scenario"] = id;
            GameMgmt.StartConfig["name"] = name+" scenario";
            GameMgmt.Init();
        }

        public IScenarioRun ScenarioRun()
        {
            return LClass.s.scenarioRuns[id];
        }
    }
}