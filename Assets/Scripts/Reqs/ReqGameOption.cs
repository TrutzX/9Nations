using System;
using Libraries;
using Players;
using Tools;
using UnityEngine;

namespace reqs
{
    
    public class ReqGameOption : BaseReqOnlyPlayer
    {
        public override bool Check(Player player, string sett)
        {
            var v = SplitHelper.Split(sett);
            return L.b.gameOptions[v.key].Same(v.value);
        }

        public override bool Final(Player player, string sett)
        {
            return true;
        }

        public override string Desc(Player player, string sett)
        {
            return $"Need the game feature {SplitHelper.Split(sett).key}.";
        }
    }
}