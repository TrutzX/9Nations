using System;
using Game;
using Players;
using UnityEngine;

namespace reqs
{
    
    public class ReqQuestCount : ReqMinMaxPlayer
    {

        protected override int ValueMax(Player player, string element, string sett)
        {
            //TODO
            return 10;
        }

        protected override int ValueAct(Player player, string element, string sett)
        {
            return player.quests.quests.Count;
        }

        protected override string Name(string element, string sett)
        {
            return "quest";
        }
    }
    
    [Obsolete]
    public class ReqOldQuestMin : BaseReqOnlyPlayer
    {

        public override bool Check(Player player, string sett)
        {
            Debug.LogError("Obsolete ReqOldQuestMin");
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

    [Obsolete]
    public class ReqOldQuestMax : BaseReqOnlyPlayer
    {

        public override bool Check(Player player, string sett)
        {
            Debug.LogError("Obsolete ReqOldQuestMax");
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