using System;
using System.Collections.Generic;
using Classes.Actions;
using Classes.Elements;
using Classes.GameButtons;
using Classes.MapGenerator;
using Classes.NameGenerator;
using Classes.Options;
using Classes.Scenarios;
using Endless;
using Game;
using Libraries.Campaigns;
using Libraries.Elements;
using Libraries.FActions;
using Libraries.MapGenerations;
using Players;
using Players.Kingdoms;
using UnityEngine;

namespace Classes
{
    public class LClass : ScriptableObject
    {
        public Dictionary<string,IRun> scenarioRuns;
        public Dictionary<string,IRun> optionRuns;
        public Dictionary<string,BaseElementRun> elementRuns;
        public Dictionary<string,BaseGameButtonRun> gameButtonRuns;
        public Dictionary<string,BaseMapGenerator> mapGenerators;
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
            Add(scenarioRuns, new TutorialBasicScenario());
            Add(scenarioRuns, new PantheonScenario());
        
            optionRuns = new Dictionary<string, IRun>();
            Add(optionRuns, CreateInstance<OptionAudioMusic>());
            Add(optionRuns, CreateInstance<OptionAudioSound>());
            Add(optionRuns, CreateInstance<OptionFullscreen>());
            Add(optionRuns, CreateInstance<OptionShowLog>());
            Add(optionRuns, CreateInstance<OptionUiScale>());
            Add(optionRuns, CreateInstance<OptionNight>());
            
            elementRuns = new Dictionary<string, BaseElementRun>();
            Add(CreateInstance<LightElement>());
            Add(CreateInstance<ShadowElement>());
            
            _actions = new Dictionary<string, BasePerformAction>();
            Add(CreateInstance<ActionAttackUnit>());
            Add(CreateInstance<ActionAttackBuilding>());
            Add(CreateInstance<ActionBuild>());
            Add(CreateInstance<ActionCameraMove>());
            Add(CreateInstance<ActionCraft>());
            Add(CreateInstance<ActionDestroy>());
            Add(CreateInstance<ActionEvolve>());
            Add(CreateInstance<ActionExplore>());
            Add(CreateInstance<ActionFeaturePlayer>());
            Add(CreateInstance<ActionFoundTown>());
            Add(CreateInstance<ActionHeal>());
            Add(CreateInstance<ActionInteract>());
            Add(CreateInstance<ActionGameButton>());
            Add(CreateInstance<ActionGameLose>());
            Add(CreateInstance<ActionGameWin>());
            Add(CreateInstance<ActionMove>());
            Add(CreateInstance<ActionMoveLevel>());
            Add(CreateInstance<ActionMoveTo>());
            Add(CreateInstance<ActionProduce>());
            Add(CreateInstance<ActionSleep>());
            Add(CreateInstance<ActionTerraform>());
            Add(CreateInstance<ActionTownEvolve>());
            Add(CreateInstance<ActionTrade>());
            Add(CreateInstance<ActionTrain>());
            Add(CreateInstance<ActionUpgrade>());
            
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
            
            gameButtonRuns = new Dictionary<string, BaseGameButtonRun>();
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
            
        }

        public string NameGenerator(string id, string include = null)
        {
            if (!_nameGenerators.ContainsKey(id))
            {
                throw new MissingMemberException("nameGenerators " + id +" is missing.");
            }
            return _nameGenerators[id].Gen(include);
        }
        
        private void Add(BaseElementRun element)
        {
            elementRuns[element.id] = element;
        }
        
        private void Add(Dictionary<string,IRun> holder, IRun run)
        {
            holder[run.ID()] = run;
        }
        
        private void Add(BaseNameGenerator name)
        {
            _nameGenerators[name.id] = name;
        }

        private void Add(BasePerformAction performAction)
        {
            _actions[performAction.id] = performAction;
        }

        private void Add(BaseGameButtonRun gameButton)
        {
            gameButtonRuns[gameButton.id] = gameButton;
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