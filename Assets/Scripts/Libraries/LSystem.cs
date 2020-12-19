using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Classes;

using Help;
using Improvements;
using Libraries.Buildings;
using Libraries.Campaigns;
using Libraries.Coats;
using Libraries.Elements;
using Libraries.FActions;
using Libraries.FActions.General;
using Libraries.FightModis;
using Libraries.GameButtons;
using Libraries.Helps;
using Libraries.Icons;
using Libraries.Inputs;
using Libraries.MapGenerations;
using Libraries.Maps;
using Libraries.Movements;
using Libraries.Nations;
using Libraries.Options;
using Libraries.Res;
using Libraries.Researches;
using Libraries.Rounds;
using Libraries.Terrains;
using Libraries.Translations;
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
    public class LSys : LBase
    {
        public static LSys tem;
        
        public MapMgmt maps;
        public MapGenerationMgmt mapGeneration;
        public CampaignMgmt campaigns;
        public ScenarioMgmt scenarios;
        public InputMgmt inputs;
        public OptionMgmt<Option> options;
        public BaseMgmt<Icon> icons;
        public HelpMgmt helps;
        public LanguageMgmt languages;
        public TranslationMgmt translations;
        
        [NonSerialized] public LoadingScreen Load;
        public LSys(string file) : base(file)
        {
        }
        
        public static IEnumerator Init(LoadingScreen load)
        {
            tem = new LSys("system");
            tem.Load = load;
            yield return tem.Loading();
        }

        protected override IEnumerator Loading()
        {
            yield return tem.Load.ShowMessage("Loading "+file);
            languages = (LanguageMgmt) Add(new LanguageMgmt());
            translations = (TranslationMgmt) Add(new TranslationMgmt());
            yield return ReadLang();
            maps = (MapMgmt) Add(new MapMgmt());
            mapGeneration = (MapGenerationMgmt) Add(new MapGenerationMgmt());
            campaigns = (CampaignMgmt) Add(new CampaignMgmt());
            scenarios = (ScenarioMgmt) Add(new ScenarioMgmt());
            inputs = (InputMgmt) Add(new InputMgmt());
            options = (OptionMgmt<Option>) Add(new OptionMgmt<Option>());
            icons = (BaseMgmt<Icon>) Add(new BaseMgmt<Icon>("icon"));
            helps = (HelpMgmt) Add(new HelpMgmt());
            
            yield return base.Loading();
        }

        protected IEnumerator ReadLang()
        {
            //todo dynamic
            yield return translations.ParseIni("!Translations/english");
            yield return translations.ParseIni("!Translations/russian");
        }
    }
}
