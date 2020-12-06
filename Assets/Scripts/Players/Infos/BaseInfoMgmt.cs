using System;
using System.Collections.Generic;
using Game;
using Libraries.Rounds;
using UnityEngine;

namespace Players.Infos
{
    [Serializable]
    public class BaseInfoMgmt
    {
        public List<Info> infos;
        
        public BaseInfoMgmt()
        {
            infos = new List<Info>();
        }
        
        public virtual void Add(Info noti)
        {
            //Debug.Log($"Add Note: {noti.title}");
            noti.round = S.Round().Round;
            infos.Insert(0,noti);
        }

        /// <summary>
        /// Clear old notis
        /// </summary>
        public void NextRound()
        {
            if (infos.Count > 50)
            {
                infos.RemoveRange(50, infos.Count-51);
            }
        }
    }
}