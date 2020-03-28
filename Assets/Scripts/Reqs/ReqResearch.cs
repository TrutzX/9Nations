using Libraries;
using Players;
using UnityEngine;

namespace reqs
{
    
    public class ReqResearch : BaseReqOnlyPlayer
    {
        public override bool Check(Player player, string sett)
        {
            return player.research.IsFinish(sett);
        }

        public override bool Final(Player player, string sett)
        {
            return true;
        }

        public override string Desc(Player player, string sett)
        {
            return $"Need the research {L.b.researches[sett].name}.";
        }
    }
}