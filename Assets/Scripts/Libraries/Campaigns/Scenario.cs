using System;
using System.Collections.Generic;
using Classes;

using Game;
using Tools;
using UI;

namespace Libraries.Campaigns
{
    [Serializable]
    public class Scenario : BaseData
    {
        public string map;

        public void Start()
        {
            GameMgmt.StartConfig = new Dictionary<string, string>();
            GameMgmt.StartConfig["type"] = "scenario";
            GameMgmt.StartConfig["scenario"] = id;
            GameMgmt.StartConfig["name"] = Name()+" scenario";
            GameMgmt.Init();
        }

        public IRun ScenarioRun()
        {
            return LClass.s.scenarioRuns[id];
        }
    }
}