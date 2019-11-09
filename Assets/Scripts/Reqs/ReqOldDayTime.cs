using Players;
using UnityEngine;

namespace reqs
{
    
    public class ReqOldDayTime : BaseReqOld
    {
        public override bool Check(Player player, GameObject onMap, string sett, int x, int y)
        {
            return Check(player, sett);
        }

        public override bool Check(Player player, string sett)
        {
            if (sett.StartsWith("not-"))
            {
                return !RoundMgmt.Get().IsDayTime(sett.Split('-')[1]);
            }
            return RoundMgmt.Get().IsDayTime(sett);
        }

        public override bool Final(Player player, GameObject onMap, string sett, int x, int y)
        {
            return false;
        }

        public override string Desc(Player player, GameObject onMap, string sett, int x, int y)
        {
            return Desc(sett);
        }

        public override string Desc(string sett)
        {
            if (sett.StartsWith("not-"))
            {
                return $"Can only not used in the day time {sett.Split('-')[1]}.";
            }
            return $"Can only used in the day time {sett}.";
        }
    }
}