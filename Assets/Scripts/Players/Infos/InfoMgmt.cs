using System;
using System.Collections.Generic;
using Players.Infos;

namespace Players
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
            noti.round = RoundMgmt.Get().Round;
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