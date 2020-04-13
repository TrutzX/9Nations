using System;
using Buildings;
using Libraries;
using Players;
using Tools;
using UnityEngine;

namespace reqs
{
    public class ReqRes : BaseReqMinMax
    {
        protected override int ValueMax(Player player, MapElementInfo onMap, string element, string sett, NVector pos)
        {
            return ValueMax(player, element, sett);
        }

        protected override int ValueMax(Player player, string element, string sett)
        {
            //TODO
            return 100;
        }

        protected override int ValueAct(Player player, MapElementInfo onMap, string element, string sett, NVector pos)
        {
            return onMap.Town().GetRes(element);
        }

        protected override int ValueAct(Player player, string element, string sett)
        {
            return player.GetResTotal(element);
        }

        protected override string Name(string element, string sett)
        {
            return L.b.res[element].name;
        }
    }
}