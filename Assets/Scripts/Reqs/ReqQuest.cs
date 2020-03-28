using System;
using Players;
using UnityEngine;

namespace reqs
{
    public class ReqOldQuestMin : BaseReqOnlyPlayer
    {

        public override bool Check(Player player, string sett)
        {
            return player.quests.quests.Count >= Int32.Parse(sett);
        }

        public override bool Final(Player player, string sett)
        {
            return false;
        }

        public override string Desc(Player player, string sett)
        {
            return $"Need at least {sett} quest."+(player == null?"":$" You have only {player.quests.quests.Count} town.");
        }
    }
    
    
    public class ReqOldQuestMax : BaseReqOnlyPlayer
    {

        public override bool Check(Player player, string sett)
        {
            return player.quests.quests.Count <= Int32.Parse(sett);
        }

        public override bool Final(Player player, string sett)
        {
            return false;
        }

        public override string Desc(Player player, string sett)
        {
            return $"Need maximal {sett} quest."+(player == null?"":$" You have already {player.quests.quests.Count} town.");
        }
    }
}