using System;
using System.Linq;
using Buildings;
using Players;
using Towns;
using UnityEngine;

namespace reqs
{
    
    public class ReqQuestFinish : BaseReqOnlyPlayer
    {

        public override bool Check(Player player, string sett)
        {
            if (player.quests.quests.Count(q =>q.id == sett) > 0)
            {
                return player.quests.quests.Single(q => q.id == sett).IsFinish();
            }

            return false;
        }

        public override bool Final(Player player, string sett)
        {
            return true;
        }

        public override string Desc(Player player, MapElementInfo onMap, string sett, int x, int y)
        {
            if (player.quests.quests.Count(q =>q.id == sett) > 0)
            {
                Quest qu = player.quests.quests.Single(q => q.id == sett);
                return "Finish the quest " + qu.name + ". Status: " + (qu.IsFinish() ? "Finished" : "In Progress");
            }

            return Desc(sett);
        }

        public override string Desc(string sett)
        {
            return "Finish the quest " + sett;
        }
    }
}