using System;
using Players;
using UnityEngine;

namespace reqs
{
    
    public class ReqFeature : BaseReq
    {
        public override bool Check(Player player, GameObject onMap, string sett, int x, int y)
        {
            return Check(player, sett);
        }

        public override bool Check(Player player, string sett)
        {
            string[] v = sett.Split(',');
            return Data.features[v[0]].Same(v[1]);
        }

        public override bool Final(Player player, GameObject onMap, string sett, int x, int y)
        {
            return true;
        }

        public override string Desc(Player player, GameObject onMap, string sett, int x, int y)
        {
            return Desc(sett);
        }

        public override string Desc(string sett)
        {
            return $"Need the feature {sett.Split(',')[0]}.";
        }
    }
    
    public class ReqFeaturePlayer : BaseReq
    {
        public override bool Check(Player player, GameObject onMap, string sett, int x, int y)
        {
            return Check(player, sett);
        }

        public override bool Check(Player player, string sett)
        {
            string[] v = sett.Split(',');
            return player.GetFeature(v[0]) == v[1];
        }

        public override bool Final(Player player, GameObject onMap, string sett, int x, int y)
        {
            return true;
        }

        public override string Desc(Player player, GameObject onMap, string sett, int x, int y)
        {
            return Desc(sett);
        }

        public override string Desc(string sett)
        {
            return $"Need the feature {sett.Split(',')[0]}.";
        }
    }
}