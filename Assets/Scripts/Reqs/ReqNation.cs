using Libraries;
using Players;
using UnityEngine;

namespace reqs
{
    
    public class ReqNation : BaseReqOnlyPlayer
    {
        public override bool Check(Player player, string sett)
        {
            return player.nation == sett;
        }

        public override bool Final(Player player, string sett)
        {
            return true;
        }

        public override string Desc(Player player, string sett)
        {
            return $"Can only build from the nation {L.b.nations[sett].name}.";
        }
    }
}