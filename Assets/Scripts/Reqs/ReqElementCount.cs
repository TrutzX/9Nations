using System;
using Buildings;
using Game;
using Players;
using Tools;

namespace reqs
{
    public class ReqElementCount : BaseReqMinMaxPlayer
    {

        protected override int ValueMax(Player player, string element, string sett)
        {
            return player.Nation().maxElement;
        }

        protected override int ValueAct(Player player, string element, string sett)
        {
            return player.Nation().elements.Count;
        }

        protected override string Name(string element, string sett)
        {
            return "element";
        }
    }
}