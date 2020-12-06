using System;
using Audio;
using DG.Tweening;
using Game;
using Libraries.Campaigns;
using MapElements;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Libraries.Animations
{
    [Serializable]
    public class AnimationMgmt : BaseMgmt<NAnimation>
    {
        public AnimationMgmt() : base("animation") { }

        protected override void ParseElement(NAnimation ele, string header, string data)
        {
            switch (header)
            {
                case "speed":
                    ele.speed = Int(data);
                    break;
                case "repeat":
                    ele.repeat = Int(data);
                    break;
                case "sound":
                    ele.sound = data;
                    break;
                default:
                    base.ParseElement(ele, header, data);
                    break;
            }
        }

        public void Create(string id, NVector pos)
        {
            NAnimation nAni = L.b.animations[id];
            
            //create animation
            var ani = AnimationObject.Create(pos);
            ani.sprites = nAni.Sprites();

                if (ani.sprites == null)
                {
                    Debug.LogError(nAni.Icon+" is wrong formed");
                    return;
                }
            Debug.Log(nAni.Icon+" found animation "+ani.sprites.Count);

            //play sound?
            if (!string.IsNullOrEmpty(nAni.sound))
            {
                NAudio.Play(nAni.sound);
            }
            
            ani.countMax = nAni.speed;
        }

        public AnimationText Text(string txt, NVector pos, Color color, MapElementInfo mei = null)
        {
            var at = AnimationText.Create(txt, pos);
            Debug.Log("at"+at.transform.position);
            at.GetComponent<Text>().color = new Color(color.r,color.g,color.b,1);

            //has mapelement?
            if (mei != null)
            {
                Sequence seq = DOTween.Sequence();
                seq.Append(mei.GetComponent<SpriteRenderer>().DOColor(color, 1));
                seq.Append(mei.GetComponent<SpriteRenderer>().DOColor(Color.white, 1));
            }

            return at;
        }

        public AnimationText Hp(int hp, NVector pos, MapElementInfo mei = null)
        {
            return Text(S.T("hpAdd",TextHelper.Plus(hp)), pos, hp<0?Color.red:Color.green, mei);
        }
    }
}