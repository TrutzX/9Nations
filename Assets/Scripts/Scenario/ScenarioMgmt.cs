using System;
using System.Collections.Generic;
using System.IO;
using Help;
using UnityEngine;

namespace Scenario
{
    public class ScenarioMgmt
    {
        public static List<Map> GetAllMaps()
        {
            List<Map> maps = new List<Map>();
            //DirectoryInfo[] info = new DirectoryInfo("Assets/Resources/Maps").GetDirectories();
            //foreach (DirectoryInfo f in info) 
            //{
                //maps.Add(new Map(f));
            //}
            
            string map = Resources.Load<TextAsset>("Maps/list").text;
            
            
            foreach (string m in CSV.StringToList(map))
            {
                maps.Add(new Map(m));
            }
            //Resources.LoadAll<Sprite>("Units/" + file);
            
            return maps;
        }

        
    }
}