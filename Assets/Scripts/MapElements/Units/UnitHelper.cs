using System.Collections.Generic;

using reqs;
using UI;
using UnityEngine;

namespace Game
{
    public static class UnitHelper
    {
        public static Sprite GetIcon(string file, int id = 1)
        {
            Sprite[] s = Resources.LoadAll<Sprite>("Units/" + file);

            if (s==null || s.Length != 12)
            {
                Debug.LogWarning($"Sprite Units/{file} is wrong formatted.");
                return Resources.Load<Sprite>("Units/" + file);
            }
            return s[id];
        }
    }
    
    
}