using System;
using Libraries.Units;
using UnityEngine;
using UnityEngine.UI;

namespace Units
{
    public class UnitAnimator : MonoBehaviour
    {
        public bool running;
        private int count, act;
        private int[] anList;
        private DataUnit dataUnit;
        public void Update()
        {
            if (!running) return;

            count++;
            
            if (count < 6) return;

            count = 0;
            act = (++act) % anList.Length;
            ShowRen();
        }

        public void AnStart()
        {
            running = true;
        }

        public void AnStop()
        {
            running = false;
            act = 0;
            ShowRen();
        }

        public void Calc(int x, int y)
        {
            dataUnit = GetComponent<UnitInfo>().dataUnit;
            //rotate
            if (x > 0)
            {
                act = 7;
            }
            else if (x < 0)
            {
                act = 4;
            } 
            else if (y < 0)
            {
                act = 1;
            } 
            else 
            {
                act = 10;
            }
            anList = new []{act, act-1, act, act+1};
            act = 0;
            ShowRen();
            AnStart();
        }

        private void ShowRen()
        {
            GetComponent<SpriteRenderer>().sprite = dataUnit.Sprite(anList[act]);
        }
    }
}