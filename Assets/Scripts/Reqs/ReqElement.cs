using Players;
using UnityEngine;

namespace reqs
{
    public class ReqElement : BaseReqOnlyPlayer
    {

        public override bool Check(Player player, string sett)
        {

            foreach (var e in sett.Split(';'))
            {
                if (player.elements.Contains(e))
                    return true;
            }

            return false;
        }

        public override bool Final(Player player, string sett)
        {
            return true;
        }

        public override string Desc(Player player, string sett)
        {
            return $"Need the element {sett}.";
        }
    }
}