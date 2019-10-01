using System;
using System.Collections;
using System.Collections.Generic;
using Actions;
using DataTypes;
using reqs;
using UnityEngine;
using UnityEngine.EventSystems;

public class NLib : ScriptableObject
{
    
    private static NLib self;
    public Dictionary<string,BaseAction> actions;
    public Dictionary<string,BaseReq> req;

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
        AddAction("foundTown",CreateInstance<FoundTownAction>());
        AddAction("train",CreateInstance<TrainAction>());
        AddAction("trainUpgrade",CreateInstance<TrainUpgradeAction>());
        AddAction("endGameLose",CreateInstance<EndGameLoseAction>());
        AddAction("endGameWin",CreateInstance<EndGameWinAction>());
        AddAction("trade",CreateInstance<TradeAction>());
        AddAction("sleep",CreateInstance<SleepAction>());
        AddAction("featureP",CreateInstance<FeaturePlayerAction>());
        
        self.req = new Dictionary<string, BaseReq>();
        AddReq("nation", CreateInstance<ReqNation>());
        AddReq("terrain", CreateInstance<ReqTerrain>());
        AddReq("terrainNear", CreateInstance<ReqTerrainNear>());
        AddReq("season", CreateInstance<ReqSeason>());
        AddReq("daytime", CreateInstance<ReqDayTime>());
        AddReq("empty", CreateInstance<ReqEmpty>());
        AddReq("townLevel", CreateInstance<ReqTownLevel>());
        AddReq("townMin", CreateInstance<ReqTownMin>());
        AddReq("townMax", CreateInstance<ReqTownMax>());
        AddReq("townNear", CreateInstance<ReqTownNear>());
        AddReq("questMin", CreateInstance<ReqQuestMin>());
        AddReq("questMax", CreateInstance<ReqQuestMax>());
        AddReq("resMin", CreateInstance<ReqResMin>());
        AddReq("maxUnitPlayer", CreateInstance<ReqUnitMaxPlayer>());
        AddReq("field", CreateInstance<ReqSameField>());
        AddReq("research", CreateInstance<ReqResearch>());
        AddReq("feature", CreateInstance<ReqFeature>());
        AddReq("featureP", CreateInstance<ReqFeaturePlayer>());
        AddReq("building", CreateInstance<ReqBuilding>());
    }

    private static void AddAction(string id, BaseAction action)
    {
        self.actions[id] = action;
        self.actions[id].id = id;
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
