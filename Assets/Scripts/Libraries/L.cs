﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Classes;
using Help;
using Improvements;
using Libraries.Buildings;
using Libraries.Campaigns;
using Libraries.Elements;
using Libraries.FActions;
using Libraries.FActions.General;
using Libraries.FightModis;
using Libraries.GameButtons;
using Libraries.GameOptions;
using Libraries.Icons;
using Libraries.Inputs;
using Libraries.MapGenerations;
using Libraries.Maps;
using Libraries.Movements;
using Libraries.Nations;
using Libraries.Options;
using Libraries.PlayerOptions;
using Libraries.Res;
using Libraries.Researches;
using Libraries.Rounds;
using Libraries.Terrains;
using Libraries.Units;
using Libraries.Usages; 
using Loading;
using Maps;
using Modifiers;
using ModIO;
using Newtonsoft.Json.Utilities;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.SceneManagement;
 
namespace Libraries
{
    [Serializable]
    public class L : LBase
    {
        public static L b;
        
        public ModifierMgmt modifiers;
        public TerrainMgmt terrains;
        public NationMgmt nations;
        public ImprovementMgmt improvements;
        public ElementMgmt elements;
        public RoundMgmt rounds;
        public DataBuildingMgmt buildings;
        public DataUnitMgmt units;
        public UsageMgmt usages;
        public ResourceMgmt res;
        public FActionMgmt actions;
        public ResearchMgmt researches;
        public MovementMgmt movements;
        public GameButtonMgmt gameButtons;
        public FightModiMgmt fightModis;
        public GameOptionMgmt gameOptions;
        public PlayerOptionMgmt playerOptions;
        
        /// <summary>
        /// For save only
        /// </summary>
        public L() : base(null)
        {
        }
        
        public L(string file) : base(file)
        {
        }
        
        public static IEnumerator Init()
        {
            b = new L("game");
            yield return b.Loading();
        }

        protected override IEnumerator Loading()
        {
            mgmts = new Dictionary<string, IRead>();
            terrains = (TerrainMgmt) Add(new TerrainMgmt());
            modifiers = (ModifierMgmt) Add(new ModifierMgmt());
            nations = (NationMgmt) Add(new NationMgmt());
            improvements = (ImprovementMgmt) Add(new ImprovementMgmt());
            elements = (ElementMgmt) Add(new ElementMgmt());
            rounds = (RoundMgmt) Add(new RoundMgmt());
            buildings = (DataBuildingMgmt) Add(new DataBuildingMgmt());
            units = (DataUnitMgmt) Add(new DataUnitMgmt());
            usages = (UsageMgmt) Add(new UsageMgmt());
            res = (ResourceMgmt) Add(new ResourceMgmt());
            actions = (FActionMgmt) Add(new FActionMgmt());
            researches = (ResearchMgmt) Add(new ResearchMgmt());
            movements = (MovementMgmt) Add(new MovementMgmt());
            gameButtons = (GameButtonMgmt) Add(new GameButtonMgmt());
            fightModis = (FightModiMgmt) Add(new FightModiMgmt());
            gameOptions = (GameOptionMgmt) Add(new GameOptionMgmt());
            playerOptions = (PlayerOptionMgmt) Add(new PlayerOptionMgmt());
            
            yield return base.Loading();
        }
    }
}
