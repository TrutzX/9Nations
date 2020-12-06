using System;
using System.Collections.Generic;
using System.Linq;
using Game;
using Libraries.Rounds;
using MapElements;
using UnityEngine;

namespace Players.Infos
{
    [Serializable]
    public class MapElementInfoInfoMgmt : BaseInfoMgmt
    {
        [NonSerialized] public MapElementInfo mapElementInfo;

        public override void Add(Info noti)
        {
            base.Add(noti);

            if (mapElementInfo.Town() != null)
            {
                mapElementInfo.Town()?.info.Add(noti);
            }
            else
            {
                mapElementInfo.Player().info.Add(noti);
            }
        }

        public string LastInfo()
        {
            if (infos.Count == 0)
                return null;
            
            if (infos[0].round == S.Round().Round)
                return infos[0].title;

            return null;
        }
    }
}