using System;
using Buildings;
using Game;
using Players;
using Tools;
using Towns;
using Units;
using UnityEngine;

namespace reqs
{
    
    public class ReqResMin : BaseReqOnlyMapElement
    {
        public override bool Check(Player player, MapElementInfo onMap, string sett, NVector pos)
        {
            var d = SplitHelper.SplitInt(sett);
            
            Town t = onMap==null?null:onMap.Town();
            return t != null && t.GetRes(d.key) >= d.value;
        }

        public override bool Check(Player player, string sett)
        {
            var d = SplitHelper.SplitInt(sett);
            
            return player.GetResTotal(d.key) >= d.value;
        }

        public override bool Final(Player player, MapElementInfo onMap, string sett, NVector pos)
        {
            return false;
        }

        public override string Desc(Player player, MapElementInfo onMap, string sett, NVector pos)
        {
            var d = SplitHelper.SplitInt(sett);
            
            Town t = onMap==null?null:onMap.Town();
            if (t == null)
            {
                return Desc(player, sett)+$" You have total {player.GetResTotal(d.key)}x.";
            }
            return Desc(player, sett)+$" You have {t.GetRes(d.key)}x.";
        }

        public override string Desc(Player player, string sett)
        {
            var d = SplitHelper.SplitInt(sett);
            
            return $"Need at least {d.value}x {Data.ress[d.key].name}.";
        }
    }
}