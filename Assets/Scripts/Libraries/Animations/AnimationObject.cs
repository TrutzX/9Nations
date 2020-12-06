using System;
using System.Collections.Generic;
using Game;
using Tools;
using UI;
using UnityEngine;

namespace Libraries.Animations
{
    public class AnimationObject : MonoBehaviour
    {

        public int count, countMax;
        public int act;
        public List<Sprite> sprites;
        
        public void Update()
        {
            count++;
            
            if (count < countMax) return;

            count = 0;
            act++;
            if (act >= sprites.Count)
            {
                Destroy(gameObject);
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = sprites[act];
            }
            
        }

        public static AnimationObject Create(NVector pos)
        {
            var ani = Instantiate(UIElements.Get().animationHelper, GameMgmt.Get().newMap[pos.level].effects.transform).GetComponent<AnimationObject>();
            ani.transform.position = new Vector3(pos.x+0.5f,pos.y+0.5f);
            return ani;
        }
    }
}