using System;
using Libraries.Buildings;
using UI;
using UnityEngine;

namespace Libraries.Units
{
    [Serializable]
    public class DataUnit : BaseDataBuildingUnit
    {
        public string movement;
        public string type;
        
        public Sprite Sprite(int sprite = 1)
        {
            return SpriteHelper.Load(Icon.Replace("1", sprite.ToString()));
        }
    }
}