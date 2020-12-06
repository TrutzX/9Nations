using System;
using Buildings;
using Game;
using Libraries;
using Players;
using Tools;
using Towns;
using Units;
using UnityEngine;
using MapElementInfo = MapElements.MapElementInfo;

namespace reqs
{
    
    public class ReqUnit : BaseReqMinMax
    {

        protected override int ValueMax(Player player, MapElementInfo onMap, string element, string sett, NVector pos)
        {
            throw new NotImplementedException();
        }

        protected override int ValueMax(Player player, string element, string sett)
        {
            throw new NotImplementedException();
        }
        protected override int ValueAct(Player player, MapElementInfo onMap, string element, string sett, NVector pos)
        {
            return S.Unit().GetByPlayerType(player.id, element).Length;
        }

        protected override int ValueAct(Player player, string element, string sett)
        {
            return S.Unit().GetByPlayerType(player.id, element).Length;
        }

        protected override string Name(string element, string sett)
        {
            return L.b.units[element].Name();
        }
    }
}