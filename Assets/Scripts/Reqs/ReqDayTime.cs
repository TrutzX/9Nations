using Buildings;
using Game;
using JetBrains.Annotations;
using Libraries.Rounds;
using Players;
using Tools;
using UnityEngine;

namespace reqs
{
    
    public class ReqDayTime : BaseReqOnlyPlayer
    {
        public override bool Check(Player player, string sett)
        {
            if (sett.StartsWith("not-"))
            {
                return !S.Round().IsDayTime(sett.Split('-')[1]);
            }
            return S.Round().IsDayTime(sett);
        }

        public override bool Final(Player player, string sett)
        {
            return false;
        }

        public override string Desc(Player player, string sett)
        {
            if (sett.StartsWith("not-"))
            {
                return $"Can only not used in the day time {sett.Split('-')[1]}.";
            }
            return $"Can only used in the day time {sett}.";
        }
    }
}