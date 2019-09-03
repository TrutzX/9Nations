using System;
using Players;
using UnityEngine;

namespace reqs
{
    
    public class ReqTownMin : BaseReq
    {
        public override bool Check(Player player, GameObject onMap, string sett, int x, int y)
        {
            return Check(player, sett);
        }

        public override bool Check(Player player, string sett)
        {
            return TownMgmt.Get().GetByPlayer(player.id).Count >= Int32.Parse(sett);
        }

        public override bool Final(Player player, GameObject onMap, string sett, int x, int y)
        {
            return false;
        }

        public override string Desc(Player player, GameObject onMap, string sett, int x, int y)
        {
            return Desc(sett)+$" You have only {TownMgmt.Get().GetByPlayer(player.id).Count} town.";
        }

        public override string Desc(string sett)
        {
            return $"Need at least {sett} town.";
        }
    }
    
    
    public class ReqTownMax : BaseReq
    {
        public override bool Check(Player player, GameObject onMap, string sett, int x, int y)
        {
            return Check(player, sett);
        }

        public override bool Check(Player player, string sett)
        {
            return TownMgmt.Get().GetByPlayer(player.id).Count <= Int32.Parse(sett);
        }

        public override bool Final(Player player, GameObject onMap, string sett, int x, int y)
        {
            return false;
        }

        public override string Desc(Player player, GameObject onMap, string sett, int x, int y)
        {
            return Desc(sett)+$" You have already {TownMgmt.Get().GetByPlayer(player.id).Count} town.";
        }

        public override string Desc(string sett)
        {
            return $"Need maximal {sett} town.";
        }
    }
}