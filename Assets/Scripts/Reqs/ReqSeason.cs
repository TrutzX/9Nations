using Players;
using UnityEngine;

namespace reqs
{
    
    public class ReqSeason : BaseReq
    {
        public override bool Check(Player player, GameObject onMap, string sett, int x, int y)
        {
            return Check(player, sett);
        }

        public override bool Check(Player player, string sett)
        {
            if (sett.StartsWith("not-"))
            {
                return !RoundMgmt.Get().IsSeason(sett.Split('-')[1]);
            }
            return RoundMgmt.Get().IsSeason(sett);
        }

        public override bool Final(Player player, GameObject onMap, string sett, int x, int y)
        {
            return false;
        }

        public override string Desc(Player player, GameObject onMap, string sett, int x, int y)
        {
            return Desc(sett)+$" At the moment it is {RoundMgmt.Get().GetSeasonString()}.";
        }

        public override string Desc(string sett)
        {
            if (sett.StartsWith("not-"))
            {
                return $"Can only not used in the season {sett.Split('-')[1]}.";
            }
            return $"Can only used in the season {sett}.";
        }
    }
}