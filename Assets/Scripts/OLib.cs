﻿using System;
using System.Collections;
using System.Collections.Generic;
using Actions;
using DataTypes;
using Endless;
using Libraries.Elements;
using Libraries.FActions;
using MapActions;
using MapActions.Actions;
using reqs;
using UnityEngine;
using UnityEngine.EventSystems;

public class OLib : ScriptableObject
{
    
    private static OLib self;
    private Dictionary<string,BaseAction> _oActions;
    public Dictionary<string,BaseMapAction> mapActions;
    private Dictionary<string,BaseReq> _req;

    public static OLib Get()
    {
        if (self == null)
            Init();
        return self;
    }
    
    // Start is called before the first frame update
    private static void Init()
    {
        self = CreateInstance<OLib>();
        self._oActions = new Dictionary<string,BaseAction>();
        AddOldAction("trade",CreateInstance<TradeAction>());
        AddOldAction("improvement",CreateInstance<ImprovementAction>());
        
        self._req = new Dictionary<string, BaseReq>();
        AddReq("nation", CreateInstance<ReqNation>());
        AddReq("terrain", CreateInstance<ReqTerrain>());
        AddReq("terrainCategory", CreateInstance<ReqTerrainCategory>());
        AddReq("terrainNear", CreateInstance<ReqTerrainNear>());
        AddReq("season", CreateInstance<ReqSeason>());
        AddReq("daytime", CreateInstance<ReqDayTime>());
        AddReq("empty", CreateInstance<ReqEmpty>());
        AddReq("notEmpty", CreateInstance<ReqNotEmpty>());
        AddReq("townLevel", CreateInstance<ReqTownLevel>());
        AddReq("townMin", CreateInstance<ReqOldTownMin>());
        AddReq("townMax", CreateInstance<ReqOldTownMax>());
        AddReq("townNear", CreateInstance<ReqTownNear>());
        AddReq("questMin", CreateInstance<ReqOldQuestMin>());
        AddReq("questMax", CreateInstance<ReqOldQuestMax>());
        AddReq("resMin", CreateInstance<ReqResMin>());
        AddReq("maxUnitPlayer", CreateInstance<ReqUnitMaxPlayer>());
        AddReq("field", CreateInstance<ReqSameField>());
        AddReq("research", CreateInstance<ReqResearch>());
        AddReq("feature", CreateInstance<ReqFeature>());
        AddReq("featureP", CreateInstance<ReqFeaturePlayer>());
        AddReq("building", CreateInstance<ReqBuilding>());
        AddReq("hp", CreateInstance<ReqHp>());
        AddReq("ap", CreateInstance<ReqAp>());
        AddReq("round", CreateInstance<ReqRoundCount>());
        AddReq("unitOwn", CreateInstance<ReqUnitOwn>());
        AddReq("unit", CreateInstance<ReqUnit>());
        AddReq("unitCount", CreateInstance<ReqUnitCount>());
        AddReq("town", CreateInstance<ReqTown>());
        AddReq("townCount", CreateInstance<ReqTown>());
        AddReq("questFinish", CreateInstance<ReqQuestFinish>());
        AddReq("fogField", CreateInstance<ReqFogField>());
        AddReq("disabled", CreateInstance<ReqDisabled>());
        AddReq("element", CreateInstance<ReqElement>());
        AddReq("res", CreateInstance<ReqRes>());
        AddReq("upgrade", CreateInstance<ReqUpgradeCan>());
        AddReq("saveFileCount", CreateInstance<ReqSaveFileCount>());
        AddReq("update", CreateInstance<ReqUpdate>());
        
        self.mapActions = new Dictionary<string, BaseMapAction>();
        AddMapAction("leave",CreateInstance<LeaveAction>());
        AddMapAction("heal",CreateInstance<HealAction>());
        AddMapAction("attack",CreateInstance<AttackAction>());
    }
    
    public static void AddOldAction(string id, BaseAction action)
    {
        self._oActions[id] = action;
        action.id = id;
    }

    private static void AddMapAction(string id, BaseMapAction action)
    {
        self.mapActions[id] = action;
        action.id = id;
    }

    private static void AddReq(string id, BaseReq action)
    {
        self._req[id] = action;
        self._req[id].id = id;
    }

    public static BaseAction GetOldAction(string key)
    {
        if (!Get()._oActions.ContainsKey(key))
        {
            throw new MissingMemberException($"Old action {key} is missing");
        }

        return self._oActions[key];
    }

    public static BaseReq GetReq(string key)
    {
        if (!Get()._req.ContainsKey(key))
        {
            throw new MissingMemberException($"Req {key} is missing");
        }

        return self._req[key];
    }
}
