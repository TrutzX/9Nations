using Game;
using Players;
using UnityEngine;

namespace reqs
{
    public class ReqGame : BaseReqOnlyPlayer
    {

        public override bool Check(Player player, string sett)
        {
            return GameHelper.IsGame();
        }

        public override bool Final(Player player, string sett)
        {
            return true;
        }

        public override string Desc(Player player, string sett)
        {
            return $"Need a running game.";
        }
    }
}