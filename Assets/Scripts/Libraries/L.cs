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
using Libraries.Elements;
using Libraries.FActions;
using Libraries.FActions.General;
using Libraries.GameButtons;
using Libraries.MapGenerations;
using Libraries.Maps;
using Libraries.Movements;
using Libraries.Nations;
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
    public class L
    {
        public static L b;
        
        [NonSerialized] public LoadingScreen Load;
        
        public ModifierMgmt modifiers;
        public TerrainMgmt terrain;
        public MapMgmt maps;
        public NationMgmt nations;
        public ImprovementMgmt improvements;
        public ElementMgmt elements;
        public RoundMgmt rounds;
        public MapGenerationMgmt mapGeneration;
        public DataBuildingMgmt buildings;
        public DataUnitMgmt units;
        public UsageMgmt usages;
        public ResourceMgmt res;
        public FActionMgmt actions;
        public ResearchMgmt researches;
        public MovementMgmt movements;
        public CampaignMgmt campaigns;
        public ScenarioMgmt scenarios;
        public GameButtonMgmt gameButtons;
        

        public Dictionary<string, IRead> mgmts;
        public static IEnumerator Init(LoadingScreen load)
        {
            b = new L();
            b.Load = load;
            yield return b.Load.ShowMessage("Loading data");
            b.Init();
            
            yield return b.ReadData();

            //load debug mods?
            if (!string.IsNullOrWhiteSpace(PlayerPrefs.GetString("mod.folder", "")))
            {
                yield return b.LoadMods(new List<string>(Directory.GetDirectories(PlayerPrefs.GetString("mod.folder"))));
            }
            
            //Debug.Log(string.Join(",", ModManager.GetInstalledModDirectories(true)));
            yield return b.LoadMods(ModManager.GetInstalledModDirectories(true));
        }

        private void Init()
        {
            mgmts = new Dictionary<string, IRead>();
            terrain = (TerrainMgmt) Add(new TerrainMgmt());
            maps = (MapMgmt) Add(new MapMgmt());
            modifiers = (ModifierMgmt) Add(new ModifierMgmt());
            nations = (NationMgmt) Add(new NationMgmt());
            improvements = (ImprovementMgmt) Add(new ImprovementMgmt());
            elements = (ElementMgmt) Add(new ElementMgmt());
            rounds = (RoundMgmt) Add(new RoundMgmt());
            mapGeneration = (MapGenerationMgmt) Add(new MapGenerationMgmt());
            buildings = (DataBuildingMgmt) Add(new DataBuildingMgmt());
            units = (DataUnitMgmt) Add(new DataUnitMgmt());
            usages = (UsageMgmt) Add(new UsageMgmt());
            res = (ResourceMgmt) Add(new ResourceMgmt());
            actions = (FActionMgmt) Add(new FActionMgmt());
            researches = (ResearchMgmt) Add(new ResearchMgmt());
            movements = (MovementMgmt) Add(new MovementMgmt());
            campaigns = (CampaignMgmt) Add(new CampaignMgmt());
            scenarios = (ScenarioMgmt) Add(new ScenarioMgmt());
            gameButtons = (GameButtonMgmt) Add(new GameButtonMgmt());
        }
        
        private IRead Add(IRead mgmt)
        {
            mgmts.Add(mgmt.Id(),mgmt);
            return mgmt;
        }
        
        public IEnumerator LoadMods(List<string> folder)
        {
            yield return Load.ShowMessage("Loading mods");
            foreach (string f in folder)
            {
                //Debug.Log(f);

                DirectoryInfo dir = new DirectoryInfo(f);
                yield return Load.ShowSubMessage("Loading " + dir.Name);

                //has a file?
                FileInfo info = new FileInfo(Path.Combine(f, "files.txt"));
                if (!info.Exists)
                {
                    Debug.LogWarning("Mod file "+info.FullName+" is missing.");
                    continue;
                }
                
                // Read the file and display it line by line.
                string line;
                StreamReader file = new StreamReader(info.OpenRead());  
                while((line = file.ReadLine()) != null)  
                {  
                    string[] d = line.Split('=');
                    if (d[1].EndsWith(".ini"))
                    {
                        yield return GetMgmt(d[0]).ParseIni(Path.Combine(f, d[1]));
                    } else
                        yield return GetMgmt(d[0]).ParseCsv(Path.Combine(f, d[1]));
                }
            }
        }
        
        public IEnumerator ReadData()
        {
            string fs = Resources.Load<TextAsset>("Data/files").text;
            string[] fLines = Regex.Split ( fs, "\n|\r|\r\n" );
            foreach (string line in fLines)
            {
                string[] d = line.Split('=');
                yield return GetMgmt(d[0]).ParseCsv("!Data/"+d[1]);
            }
            
        }

        private IRead GetMgmt(string typ)
        {
            if (mgmts.ContainsKey(typ))
            {
                return mgmts[typ];
            }
            throw new MissingMemberException($"Mgmt {typ} is missing");
        }
    }
}
