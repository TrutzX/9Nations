using System;
using Game;
using Libraries.Buildings;
using Libraries.FActions;
using MapElements;
using MapElements.Units;
using Tools;
using UI;
using UnityEngine;

namespace Libraries.Units
{
    [Serializable]
    public class DataUnit : BaseDataBuildingUnit
    {
        public string movement;
        public string type;
        public string folder;
        
        public Sprite Sprite(int sprite = 1)
        {
            return SpriteHelper.Load(Icon.Replace("1", sprite.ToString()));
        }

        public bool ExistAnimationSprite(UnitAnimatorType typ)
        {
            if (typ == UnitAnimatorType.Face)
                return SpriteHelper.Exist(folder + "face");
            
            if (typ.ToString().StartsWith("Attack") || typ.ToString().StartsWith("Defend"))
                return SpriteHelper.Exist(folder + "fight");
            
            if (typ == UnitAnimatorType.Yes || typ == UnitAnimatorType.No || typ == UnitAnimatorType.Laugh ||
                typ == UnitAnimatorType.Cast)
                return SpriteHelper.Exist(folder + "emo");

            return false;
        }
        
        public Sprite AnimationSprite(UnitAnimatorType typ, int offset=0)
        {
            //has a face?
            if (typ == UnitAnimatorType.Face)
            {
                return ExistAnimationSprite(typ) ? SpriteHelper.Load(folder + "face:face") : Sprite();
            }

            //show attack
            if (typ.ToString().StartsWith("Attack") || typ.ToString().StartsWith("Defend"))
            {
                return SpriteHelper.Load(folder + "fight:fight_"+((int)typ+offset));
            }
            
            //has default animations?
            if (typ == UnitAnimatorType.Yes || typ == UnitAnimatorType.No || typ == UnitAnimatorType.Laugh ||
                typ == UnitAnimatorType.Cast)
            {
                return SpriteHelper.Load(folder + "emo:emo_"+((int)typ+offset));
            }
            
            return null;
        }
        
        public override void ShowLexicon(PanelBuilder panel, MapElementInfo onMap, NVector pos)
        {
            base.ShowLexicon(panel, onMap!=null && onMap.IsBuilding()?null:onMap, pos);
        }
    }
}