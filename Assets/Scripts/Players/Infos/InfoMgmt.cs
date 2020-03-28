using System;
using System.Collections.Generic;
using Game;
using Libraries.Rounds;
using UnityEngine;

namespace Players.Infos
{
    [Serializable]
    public class InfoMgmt
    {
        public List<Info> infos;

        public InfoMgmt()
        {
            infos = new List<Info>();
        }
        
        public void Add(Info noti)
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
                infos.RemoveAt(50);
            }
        }
    }
}