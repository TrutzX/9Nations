using System;
using System.Collections;
using System.Collections.Generic;

using Endless;
using Libraries.Elements;
using Libraries.FActions;
using reqs;
using UnityEngine;
using UnityEngine.EventSystems;

public class OLib : ScriptableObject
{
    
    private static OLib self;
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
        
        self._req = new Dictionary<string, BaseReq>();
        AddReq("ap", CreateInstance<ReqAp>());
        AddReq("building", CreateInstance<ReqBuilding>());
        AddReq("daytime", CreateInstance<ReqDayTime>());
        AddReq("disabled", CreateInstance<ReqDisabled>());
        AddReq("dummy", CreateInstance<ReqDummy>());
        AddReq("element", CreateInstance<ReqElement>());
        AddReq("elementCount", CreateInstance<ReqElementCount>());
        AddReq("empty", CreateInstance<ReqEmpty>());
        AddReq("gameOption", CreateInstance<ReqGameOption>());
        AddReq("option", CreateInstance<ReqOption>());
        AddReq("featureP", CreateInstance<ReqFeaturePlayer>());
        AddReq("field", CreateInstance<ReqSameField>());
        AddReq("fightElement", CreateInstance<ReqFightElement>());
        AddReq("fightMovementType", CreateInstance<ReqFightMovementType>());
        AddReq("fightNationEthos", CreateInstance<ReqFightNationEthos>());
        AddReq("fightUnitType", CreateInstance<ReqFightUnitType>());
        AddReq("fogField", CreateInstance<ReqFogField>());
        AddReq("hp", CreateInstance<ReqHp>());
        AddReq("game", CreateInstance<ReqGame>());
        AddReq("windowOpen", CreateInstance<ReqWindowOpen>());
        AddReq("gameActiveElement", CreateInstance<ReqGameActiveElement>());
        AddReq("mapLevel", CreateInstance<ReqMapLevelCount>());
        AddReq("maxUnitPlayer", CreateInstance<ReqUnitMaxPlayer>());
        AddReq("nation", CreateInstance<ReqNation>());
        AddReq("notEmpty", CreateInstance<ReqNotEmpty>());
        AddReq("platform", CreateInstance<ReqPlatform>());
        AddReq("questCount", CreateInstance<ReqQuestCount>());
        AddReq("questFinish", CreateInstance<ReqQuestFinish>());
        AddReq("res", CreateInstance<ReqRes>());
        AddReq("research", CreateInstance<ReqResearch>());
        AddReq("round", CreateInstance<ReqRoundCount>());
        AddReq("saveFileCount", CreateInstance<ReqSaveFileCount>());
        AddReq("season", CreateInstance<ReqSeason>());
        AddReq("terrain", CreateInstance<ReqTerrain>());
        AddReq("terrainCategory", CreateInstance<ReqTerrainCategory>());
        AddReq("terrainNear", CreateInstance<ReqTerrainNear>());
        AddReq("terrainRes", CreateInstance<ReqTerrainRes>());
        AddReq("townCount", CreateInstance<ReqTownCount>());
        AddReq("townLevel", CreateInstance<ReqTownLevel>());
        AddReq("townNear", CreateInstance<ReqTownNear>());
        AddReq("unit", CreateInstance<ReqUnit>());
        AddReq("unitCount", CreateInstance<ReqUnitCount>());
        AddReq("unitDest", CreateInstance<ReqUnitDest>());
        AddReq("unitOwn", CreateInstance<ReqUnitOwn>());
        AddReq("update", CreateInstance<ReqUpdate>());        
        AddReq("upgrade", CreateInstance<ReqUpgradeCan>());
        AddReq("frontier", CreateInstance<ReqFrontier>());
    }

    private static void AddReq(string id, BaseReq action)
    {
        self._req[id] = action;
        self._req[id].id = id;
    }

    public static BaseReq GetReq(string key)
    {
        if (!Get()._req.ContainsKey(key))
        {
            Debug.LogError($"Req {key} is missing");
            return self._req["dummy"];
        }

        return self._req[key];
    }
}
