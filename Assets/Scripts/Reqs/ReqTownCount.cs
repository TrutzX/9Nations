using System;
using Buildings;
using Game;
using Players;
using Tools;
using UnityEngine;

namespace reqs
{
    public class ReqTownCount : ReqMinMaxPlayer
    {

        protected override int ValueMax(Player player, string element, string sett)
        {
            //TODO
            return 10;
        }

        protected override int ValueAct(Player player, string element, string sett)
        {
            return S.Towns().GetByPlayer(player.id).Count;
        }

        protected override string Name(string element, string sett)
        {
            return "town";
        }
    }
}