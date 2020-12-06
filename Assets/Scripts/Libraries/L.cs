using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Classes;
using Help;
using Improvements;
using Libraries.Animations;
using Libraries.Buildings;
using Libraries.Campaigns;
using Libraries.Coats;
using Libraries.Crafts;
using Libraries.Elements;
using Libraries.FActions;
using Libraries.FActions.General;
using Libraries.FightModis;
using Libraries.GameButtons;
using Libraries.GameOptions;
using Libraries.Icons;
using Libraries.Inputs;
using Libraries.Items;
using Libraries.MapGenerations;
using Libraries.Maps;
using Libraries.Modifiers;
using Libraries.Movements;
using Libraries.Nations;
using Libraries.Options;
using Libraries.Overlays;
using Libraries.PlayerOptions;
using Libraries.Res;
using Libraries.Researches;
using Libraries.Rounds;
using Libraries.Spells;
using Libraries.Terrains;
using Libraries.Units;
using Libraries.Usages; 
using Loading;
using Maps;
using ModIO;
using Newtonsoft.Json.Utilities;
using UnityEngine;
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
        public OverlayMgmt overlays;
        public CraftMgmt crafts;
        public CoatMgmt coats;
        public ItemMgmt items;
        public SpellMgmt spells;
        public AnimationMgmt animations;
        
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
        
        public IEnumerator Reload()
        {
            yield return base.Loading();
            LSys.tem.Load.FinishLoading();
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
            crafts = (CraftMgmt) Add(new CraftMgmt());
            overlays = (OverlayMgmt) Add(new OverlayMgmt());
            coats = (CoatMgmt) Add(new CoatMgmt());
            items = (ItemMgmt) Add(new ItemMgmt());
            spells = Add<SpellMgmt>();
            animations = Add<AnimationMgmt>();
            
            yield return base.Loading();
        }
    }
}
