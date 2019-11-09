using System;
using Buildings;
using Game;
using Players;
using Towns;
using Units;
using UnityEngine;

namespace reqs
{
    
    public class ReqHp : ReqMinMax
    {
        protected override int ValueMax(Player player, MapElementInfo onMap, string element, string sett, int x, int y)
        {
            return onMap.data.hpMax;
        }

        protected override int ValueMax(Player player, string element, string sett)
        {
            throw new NotImplementedException();
        }

        protected override int ValueAct(Player player, MapElementInfo onMap, string element, string sett, int x, int y)
        {
            return onMap.data.hp;
        }

        protected override int ValueAct(Player player, string element, string sett)
        {
            throw new NotImplementedException();
        }

        protected override string Name(string element, string sett)
        {
            return "HP";
        }
    }
}