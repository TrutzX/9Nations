using System;
using System.Linq;
using UnityEngine;

namespace UI
{
    public class SpriteHelper : ScriptableObject
    {
        public static Sprite Load(string path)
        {
            if (path.Contains(":"))
            {
                string[] prepath = path.Split(':');
                try
                {
                    return Resources.LoadAll<Sprite>(prepath[0]).Single(s => s.name == prepath[1]);
                }
                catch (InvalidOperationException e)
                {
                    Debug.LogWarning($"icon {path} is missing");
                    Debug.LogException(e);
                    return Resources.Load<Sprite>(prepath[0]);
                }
            }

            return Resources.Load<Sprite>(path);
        }

        public static Sprite LoadIcon(string icon)
        {
            return Load("Icons/" + icon);
        }
    }
    
    
}