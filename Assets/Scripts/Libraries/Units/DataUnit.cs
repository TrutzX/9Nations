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

        public DataUnit()
        {
            oActions.Add("move","");
        }
        
        public Sprite Sprite(int sprite = 1)
        {
            return SpriteHelper.Load(Icon.Replace("1", sprite.ToString()));
        }
    }
}