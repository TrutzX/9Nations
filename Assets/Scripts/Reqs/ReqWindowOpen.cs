using System;
using Game;
using Players;
using UI;
using UnityEngine;

namespace reqs
{
    public class ReqWindowOpen : BaseReqOnlyPlayer
    {

        public override bool Check(Player player, string sett)
        {
            return Boolean.Parse(sett) == WindowsMgmt.Get().AnyOpenWindow();
        }

        public override bool Final(Player player, string sett)
        {
            return true;
        }

        public override string Desc(Player player, string sett)
        {
            return sett=="false"?"No window allowed to open":"Need a open window";
        }
    }
}