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
        protected override int Value(Player player, GameObject onMap, string element, string sett, int x, int y)
        {
            
            MapElementInfo info = GameHelper.GetMapElement(onMap);
            Town t = info.Town();
            if (t == null)
            {
                return Value(player, element, sett);
            }
            
            return BuildingMgmt.Get().GetByTownType(t.id, element).Length;
        }

        protected override int Value(Player player, string element, string sett)
        {
            return BuildingMgmt.Get().GetByPlayerType(player.id, element).Length;
        }

        protected override string Name(string element, string sett)
        {
            return Data.building[element].name;
        }
    }
}