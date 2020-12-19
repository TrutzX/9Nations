using Game;
using Libraries.Units;
using Units;
using UnityEngine;

namespace MapElements.Units
{
    public class UnitAnimator : MonoBehaviour
    {
        public bool running;
        private int count, act, maxRepeat;
        private Sprite[] anList;
        public void Update()
        {
            if (!running) return;

            count++;
            
            if (count < 6) return;

            count = 0;
            act = (++act) % anList.Length;
            ShowRen();
            if (maxRepeat != 0)
            {
                maxRepeat--;
                if (maxRepeat == 0) ResetIdle();
            }
        }

        public void MoveAnimationCalc(int x, int y)
        {
            var dataUnit = GetComponent<UnitInfo>().dataUnit;
            UnitAnimatorType type = UnitAnimatorType.MoveSouth;
            //rotate
            if (x > 0)
            {
                type = UnitAnimatorType.MoveEast;
            }
            else if (x < 0)
            {
                type = UnitAnimatorType.MoveWest;
            } 
            else if (y < 0)
            {
                type = UnitAnimatorType.MoveNorth;
            }

            PlayRunAnimation(type);
        }

        public void PlayRunAnimation(UnitAnimatorType type)
        {
            act = (int) type;
            var dataUnit = GetComponent<UnitInfo>().dataUnit;
            anList = new []{dataUnit.Sprite(act-1), dataUnit.Sprite(act), dataUnit.Sprite(act-1), dataUnit.Sprite(act+1)};
            act = 0;
            ShowRen();
            running = true;
        }
        
        public void PlayIdleAnimation(UnitAnimatorType type, int repeat=3)
        {
            var dataUnit = GetComponent<UnitInfo>().dataUnit;
            
            //has animation?
            if (!dataUnit.ExistAnimationSprite(type))
            {
                return;
            }

            //build it
            anList = new []{dataUnit.AnimationSprite(type), dataUnit.AnimationSprite(type,-1), dataUnit.AnimationSprite(type), dataUnit.AnimationSprite(type,1)};
            act = 0;
            maxRepeat = 4 * repeat;
            ShowRen();
            running = true;
            
        }

        public void PlayFightAnimation(UnitAnimatorType type)
        {
            var dataUnit = GetComponent<UnitInfo>().dataUnit;
            
            //has animation?
            if (!dataUnit.ExistAnimationSprite(type))
            {
                if (type == UnitAnimatorType.AttackEast || type == UnitAnimatorType.DefendEast)
                {
                    type = UnitAnimatorType.MoveEast;
                } else if (type == UnitAnimatorType.AttackWest || type == UnitAnimatorType.DefendWest)
                {
                    type = UnitAnimatorType.MoveWest;
                } else if (type == UnitAnimatorType.AttackSouth || type == UnitAnimatorType.DefendSouth)
                {
                    type = UnitAnimatorType.MoveSouth;
                } else {
                    type = UnitAnimatorType.MoveNorth;
                }
                PlayRunAnimation(type);
                maxRepeat = 4;
                return;
            }
            
            //build it
            anList = new []{dataUnit.AnimationSprite(type), dataUnit.AnimationSprite(type,-1), dataUnit.AnimationSprite(type), dataUnit.AnimationSprite(type,1)};
            act = 0;
            maxRepeat = 4;
            ShowRen();
            running = true;
            
            GetComponent<SpriteRenderer>().flipX = type == UnitAnimatorType.DefendEast || type == UnitAnimatorType.AttackWest;
        }

        private void ShowRen()
        {
            GetComponent<SpriteRenderer>().sprite = anList[act];
        }

        public void ResetRunning()
        {
            running = false;
            GetComponent<SpriteRenderer>().sprite = anList[1];
        }

        public void ResetIdle()
        {
            running = false;
            GetComponent<SpriteRenderer>().sprite = GetComponent<UnitInfo>().dataUnit.Sprite();
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}