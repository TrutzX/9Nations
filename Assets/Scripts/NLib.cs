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
        addAction("destroy",CreateInstance<DestroyAction>());
        addAction("build",CreateInstance<BuildAction>());
        addAction("buildUpgrade",CreateInstance<BuildUpgradeAction>());
        addAction("foundTown",CreateInstance<FoundTownAction>());
        addAction("train",CreateInstance<TrainAction>());
        addAction("trainUpgrade",CreateInstance<TrainUpgradeAction>());
        addAction("endGameLose",CreateInstance<EndGameLoseAction>());
        addAction("endGameWin",CreateInstance<EndGameWinAction>());
        addAction("trade",CreateInstance<TradeAction>());
        
        self.req = new Dictionary<string, BaseReq>();
        addReq("nation", CreateInstance<ReqNation>());
        addReq("terrain", CreateInstance<ReqTerrain>());
        addReq("terrainNear", CreateInstance<ReqTerrainNear>());
        addReq("season", CreateInstance<ReqSeason>());
        addReq("daytime", CreateInstance<ReqDayTime>());
        addReq("empty", CreateInstance<ReqEmpty>());
        addReq("townLevel", CreateInstance<ReqTownLevel>());
        addReq("townMin", CreateInstance<ReqTownMin>());
        addReq("townMax", CreateInstance<ReqTownMax>());
        addReq("townNear", CreateInstance<ReqTownNear>());
        addReq("ressMin", CreateInstance<ReqTownNear>());
        addReq("maxUnitPlayer", CreateInstance<ReqUnitMaxPlayer>());
        addReq("field", CreateInstance<ReqSameField>());
    }

    private static void addAction(string id, BaseAction action)
    {
        self.actions[id] = action;
        self.actions[id].id = id;
    }

    private static void addReq(string id, BaseReq action)
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
