using System;
using Buildings;
using Game;
using Players;
using Towns;
using Units;
using UnityEngine;

namespace reqs
{
    
    public class ReqBuilding : ReqMinMax
    {

        protected override int ValueMax(Player player, MapElementInfo onMap, string element, string sett, int x, int y)
        {
            throw new NotImplementedException();
        }

        protected override int ValueMax(Player player, string element, string sett)
        {
            throw new NotImplementedException();
        }
        protected override int ValueAct(Player player, MapElementInfo onMap, string element, string sett, int x, int y)
        {
            
            Town t = onMap.Town();
            if (t == null)
            {
                return ValueAct(player, element, sett);
            }
            
            return BuildingMgmt.Get().GetByTownType(t.id, element).Length;
        }

        protected override int ValueAct(Player player, string element, string sett)
        {
            return BuildingMgmt.Get().GetByPlayerType(player.id, element).Length;
        }

        protected override string Name(string element, string sett)
        {
            return Data.building[element].name;
        }
    }
}