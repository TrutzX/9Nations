using System;
using System.Linq;
using Game;
using Libraries;
using Players;
using Tools;
using UnityEngine;

namespace reqs
{
    
    public class ReqResearch : BaseReqOnlyPlayer
    {
        public override bool Check(Player player, string sett)
        {
            string[] research = SplitHelper.Separator(sett);
            return research.Any(res => player.research.IsFinish(res));
        }

        public override bool Final(Player player, string sett)
        {
            return true;
        }

        public override string Desc(Player player, string sett)
        {
            var names = SplitHelper.Separator(sett).Select(s => L.b.researches[s].Name());
            return S.T("reqResearch",TextHelper.CommaSep(names.ToArray()));
        }
    }
}