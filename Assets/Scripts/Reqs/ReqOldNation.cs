using Libraries;
using Players;
using UnityEngine;

namespace reqs
{
    
    public class ReqOldNation : BaseReqOld
    {
        public override bool Check(Player player, GameObject onMap, string sett, int x, int y)
        {
            return Check(player, sett);
        }

        public override bool Check(Player player, string sett)
        {
            return player.nation == sett;
        }

        public override bool Final(Player player, GameObject onMap, string sett, int x, int y)
        {
            return true;
        }

        public override string Desc(Player player, GameObject onMap, string sett, int x, int y)
        {
            return Desc(sett);
        }

        public override string Desc(string sett)
        {
            return $"Can only build from the nation {L.b.nations[sett].Name}.";
        }
    }
}