using System;
using System.IO;
using System.Linq;
using Libraries;
using UnityEngine;

namespace UI
{
    public class SpriteHelper : ScriptableObject
    {
        public static Sprite Load(string path)
        {
            
            if (L.b != null && LSys.tem.icons.ContainsKey(path))
            {
                path = LSys.tem.icons[path].Icon;
            }

            if (path.StartsWith("!"))
            {
                path = path.Substring(1);
                
            }
            else
            {
                //TODO load extern
                Debug.LogWarning($"Path {path} is wrong formatted");
            }
            
            if (path.Contains(":"))
            {
                string[] prepath = path.Split(':');
                try
                {
                    try
                    {
                        return UnityEngine.Resources.LoadAll<Sprite>(prepath[0]).Single(s => s.name == prepath[1]);
                    }
                    catch (InvalidOperationException)
                    {
                        //TODO übergang
                        return UnityEngine.Resources.LoadAll<Sprite>("Icons/" + prepath[0]).Single(s => s.name == prepath[1]);
                    }
                }
                catch (InvalidOperationException e)
                {
                    Debug.LogWarning($"icon {path} is missing");
                    Debug.LogException(e);
                    return UnityEngine.Resources.Load<Sprite>(prepath[0]);
                }
            }
            
            if (!path.EndsWith("logo"))
            {
                Debug.LogWarning($"icon {path} looks broken");
            }

            return UnityEngine.Resources.Load<Sprite>(path);
        }

        [ObsoleteAttribute("This method is obsolete. Call Load instead.", false)] 
        public static Sprite LoadIcon(string icon)
        {
            return Load(icon);
        }
        
        public static Sprite LoadExternalSprite(string filePath, float pixelsPerUnit = 32f, SpriteMeshType spriteType = SpriteMeshType.Tight)
        {
            // Load a PNG or JPG image from disk to a Texture2D, assign this texture to a new sprite and return its reference
 
            Texture2D spriteTexture = LoadExternalTexture(filePath);
            return Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), new Vector2(0, 0), pixelsPerUnit, 0 , spriteType);
        }
        
        public static Texture2D LoadExternalTexture(string filePath)
        {
            // original author https://forum.unity.com/threads/generating-sprites-dynamically-from-png-or-jpeg-files-in-c.343735/
            // Load a PNG or JPG file from disk to a Texture2D
            // Returns null if load fails
 
            if (File.Exists(filePath))
            {
                Texture2D tex2D = new Texture2D(2, 2);
                if (tex2D.LoadImage(File.ReadAllBytes(filePath)))           // Load the imagedata into the texture (size is set automatically)
                    return (tex2D);                 // If data = readable -> return texture
            }
            return null;                     // Return null if load failed
        }
    }
    
    
}