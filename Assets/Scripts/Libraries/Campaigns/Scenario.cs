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
            GameMgmt.startConfig = new Dictionary<string, string>();
            GameMgmt.startConfig["type"] = "scenario";
            GameMgmt.startConfig["scenario"] = id;
            GameMgmt.startConfig["name"] = Name()+" scenario";
            GameMgmt.Init();
        }

        public IRun ScenarioRun()
        {
            return LClass.s.scenarioRuns[id];
        }
    }
}