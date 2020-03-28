using System;
using Buildings;
using Game;
using Players;
using Tools;

namespace reqs
{
    public class ReqTown : ReqMinMaxPlayer
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
            return "Town";
        }
    }
    
    public class ReqOldTownMin : BaseReqOnlyPlayer
    {
        public override bool Check(Player player, string sett)
        {
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
    
    
    public class ReqOldTownMax : BaseReqOnlyPlayer
    {
        public override bool Check(Player player, string sett)
        {
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