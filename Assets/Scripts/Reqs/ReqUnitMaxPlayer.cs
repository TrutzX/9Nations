using System;
using Players;
using Towns;
using UnityEngine;

namespace reqs
{
    
    public class ReqUnitMaxPlayer : BaseReq
    {
        public override bool Check(Player player, GameObject onMap, string sett, int x, int y)
        {
            return Check(player, sett);
        }

        public override bool Check(Player player, string sett)
        {
            string unit = sett.Split(',')[0];
            int amount = Int32.Parse(sett.Split(',')[1]);

            return UnitMgmt.Get().GetUnitPlayerType(player.id, unit).Length >= amount;
        }

        public override bool Final(Player player, GameObject onMap, string sett, int x, int y)
        {
            return false;
        }

        public override string Desc(Player player, GameObject onMap, string sett, int x, int y)
        {
            string unit = sett.Split(',')[0];
            int amount = Int32.Parse(sett.Split(',')[1]);

            return Desc(sett)+$" You have {UnitMgmt.Get().GetUnitPlayerType(player.id, unit).Length}x.";
        }

        public override string Desc(string sett)
        {
            string unit = sett.Split(',')[0];
            int amount = Int32.Parse(sett.Split(',')[1]);
            
            return $"Need max {amount}x {Data.unit[unit].name}.";
        }
    }
}