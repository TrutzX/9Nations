using System;
using System.Linq;
using Buildings;

using Game;
using MapElements;
using MapElements.Buildings;
using MapElements.Units;
using Players;
using Tools;
using Towns;
using Units;
using UnityEngine;

namespace reqs
{
    
    public class ReqTownLevel : BaseReqMinMax
    {
        protected override int ValueMax(Player player, MapElementInfo onMap, string element, string sett, NVector pos)
        {
            return ValueMax(player, element, sett);
        }

        protected override int ValueMax(Player player, string element, string sett)
        {
            //TODO
            return 5;
        }

        protected override int ValueAct(Player player, MapElementInfo onMap, string element, string sett, NVector pos)
        {
            //has it?
            if (onMap != null)
            {
                return onMap.Town()?.level??0;
            }
            //has the field it?
            UnitInfo u = S.Unit().At(pos);
            if (u != null)
            {
                return u.Town()?.level??0;
            }
            //has the field it?
            BuildingInfo b = S.Building().At(pos);
            if (b != null)
            {
                return b.Town()?.level??0;
            }
            //get the nearest town
            Town t = S.Towns().NearestTown(player, pos,false);
            
            return t?.level??0;
        }

        protected override int ValueAct(Player player, string element, string sett)
        {
            return S.Towns().GetByPlayer(player.id)?[0].level??0;
        }

        protected override string Name(string element, string sett)
        {
            return "town level";
        }
    }
}