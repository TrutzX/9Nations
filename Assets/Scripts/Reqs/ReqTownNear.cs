using System;
using Buildings;
using Game;
using Players;
using Tools;
using Towns;
using UnityEngine;

namespace reqs
{
    
    public class ReqTownNear : BaseReqOnlyMapElement
    {
        public override bool Check(Player player, MapElementInfo onMap, string sett, NVector pos)
        {
            return S.Towns().NearstTown(player, pos, true)!=null;
        }

        public override bool Final(Player player,MapElementInfo onMap, string sett, NVector pos)
        {
            return false;
        }

        public override string Desc(Player player,MapElementInfo onMap, string sett, NVector pos)
        {
            return Desc(player, sett);
        }

        public override string Desc(Player player, string sett)
        {
                return $"Needs on the next field a town building.";
        }
    }
}