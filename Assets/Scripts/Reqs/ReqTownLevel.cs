using System;
using System.Linq;
using Buildings;
using DataTypes;
using Players;
using Towns;
using Units;
using UnityEngine;

namespace reqs
{
    
    public class ReqTownLevel : ReqMinMax
    {
        protected override int ValueMax(Player player, MapElementInfo onMap, string element, string sett, int x, int y)
        {
            return 5;
        }

        protected override int ValueMax(Player player, string element, string sett)
        {
            return 5;
        }

        protected override int ValueAct(Player player, MapElementInfo onMap, string element, string sett, int x, int y)
        {
            //has it?
            if (onMap != null)
            {
                return onMap.Town()?.level??0;
            }
            //has the field it?
            UnitInfo u = UnitMgmt.At(x, y);
            if (u != null)
            {
                return u.Town()?.level??0;
            }
            //has the field it?
            BuildingInfo b = BuildingMgmt.At(x, y);
            if (b != null)
            {
                return b.Town()?.level??0;
            }
            //get the nearest town
            Town t = TownMgmt.Get().NearstTown(player, x, y,false);
            
            return t?.level??0;
        }

        protected override int ValueAct(Player player, string element, string sett)
        {
            return TownMgmt.Get().GetByPlayer(player.id)?[0].level??0;
        }

        protected override string Name(string element, string sett)
        {
            return "town level";
        }
    }
}