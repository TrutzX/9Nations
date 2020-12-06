using DG.Tweening;
using Game;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Libraries.Animations
{
    public class AnimationText : MonoBehaviour
    {
        

        public static AnimationText Create(string txt, NVector pos)
        {
            var ani = Instantiate(UIElements.Get().animationText, OnMapUI.Get().textAnimation.transform).GetComponent<AnimationText>();
            var p = Camera.main.WorldToScreenPoint(new Vector3(pos.x + 1.5f, pos.y + 0.5f));
            Debug.Log(pos);
            Debug.Log(p);
            ani.GetComponent<RectTransform>().anchoredPosition = new Vector2(p.x, p.y);
            ani.GetComponent<Text>().text = txt;
            ani.name = txt;
            
            // Grab a free Sequence to use
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(ani.GetComponent<Text>().DOFade(1,1)); //fade in
            mySequence.Append(ani.GetComponent<RectTransform>().DOAnchorPosY(ani.GetComponent<RectTransform>().anchoredPosition.y+60,4)); //move
            mySequence.Insert(2,ani.GetComponent<Text>().DOFade(0,2)); //fade in
            mySequence.OnComplete(() => Destroy(ani.gameObject));
                //mySequence.Play();
                            
            return ani;
        }
    }
}