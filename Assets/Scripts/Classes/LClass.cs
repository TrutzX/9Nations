using System;
using System.Collections.Generic;
using Classes.Actions;
using Classes.Elements;
using Classes.GameButtons;
using Classes.MapGenerator;
using Classes.NameGenerator;
using Classes.Scenarios;
using Endless;
using Libraries.Campaigns;
using Libraries.Elements;
using Libraries.FActions;
using Libraries.MapGenerations;
using UnityEngine;

namespace Classes
{
    public class LClass : ScriptableObject
    {
        public Dictionary<string,IScenarioRun> scenarioRuns;
        public Dictionary<string,BaseElementRun> elementRuns;
        public Dictionary<string,BaseGameButtonRun> gameButtonRuns;
        public Dictionary<string,BaseMapGenerator> mapGenerators;
        public Dictionary<string,BaseNameGenerator> nameGenerators;
        private Dictionary<string,BasePerformAction> _actions;

        public static LClass s;
        public static void Init()
        {
            s = CreateInstance<LClass>();
            s.Init2();
        }
        
        private void Init2()
        {
            scenarioRuns = new Dictionary<string, IScenarioRun>();
            scenarioRuns.Add("debug",new DebugScenario());
            scenarioRuns.Add("endless",new EndlessGameScenario());
            scenarioRuns.Add("tutorialbasic",new TutorialBasicScenario());
        
            elementRuns = new Dictionary<string, BaseElementRun>();
            Add(CreateInstance<LightElement>());
            Add(CreateInstance<ShadowElement>());
            
            _actions = new Dictionary<string, BasePerformAction>();
            Add(CreateInstance<ActionAttackUnit>());
            Add(CreateInstance<ActionAttackBuilding>());
            Add(CreateInstance<ActionBuild>());
            Add(CreateInstance<ActionCameraMove>());
            Add(CreateInstance<ActionDestroy>());
            Add(CreateInstance<ActionFeaturePlayer>());
            Add(CreateInstance<ActionFoundTown>());
            Add(CreateInstance<ActionInteract>());
            Add(CreateInstance<ActionGameButton>());
            Add(CreateInstance<ActionGameLose>());
            Add(CreateInstance<ActionGameWin>());
            Add(CreateInstance<ActionMove>());
            Add(CreateInstance<ActionMoveLevel>());
            Add(CreateInstance<ActionProduce>());
            Add(CreateInstance<ActionSleep>());
            Add(CreateInstance<ActionTerraform>());
            Add(CreateInstance<ActionTownLevel>());
            Add(CreateInstance<ActionTrain>());
            Add(CreateInstance<ActionUpgrade>());
            
            mapGenerators = new Dictionary<string, BaseMapGenerator>();
            mapGenerators.Add("underground",new UndergroundMapGenerator());
            mapGenerators.Add("mountain",new BaseMapGenerator());
            
            nameGenerators = new Dictionary<string, BaseNameGenerator>();
            Add("underwater", new UnderWaterTownNameGenerator());
            Add("ghost", new GhostTownNameGenerator());
            Add("dwarf", new DwarfTownNameGenerator());
            Add("elf", new ElfTownNameGenerator());
            Add("sky", new SkyTownNameGenerator());
            Add("orc", new OrcTownNameGenerator());
            Add("steam", new SteamTownNameGenerator());
            Add("fantasy", new FantasyTownNameGenerator());
            Add("viking", new VikingTownNameGenerator());
            Add("town", new TownNameGenerator());
            Add("unit", new UnitNameGenerator());
            
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

        }

        public BaseNameGenerator GetNameGenerator(string id)
        {
            if (!nameGenerators.ContainsKey(id))
            {
                throw new MissingMemberException("nameGenerators " + id +" is missing.");
            }
            return nameGenerators[id];
        }
        
        private void Add(BaseElementRun element)
        {
            elementRuns[element.id] = element;
        }
        
        private void Add(string id, BaseNameGenerator name)
        {
            nameGenerators[id] = name;
            name.id = id;
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