using System;
using Players;
using UnityEngine;

namespace reqs
{
    
    public class ReqQuestMin : BaseReq
    {
        public override bool Check(Player player, GameObject onMap, string sett, int x, int y)
        {
            return Check(player, sett);
        }

        public override bool Check(Player player, string sett)
        {
            return player.quests.quests.Count >= Int32.Parse(sett);
        }

        public override bool Final(Player player, GameObject onMap, string sett, int x, int y)
        {
            return false;
        }

        public override string Desc(Player player, GameObject onMap, string sett, int x, int y)
        {
            return Desc(sett)+$" You have only {player.quests.quests.Count} town.";
        }

        public override string Desc(string sett)
        {
            return $"Need at least {sett} quest.";
        }
    }
    
    
    public class ReqQuestMax : BaseReq
    {
        public override bool Check(Player player, GameObject onMap, string sett, int x, int y)
        {
            return Check(player, sett);
        }

        public override bool Check(Player player, string sett)
        {
            return player.quests.quests.Count <= Int32.Parse(sett);
        }

        public override bool Final(Player player, GameObject onMap, string sett, int x, int y)
        {
            return false;
        }

        public override string Desc(Player player, GameObject onMap, string sett, int x, int y)
        {
            return Desc(sett)+$" You have already {player.quests.quests.Count} town.";
        }

        public override string Desc(string sett)
        {
            return $"Need maximal {sett} quest.";
        }
    }
}