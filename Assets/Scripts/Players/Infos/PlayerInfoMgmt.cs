using System;
using System.Collections.Generic;
using Game;
using Libraries.Rounds;
using UnityEngine;

namespace Players.Infos
{
    [Serializable]
    public class PlayerInfoMgmt : BaseInfoMgmt
    {
        [NonSerialized] public Player player;

        public override void Add(Info noti)
        {
            base.Add(noti);
            //Debug.Log(player+" "+noti.title);
            if (player != null && player == S.ActPlayer())
            {
                OnMapUI.Get().InfoUi.AddInfoButton(noti);
            }
        }
    }
}