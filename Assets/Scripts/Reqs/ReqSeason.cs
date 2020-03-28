using Game;
using Libraries.Rounds;
using Players;
using UnityEngine;

namespace reqs
{
    
    public class ReqSeason : BaseReqOnlyPlayer
    {
        public override bool Check(Player player, string sett)
        {
            if (sett.StartsWith("not-"))
            {
                return !S.Round().IsSeason(sett.Split('-')[1]);
            }
            return S.Round().IsSeason(sett);
        }

        public override bool Final(Player player, string sett)
        {
            return false;
        }

        public override string Desc(Player player, string sett)
        {
            string e = $"Can only used in the season {sett}.";
            if (sett.StartsWith("not-"))
            {
                e = $"Can only not used in the season {sett.Split('-')[1]}.";
            }

            if (player != null)
            {
                e += $" At the moment it is {S.Round().GetSeasonString()}.";
            }

            return e;
        }
    }
}