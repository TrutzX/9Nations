using System;
using Players;
using Tools;
using UnityEngine;

namespace reqs
{
    
    public class ReqFeature : BaseReqOnlyPlayer
    {
        public override bool Check(Player player, string sett)
        {
            var v = SplitHelper.Split(sett);
            return Data.features[v.key].Same(v.value);
        }

        public override bool Final(Player player, string sett)
        {
            return true;
        }

        public override string Desc(Player player, string sett)
        {
            return $"Need the feature {SplitHelper.Split(sett).key}.";
        }
    }
}