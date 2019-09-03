using System.Collections.Generic;
using DataTypes;
using reqs;
using UI;
using UnityEngine;

namespace Game
{
    public class UnitHelper
    {
        

        public static string[] GetIDs()
        {
            List<string> ids = new List<string>();
            foreach (Unit b in Data.unit)
            {
                ids.Add(b.id);
            }

            return ids.ToArray();
        }

        
    }
    
    
}