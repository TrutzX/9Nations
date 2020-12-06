using System.Linq;
using Game;
using Libraries;
using Players;
using Tools;
using UnityEngine;

namespace reqs
{
    
    public class ReqResearch : BaseReqOnlyPlayer
    {
        public override bool Check(Player player, string sett)
        {
            string[] research = SplitHelper.Separator(sett);
            return research.Any(res => player.research.IsFinish(res));
        }

        public override bool Final(Player player, string sett)
        {
            return true;
        }

        public override string Desc(Player player, string sett)
        {
            return $"Need the research {sett}.";
            //return $"Need the research {L.b.researches[sett].name}.";
        }
    }
}