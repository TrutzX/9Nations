using System;
using System.Linq;
using Players;
using Towns;
using UnityEngine;

namespace reqs
{
    
    public class ReqTownLevel : BaseReq
    {
        public override bool Check(Player player, GameObject onMap, string sett, int x, int y)
        {
            Town t = TownMgmt.Get().NearstTown(player, x, y, false);
            return t.level >= Int32.Parse(sett);
        }

        public override bool Check(Player player, string sett)
        {
            return TownMgmt.Get().GetByPlayer(player.id).Max(t => t.level) >= Int32.Parse(sett);
        }

        public override bool Final(Player player, GameObject onMap, string sett, int x, int y)
        {
            return false;
        }

        public override string Desc(Player player, GameObject onMap, string sett, int x, int y)
        {
            Town t = TownMgmt.Get().NearstTown(player, x, y, false);
            return Desc(sett)+$" Has at the moment {t.GetTownLevelName()}";
        }

        public override string Desc(string sett)
        {
            return $"Needs the town level {Int32.Parse(sett)}.";
        }
    }
}