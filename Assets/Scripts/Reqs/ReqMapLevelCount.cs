using System;
using Buildings;
using Game;
using Players;
using Tools;
using UnityEngine;

namespace reqs
{
    public class ReqMapLevelCount : BaseReqMinMaxPlayer
    {

        protected override int ValueMax(Player player, string element, string sett)
        {
            return S.Map().levels.Count;
        }

        protected override int ValueAct(Player player, string element, string sett)
        {
            return S.Map().levels.Count;
        }

        protected override string Name(string element, string sett)
        {
            return "map level";
        }
    }
}