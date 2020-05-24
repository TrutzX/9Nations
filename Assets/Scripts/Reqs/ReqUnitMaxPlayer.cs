using System;
using Game;
using Libraries;
using Players;
using Towns;
using Units;
using UnityEngine;

namespace reqs
{
    
    public class ReqUnitMaxPlayer : BaseReqOnlyPlayer
    {
        public override bool Check(Player player, string sett)
        {
            string unit = sett.Split(',')[0];
            int amount = Int32.Parse(sett.Split(',')[1]);

            return S.Unit().GetByPlayerType(player.id, unit).Length <= amount;
        }

        public override bool Final(Player player, string sett)
        {
            return false;
        }

        public override string Desc(Player player, string sett)
        {
            string unit = sett.Split(',')[0];
            int amount = Int32.Parse(sett.Split(',')[1]);
            
            return $"Need max {amount}x {L.b.units[unit].Name()}."+(player==null?"":$" You have {S.Unit().GetByPlayerType(player.id, unit).Length}x.");
        }
    }
}