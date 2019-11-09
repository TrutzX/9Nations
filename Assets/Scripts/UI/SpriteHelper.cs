using System;
using System.Linq;
using UnityEngine;

namespace UI
{
    public class SpriteHelper : ScriptableObject
    {
        public static Sprite Load(string path)
        {
            if (path.StartsWith("b:"))
            {
                return Data.building[path.Substring(2)].GetIcon();
            }
            
            if (path.StartsWith("u:"))
            {
                return Data.unit[path.Substring(2)].GetIcon();
            }

            if (Data.icons.ContainsKey(path))
            {
                path = Data.icons[path].file;
            }
            
            if (path.Contains(":"))
            {
                string[] prepath = path.Split(':');
                try
                {
                    try
                    {
                        return Resources.LoadAll<Sprite>(prepath[0]).Single(s => s.name == prepath[1]);
                    }
                    catch (InvalidOperationException e)
                    {
                        //TODO übergang
                        return Resources.LoadAll<Sprite>("Icons/" + prepath[0]).Single(s => s.name == prepath[1]);
                    }
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

        [ObsoleteAttribute("This method is obsolete. Call Load instead.", false)] 
        public static Sprite LoadIcon(string icon)
        {
            return Load(icon);
        }
        
        [ObsoleteAttribute("This method is obsolete. Call Load instead.", false)] 
        public static Sprite LoadImage(string image)
        {
            return LoadIcon(image);
        }
    }
    
    
}