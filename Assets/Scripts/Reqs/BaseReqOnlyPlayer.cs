using System;
using System.Linq;
using Buildings;
using Players;
using Tools;
using Towns;
using UnityEngine;

namespace reqs
{
    
    public abstract class BaseReqOnlyPlayer : BaseReq
    {
        public override bool Check(Player player, MapElementInfo onMap, string sett, NVector pos)
        {
            return Check(player, sett);
        }

        public override bool Final(Player player, MapElementInfo onMap, string sett, NVector pos)
        {
            return Final(player, sett);
        }

        public override string Desc(Player player, MapElementInfo onMap, string sett, NVector pos)
        {
            return Desc(player, sett);
        }
    }
}