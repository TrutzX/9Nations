using System;
using Buildings;
using Game;
using Players;
using Tools;
using UnityEngine;

namespace reqs
{
    public class ReqTownCount : ReqMinMaxPlayer
    {

        protected override int ValueMax(Player player, string element, string sett)
        {
            //TODO
            return 10;
        }

        protected override int ValueAct(Player player, string element, string sett)
        {
            return S.Towns().GetByPlayer(player.id).Count;
        }

        protected override string Name(string element, string sett)
        {
            return "town";
        }
    }
    
    [Obsolete]
    public class ReqOldTownMin : BaseReqOnlyPlayer
    {
        public override bool Check(Player player, string sett)
        {
            Debug.LogError("Obsolete ReqOldTownMin");
            return S.Towns().GetByPlayer(player.id).Count >= Int32.Parse(sett);
        }

        public override bool Final(Player player, string sett)
        {
            return false;
        }

        public override string Desc(Player player, string sett)
        {
            return $"Need at least {sett} town."+(player==null?"":$" You have only {S.Towns().GetByPlayer(player.id).Count} town.");
        }
    }
    
    [Obsolete]
    public class ReqOldTownMax : BaseReqOnlyPlayer
    {
        public override bool Check(Player player, string sett)
        {
            Debug.LogError("Obsolete ReqOldTownMax");
            return S.Towns().GetByPlayer(player.id).Count <= Int32.Parse(sett);
        }

        public override bool Final(Player player, string sett)
        {
            return false;
        }

        public override string Desc(Player player, string sett)
        {
            return $"Need maximal {sett} town."+(player==null?"":$" You have already {S.Towns().GetByPlayer(player.id).Count} town.");
        }
    }
}