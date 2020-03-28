using System;
using System.Linq;
using Buildings;
using Players;
using Tools;
using Towns;
using UnityEngine;

namespace reqs
{
    
    public abstract class BaseReqOnlyMapElement : BaseReq
    {
        public override bool Check(Player player, string sett)
        {
            Debug.LogWarning($"Req {id}: Check only player not implemented");
            return false;
        }

        public override bool Final(Player player, string sett)
        {
            Debug.LogWarning($"Req {id}: Final only player not implemented");
            return false;
        }
    }
}