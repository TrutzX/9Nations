using System;
using System.Linq;
using Buildings;
using DataTypes;
using Endless;
using Game;
using NesScripts.Controls.PathFind;
using Players;
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

        public override string Desc(Player player, MapElementInfo onMap, string sett, int x, int y)
        {
            return Desc(sett);
        }

        public override string Desc(string sett)
        {
            return "This element is disabled";
        }
    }
}