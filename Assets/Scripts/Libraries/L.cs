using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Help;
using Improvements;
using Loading;
using Maps;
using Modifiers;
using ModIO;
using Nations;
using Newtonsoft.Json.Utilities;
using Terrains;
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

        public static IEnumerator Init(LoadingScreen load)
        {
            b = new L();
            b.Load = load;
            yield return b.Load.ShowMessage("Loading data");
            b.terrain = new TerrainMgmt();
            b.maps = new MapMgmt();
            b.modifiers = new ModifierMgmt();
            b.nations = new NationMgmt();
            b.improvements = new ImprovementMgmt();

            
            yield return b.ReadData();

            //load debug mods?
            if (!string.IsNullOrWhiteSpace(PlayerPrefs.GetString("mod.folder", "")))
            {
                yield return b.LoadMods(new List<string>(Directory.GetDirectories(PlayerPrefs.GetString("mod.folder"))));
            }
            
            //Debug.Log(string.Join(",", ModManager.GetInstalledModDirectories(true)));
            yield return b.LoadMods(ModManager.GetInstalledModDirectories(true));
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
            switch (typ)
            {
                case "terrain":
                    return terrain;
                case "map":
                    return maps;
                case "modifier":
                    return modifiers;
                case "nation":
                    return nations;
                case "improvement":
                    return improvements;
                default:
                    throw new MissingMemberException($"Mgmt {typ} is missing");
            }
        }
    }
}
