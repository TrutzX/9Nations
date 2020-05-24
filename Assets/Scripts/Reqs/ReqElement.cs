using System.Linq;
using Game;
using Libraries;
using Players;
using Tools;
using UnityEngine;

namespace reqs
{
    public class ReqElement : BaseReqOnlyPlayer
    {

        public override bool Check(Player player, string sett)
        {
            return SplitHelper.Separator(sett).Any(e => player.elements.Contains(e));
        }

        public override bool Final(Player player, string sett)
        {
            return true;
        }

        public override string Desc(Player player, string sett)
        {
            var l = SplitHelper.Separator(sett);
            return S.T(LSys.tem.translations.GetPlural("reqElement",l.Length), L.b.elements.NameList(l));
        }
    }
}