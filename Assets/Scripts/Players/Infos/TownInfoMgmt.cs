using System;
using System.Collections.Generic;
using Game;
using Libraries.Rounds;
using Towns;
using UnityEngine;

namespace Players.Infos
{
    [Serializable]
    public class TownInfoMgmt : BaseInfoMgmt
    {
        [NonSerialized] public Town town;

        public override void Add(Info noti)
        {
            base.Add(noti);
            //Debug.Log(player+" "+noti.title);
            town.Player().info.Add(noti);
        }
    }
}