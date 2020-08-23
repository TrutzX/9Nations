using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Libraries.Campaigns;
using Libraries.MapGenerations;
using Libraries.Maps;
using Loading;
using ModIO;
using UnityEngine;

namespace Libraries
{
    [Serializable]
    public abstract class LBase
    {
        [SerializeField] protected string file;
        
        public Dictionary<string, IRead> mgmts;

        /// <summary>
        /// For save only
        /// </summary>
        public LBase() : this(null)
        {
        }
        
        public LBase(string file)
        {
            this.file = file;
            mgmts = new Dictionary<string, IRead>();
        }
        
        public IEnumerator LoadMods(List<string> folder)
        {
            yield return LSys.tem.Load.ShowMessage("Loading mods");
            foreach (string f in folder)
            {
                //Debug.Log(f);

                DirectoryInfo dir = new DirectoryInfo(f);
                yield return LSys.tem.Load.ShowSubMessage("Loading " + dir.Name);

                //has a file?
                FileInfo info = new FileInfo(Path.Combine(f, file+".txt"));
                if (!info.Exists)
                {
                    Debug.LogWarning("Mod file "+info.FullName+" is missing.");
                    continue;
                }
                
                // Read the file and display it line by line.
                string line;
                StreamReader reader = new StreamReader(info.OpenRead());  
                while((line = reader.ReadLine()) != null)  
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
        
        protected virtual IEnumerator Loading()
        {
            yield return LSys.tem.Load.ShowMessage("Loading "+file);
            
            yield return ReadDataIntern();

            //load debug mods?
            if (!string.IsNullOrWhiteSpace(LSys.tem.options["modFolder"].Value()))
            {
                yield return LoadMods(new List<string>(Directory.GetDirectories(LSys.tem.options["modFolder"].Value())));
            }
            
            //Debug.Log(string.Join(",", ModManager.GetInstalledModDirectories(true)));
            yield return LoadMods(ModManager.GetInstalledModDirectories(true));
        }

        public IEnumerator ReadDataIntern()
        { 
            string fs = Resources.Load<TextAsset>("Data/"+file).text;
            string[] fLines = Regex.Split ( fs, "\n|\r|\r\n" );
            foreach (string line in fLines)
            {
                string[] d = line.Split('=');
                yield return GetMgmt(d[0]).ParseCsv("!Data/"+d[1]);
            }
            
        }
        
        protected IRead Add(IRead mgmt)
        {
            mgmts.Add(mgmt.Id(),mgmt);
            return mgmt;
        }

        protected IRead GetMgmt(string typ)
        {
            if (mgmts.ContainsKey(typ))
            {
                return mgmts[typ];
            }
            throw new MissingMemberException($"Mgmt {typ} is missing");
        }
    }
}