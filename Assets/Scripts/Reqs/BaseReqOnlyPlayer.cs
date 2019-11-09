using System;
using System.Linq;
using Buildings;
using Players;
using Towns;
using UnityEngine;

namespace reqs
{
    
    public abstract class BaseReqOnlyPlayer : BaseReq
    {
        public override bool Check(Player player, MapElementInfo onMap, string sett, int x, int y)
        {
            return Check(player, sett);
        }

        public override bool Final(Player player, MapElementInfo onMap, string sett, int x, int y)
        {
            return Final(player, sett);
        }
    }
}