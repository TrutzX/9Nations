using System;
using System.Linq;
using Buildings;

using Endless;
using Game;
using NesScripts.Controls.PathFind;
using Players;
using Tools;
using Towns;
using UnityEngine;

namespace reqs
{
    
    public class ReqDisabled : BaseReqOnlyPlayer
    {
        
        public override bool Check(Player player, string sett)
        {
            return false;
        }

        public override bool Final(Player player, string sett)
        {
            return true;
        }

        public override string Desc(Player player, string sett)
        {
            return "This element is disabled";
        }
    }
}