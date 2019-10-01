using System.Collections.Generic;
using DataTypes;
using Tools;
using UnityEngine;

namespace Game
{
    public class RessHelper
    {
        public static string BuildRessString(Dictionary<string, int> ress)
        {
            List<string> txt = new List<string>();

            foreach (KeyValuePair<string, int> r in ress)
            {
                string[] i = Data.ress[r.Key].icon.Split(':');
                //TODO fix
                
                txt.Add($"<sprite=\"{i[0]}\" name=\"{i[1]}\"> {r.Value}x");
            }
            
            
            
            return TextHelper.CommaSepA(txt.ToArray());
        }
    }
}