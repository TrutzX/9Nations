using System;
using Buildings;
using Game;
using Players;
using Tools;
using Towns;
using Units;
using UnityEngine;
using MapElementInfo = MapElements.MapElementInfo;

namespace reqs
{
    
    public class ReqStat : BaseReqMinMax
    {
        protected override int ValueMax(Player player, MapElementInfo onMap, string element, string sett, NVector pos)
        {
            //todo
            return 10;
        }

        protected override int ValueMax(Player player, string element, string sett)
        {
            throw new NotImplementedException();
        }

        protected override int ValueAct(Player player, MapElementInfo onMap, string element, string sett, NVector pos)
        {
            switch (element)
            {
                case "atk":
                    return onMap.data.atk;
                case "def":
                    return onMap.data.def;
                default:
                    throw new ArgumentException("Argument "+sett+" is missing");
            }
        }

        protected override int ValueAct(Player player, string element, string sett)
        {
            throw new NotImplementedException();
        }

        protected override string Name(string element, string sett)
        {
            return S.T(element);
        }
    }
}