using System;
using System.Collections.Generic;
using System.Dynamic;
using UI;
using UnityEngine;

namespace Libraries.Animations
{
    [Serializable]
    public class NAnimation : BaseData
    {
        public int speed;
        public int repeat;
        public string sound;
        [NonSerialized] private List<Sprite> _sprites;

        public NAnimation()
        {
            speed = 1;
            repeat = 1;
        }

        public List<Sprite> Sprites()
        {
            if (_sprites == null)
            {
                _sprites = new List<Sprite>();
                int count = 0;
                string path = Icon;
                while (SpriteHelper.Exist(path))
                {
                    _sprites.Add(SpriteHelper.Load(path));
                    count++;
                    path = Icon.Replace("_0", "_"+count);
                    if (path == Icon)
                    {
                        Debug.LogError(path+" is wrong formed");
                        return null;
                    }
                }

                //add the sprites on the end
                if (repeat >= 2)
                {
                    var cp = new Sprite[_sprites.Count];
                    _sprites.CopyTo(cp);
                    for (int i = 1; i < repeat; i++)
                    {
                        _sprites.AddRange(cp);
                    }
                }
            }
            
            return _sprites;
        }
    }
}