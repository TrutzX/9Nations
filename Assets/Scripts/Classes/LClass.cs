using System;
using System.Collections.Generic;
using Classes.Actions;
using Classes.Elements;
using Classes.GameButtons;
using Classes.MapGenerator;
using Classes.NameGenerator;
using Classes.Options;
using Classes.Overlays;
using Classes.Scenarios;
using Endless;
using Game;
using Libraries.Campaigns;
using Libraries.Elements;
using Libraries.FActions;
using Libraries.GameButtons;
using Libraries.MapGenerations;
using Players;
using Players.Kingdoms;
using Players.PlayerTypes;
using UnityEngine;

namespace Classes
{
    public class LClass : ScriptableObject
    {
        public Dictionary<string,IRun> scenarioRuns;
        public Dictionary<string,IRun> optionRuns;
        public Dictionary<string,BaseElementRun> elementRuns;
        public Dictionary<string,BaseOverlay> overlaysRuns;
        private Dictionary<string,BaseGameButtonRun> _gameButtonRuns;
        public Dictionary<string,BaseMapGenerator> mapGenerators;
        public Dictionary<string,BasePlayerType> playerTypes;
        private Dictionary<string,BaseNameGenerator> _nameGenerators;
        private Dictionary<string,BasePerformAction> _actions;

        public static LClass s;
        public static void Init()
        {
            s = CreateInstance<LClass>();
            s.Init2();
        }
        
        private void Init2()
        {
            scenarioRuns = new Dictionary<string, IRun>();
            Add(scenarioRuns, new DebugScenario());
            Add(scenarioRuns, new EndlessGameScenario());
            Add(scenarioRuns, new Tutorial1Scenario());
            Add(scenarioRuns, new Tutorial2Scenario());
            Add(scenarioRuns, new PantheonScenario());
            Add(scenarioRuns, new ScreenshotScenario());
        
            optionRuns = new Dictionary<string, IRun>();
            Add(optionRuns, CreateInstance<OptionAudioMusic>());
            Add(optionRuns, CreateInstance<OptionAudioSound>());
            Add(optionRuns, CreateInstance<OptionFullscreen>());
            Add(optionRuns, CreateInstance<OptionShowLog>());
            Add(optionRuns, CreateInstance<OptionUiScale>());
            Add(optionRuns, CreateInstance<OptionNight>());
            Add(optionRuns, CreateInstance<OptionLanguage>());
            
            elementRuns = new Dictionary<string, BaseElementRun>();
            Add(CreateInstance<LightElement>());
            Add(CreateInstance<ShadowElement>());
            
            overlaysRuns = new Dictionary<string, BaseOverlay>();
            Add(new OwnerOverlay());
            Add(new BoundaryOverlay());
            Add(new FrontierOverlay());
            Add(new ResOverlay());
            
            _actions = new Dictionary<string, BasePerformAction>();
            Add(CreateInstance<ActionAttackUnit>());
            Add(CreateInstance<ActionAttackBuilding>());
            Add(CreateInstance<ActionBuild>());
            Add(CreateInstance<ActionCameraMove>());
            Add(CreateInstance<ActionCraft>());
            Add(CreateInstance<ActionDestroy>());
            Add(CreateInstance<ActionEvolve>());
            Add(CreateInstance<ActionExamine>());
            Add(CreateInstance<ActionExplore>());
            Add(CreateInstance<ActionFeaturePlayer>());
            Add(CreateInstance<ActionFoundTown>());
            Add(CreateInstance<ActionHeal>());
            Add(CreateInstance<ActionInteract>());
            Add(CreateInstance<ActionGameButton>());
            Add(CreateInstance<ActionGameLose>());
            Add(CreateInstance<ActionGameWin>());
            Add(CreateInstance<ActionMoveArea>());
            Add(CreateInstance<ActionMoveLevel>());
            Add(CreateInstance<ActionMoveTo>());
            Add(CreateInstance<ActionProduce>());
            Add(CreateInstance<ActionSleep>());
            Add(CreateInstance<ActionTerraform>());
            Add(CreateInstance<ActionTownEvolve>());
            Add(CreateInstance<ActionTrade>());
            Add(CreateInstance<ActionTrain>());
            Add(CreateInstance<ActionUpgrade>());
            Add(CreateInstance<ActionClaim>());
            Add(CreateInstance<ActionImprovement>());
            Add(CreateInstance<ActionStat>());
            Add(CreateInstance<ActionModi>());
            Add(CreateInstance<ActionChestOpen>());
            
            mapGenerators = new Dictionary<string, BaseMapGenerator>();
            mapGenerators.Add("underground",new UndergroundMapGenerator());
            mapGenerators.Add("mountain",new BaseMapGenerator());
            
            _nameGenerators = new Dictionary<string, BaseNameGenerator>();
            Add(new UnderWaterTownNameGenerator());
            Add(new GhostTownNameGenerator());
            Add(new DwarfTownNameGenerator());
            Add(new ElfTownNameGenerator());
            Add(new SkyTownNameGenerator());
            Add(new OrcTownNameGenerator());
            Add(new SteamTownNameGenerator());
            Add(new FantasyTownNameGenerator());
            Add(new VikingTownNameGenerator());
            Add(new TownNameGenerator());
            Add(new UnitNameGenerator());
            
            _gameButtonRuns = new Dictionary<string, BaseGameButtonRun>();
            Add(CreateInstance<MainMenuGameButtonRun>());
            Add(CreateInstance<ResearchGameButtonRun>());
            Add(CreateInstance<LexiconGameButtonRun>());
            Add(CreateInstance<QuestGameButtonRun>());
            Add(CreateInstance<DebugGameButtonRun>());
            Add(CreateInstance<NextRoundGameButtonRun>());
            Add(CreateInstance<OptionsGameButtonRun>());
            Add(CreateInstance<SaveGameButtonRun>());
            Add(CreateInstance<LoadGameButtonRun>());
            Add(CreateInstance<ModButtonRun>());
            Add(CreateInstance<TitleGameButtonRun>());
            Add(CreateInstance<ExitGameButtonRun>());
            Add(CreateInstance<EndlessGameButtonRun>());
            Add(CreateInstance<KingdomGameButtonRun>());
            Add(CreateInstance<NextUnitGameButtonRun>());
            Add(CreateInstance<CampaignGameButtonRun>());
            Add(CreateInstance<UpdateGameButtonRun>());
            Add(CreateInstance<BackMenuGameButtonRun>());
            Add(CreateInstance<MoreMenuGameButtonRun>());
            Add(CreateInstance<MoveLevelGameButtonRun>());
            Add(CreateInstance<OverlayGameButtonRun>());
            
            playerTypes = new Dictionary<string, BasePlayerType>();
            Add(new NaturePlayerType());
            Add(new HumanPlayerType());
        }

        public string NameGenerator(string id, string include = null)
        {
            if (!_nameGenerators.ContainsKey(id))
            {
                throw new MissingMemberException("nameGenerators " + id +" is missing.");
            }
            return _nameGenerators[id].Gen(include);
        }

        public BaseGameButtonRun Button(string id)
        {
            if (!_gameButtonRuns.ContainsKey(id))
            {
                throw new MissingMemberException("BaseGameButtonRun " + id +" is missing.");
            }
            return _gameButtonRuns[id];
        }
        
        private void Add(BaseElementRun element)
        {
            elementRuns[element.id] = element;
        }
        
        private void Add(BasePlayerType element)
        {
            playerTypes[element.id.ToString()] = element;
        }
        
        private void Add(Dictionary<string,IRun> holder, IRun run)
        {
            holder[run.ID()] = run;
        }
        
        private void Add(BaseNameGenerator bName)
        {
            _nameGenerators[bName.id] = bName;
        }
        
        private void Add(BaseOverlay overlay)
        {
            overlaysRuns[overlay.ID()] = overlay;
        }

        private void Add(BasePerformAction performAction)
        {
            _actions[performAction.id] = performAction;
        }

        private void Add(BaseGameButtonRun gameButton)
        {
            _gameButtonRuns[gameButton.id] = gameButton;
        }
        

        public BasePerformAction GetNewAction(string key)
        {
            if (!_actions.ContainsKey(key))
            {
                throw new MissingMemberException($"Action {key} is missing");
            }

            return _actions[key];
        }
        
    }
}