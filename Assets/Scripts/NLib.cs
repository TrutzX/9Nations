using System;
using System.Collections;
using System.Collections.Generic;
using Actions;
using Campaigns;
using Campaigns.Scenarios;
using DataTypes;
using Endless;
using MapActions;
using MapActions.Actions;
using reqs;
using UnityEngine;
using UnityEngine.EventSystems;

public class NLib : ScriptableObject
{
    
    private static NLib self;
    public Dictionary<string,BaseAction> actions;
    public Dictionary<string,BaseMapAction> mapActions;
    public Dictionary<string,BaseReq> req;
    public Dictionary<string,IScenarioRun> ScenarioRuns;

    public static NLib Get()
    {
        if (self == null)
            Init();
        return self;
    }
    
    // Start is called before the first frame update
    private static void Init()
    {
        self = CreateInstance<NLib>();
        self.actions = new Dictionary<string,BaseAction>();
        AddAction("destroy",CreateInstance<DestroyAction>());
        AddAction("build",CreateInstance<BuildAction>());
        AddAction("buildUpgrade",CreateInstance<BuildUpgradeAction>());
        AddAction("foundTown",CreateInstance<FoundTownKillAction>());
        AddAction("foundTownFirst",CreateInstance<FoundTownAction>());
        AddAction("train",CreateInstance<TrainAction>());
        AddAction("trainUpgrade",CreateInstance<TrainUpgradeAction>());
        AddAction("endGameLose",CreateInstance<EndGameLoseOldAction>());
        AddAction("endGameWin",CreateInstance<EndGameWinOldAction>());
        AddAction("trade",CreateInstance<TradeOldAction>());
        AddAction("sleep",CreateInstance<SleepOldAction>());
        AddAction("featureP",CreateInstance<FeaturePlayerOldAction>());
        AddAction("cameraMove",CreateInstance<CameraMoveOldAction>());
        AddAction("gameButton",CreateInstance<GameButtonOldAction>());
        AddAction("move",CreateInstance<MoveAction>());
        AddAction("improvement",CreateInstance<ImprovementAction>());
        AddAction("townlevel",CreateInstance<TownLevelAction>());
        
        self.req = new Dictionary<string, BaseReq>();
        AddReq("nation", CreateInstance<ReqOldNation>());
        AddReq("terrain", CreateInstance<ReqOldTerrain>());
        AddReq("terrainNear", CreateInstance<ReqOldTerrainNear>());
        AddReq("season", CreateInstance<ReqOldSeason>());
        AddReq("daytime", CreateInstance<ReqOldDayTime>());
        AddReq("empty", CreateInstance<ReqEmpty>());
        AddReq("townLevel", CreateInstance<ReqTownLevel>());
        AddReq("townMin", CreateInstance<ReqOldTownMin>());
        AddReq("townMax", CreateInstance<ReqOldTownMax>());
        AddReq("townNear", CreateInstance<ReqOldTownNear>());
        AddReq("questMin", CreateInstance<ReqOldQuestMin>());
        AddReq("questMax", CreateInstance<ReqOldQuestMax>());
        AddReq("resMin", CreateInstance<ReqOldResMin>());
        AddReq("maxUnitPlayer", CreateInstance<ReqOldUnitMaxPlayer>());
        AddReq("field", CreateInstance<ReqOldSameField>());
        AddReq("research", CreateInstance<ReqOldResearch>());
        AddReq("feature", CreateInstance<ReqOldFeature>());
        AddReq("featureP", CreateInstance<ReqOldFeaturePlayer>());
        AddReq("building", CreateInstance<ReqBuilding>());
        AddReq("hp", CreateInstance<ReqHp>());
        AddReq("ap", CreateInstance<ReqAp>());
        AddReq("unit", CreateInstance<ReqUnit>());
        AddReq("town", CreateInstance<ReqTown>());
        AddReq("questFinish", CreateInstance<ReqQuestFinish>());
        AddReq("fogField", CreateInstance<ReqFogField>());
        AddReq("disabled", CreateInstance<ReqDisabled>());
        
        self.mapActions = new Dictionary<string, BaseMapAction>();
        AddMapAction("leave",CreateInstance<LeaveAction>());
        AddMapAction("heal",CreateInstance<HealAction>());
        AddMapAction("attack",CreateInstance<AttackAction>());
        
        self.ScenarioRuns = new Dictionary<string, IScenarioRun>();
        self.ScenarioRuns.Add("debug",new DebugScenario());
        self.ScenarioRuns.Add("endless",new EndlessGame());
        self.ScenarioRuns.Add("tutorialbasic",new TutorialBasicScenario());
    }

    private static void AddAction(string id, BaseAction action)
    {
        self.actions[id] = action;
        action.id = id;
    }

    private static void AddMapAction(string id, BaseMapAction action)
    {
        self.mapActions[id] = action;
        action.id = id;
    }

    private static void AddReq(string id, BaseReq action)
    {
        self.req[id] = action;
        self.req[id].id = id;
    }

    public static BaseAction GetAction(string key)
    {
        if (!Get().actions.ContainsKey(key))
        {
            throw new MissingMemberException($"Action {key} is missing");
        }

        return self.actions[key];
    }

    public static BaseReq GetReq(string key)
    {
        if (!Get().req.ContainsKey(key))
        {
            throw new MissingMemberException($"Req {key} is missing");
        }

        return self.req[key];
    }
}
