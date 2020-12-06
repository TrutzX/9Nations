using System;
using System.Collections.Generic;
using Buildings;
using MapElements.Units;
using UI;
using Units;
using UnityEngine;

namespace MapElements.Buildings
{
    public class BuildingAnimator : MonoBehaviour
    {
        private int _count, _act;
        private Sprite[] _anList;
        public void Update()
        {
            if (_anList == null) return;

            _count++;
            
            if (_count < 6) return;

            _count = 0;
            _act = (++_act) % _anList.Length;
            GetComponent<SpriteRenderer>().sprite = _anList[_act];
        }

        public void CreateAnimation()
        {
            var dataBuilding = GetComponent<BuildingInfo>().dataBuilding;
            _anList = null;

            string opath = dataBuilding.Icon;
            //is winter?
            if (GetComponent<BuildingInfo>().isWinter && !String.IsNullOrEmpty(dataBuilding.winter))
            {
                opath = dataBuilding.winter;
            }
            
            //has animation?
            if (!opath.Contains("Animation"))
                return;
            
            List<Sprite> sprites = new List<Sprite>();

            int count = 0;
            string path = opath;
            while (SpriteHelper.Exist(path))
            {
                sprites.Add(SpriteHelper.Load(path));
                count++;
                path = opath.Replace("_0", "_"+count);
            }
            // Debug.Log(path+" found animation "+sprites.Count);

            if (sprites.Count == 0)
            {
                throw new MissingMemberException("Building animation "+opath+" has 0 sprites");
            }

            _anList = sprites.ToArray();
        }

        public void ResetRunning()
        {
            _anList = null;
            GetComponent<SpriteRenderer>().sprite = _anList[0];
        }
    }
}