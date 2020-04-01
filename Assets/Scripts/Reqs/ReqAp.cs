using System;
using Buildings;
using Game;
using Players;
using Tools;
using Towns;
using Units;
using UnityEngine;
using MapElementInfo = Buildings.MapElementInfo;

namespace reqs
{
    
    public class ReqAp : ReqMinMax
    {
        protected override int ValueMax(Player player, MapElementInfo onMap, string element, string sett, NVector pos)
        {
            return onMap.data.apMax;
        }

        protected override int ValueMax(Player player, string element, string sett)
        {
            throw new NotImplementedException();
        }

        protected override int ValueAct(Player player, MapElementInfo onMap, string element, string sett, NVector pos)
        {
            return onMap.data.ap;
        }

        protected override int ValueAct(Player player, string element, string sett)
        {
            throw new NotImplementedException();
        }

        protected override string Name(string element, string sett)
        {
            return "AP";
        }
    }
}