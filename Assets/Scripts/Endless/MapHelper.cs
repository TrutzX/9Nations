using System.Collections.Generic;
using Help;
using UnityEngine;

namespace Endless
{
    public class MapHelper
    {
        public static List<Maps.Map> GetAllMaps()
        {
            List<Maps.Map> maps = new List<Maps.Map>();
            //DirectoryInfo[] info = new DirectoryInfo("Assets/Resources/Maps").GetDirectories();
            //foreach (DirectoryInfo f in info) 
            //{
                //maps.Add(new Map(f));
            //}
            
            string map = Resources.Load<TextAsset>("Maps/list").text;
            
            
            foreach (string m in CSV.StringToList(map))
            {
                maps.Add(new Maps.Map(m));
            }
            //Resources.LoadAll<Sprite>("Units/" + file);
            
            return maps;
        }

        
    }
}