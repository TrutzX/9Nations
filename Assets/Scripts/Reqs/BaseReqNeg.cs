using System;
using System.Linq;
using System.Net.Security;
using Buildings;
using MapElements;
using Players;
using Tools;
using Towns;
using UnityEngine;

namespace reqs
{
    public abstract class BaseReqNeg : BaseReq
    {
        private (bool neg, BaseReqArgument bra) Neg(Player player, MapElementInfo onMap, string sett, NVector pos)
        {
            bool neg = false;
            if (!string.IsNullOrEmpty(sett) && sett.StartsWith("!"))
            {
                neg = true;
                sett = sett.Substring(1);
            }

            return (neg, new BaseReqArgument(player, sett, onMap, pos));
        }

        public override bool Check(Player player, string sett)
        {
            var erg = Neg(player, null, sett, null);

            return erg.neg ? !CheckIntern(erg.bra) : CheckIntern(erg.bra);
        }

        public override bool Check(Player player, MapElementInfo onMap, string sett, NVector pos)
        {
            var erg = Neg(player, onMap, sett, pos);

            return erg.neg ? !CheckIntern(erg.bra) : CheckIntern(erg.bra);
        }

        protected abstract bool CheckIntern(BaseReqArgument bra);

        public override bool Final(Player player, string sett)
        {
            var erg = Neg(player, null, sett, null);

            return FinalIntern(erg.bra);
        }

        public override bool Final(Player player, MapElementInfo onMap, string sett, NVector pos)
        {
            var erg = Neg(player, onMap, sett, pos);

            return FinalIntern(erg.bra);
        }

        protected abstract bool FinalIntern(BaseReqArgument bra);

        public override string Desc(Player player, string sett)
        {
            var erg = Neg(player, null, sett, null);

            return erg.neg ? "Not "+DescIntern(erg.bra) : DescIntern(erg.bra);
        }

        public override string Desc(Player player, MapElementInfo onMap, string sett, NVector pos)
        {
            var erg = Neg(player, onMap, sett, pos);

            return erg.neg ? "Not "+DescIntern(erg.bra) : DescIntern(erg.bra);
        }

        protected abstract string DescIntern(BaseReqArgument bra);
    }
}