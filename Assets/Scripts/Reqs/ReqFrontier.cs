using System;
using System.Linq;
using Buildings;
using Game;
using Players;
using Tools;
using Towns;
using UI;
using Units;
using UnityEngine;
using MapElementInfo = Buildings.MapElementInfo;
using Object = UnityEngine.Object;

namespace reqs
{
    public class ReqFrontier : BaseReqNeg
    {
        protected override bool CheckIntern(BaseReqArgument bra)
        {
            bra.NeedMapException();

            if (bra.sett == "near")
            {
                foreach (var pos in CircleGenerator.Gen(bra.pos, 1))
                {
                    Debug.Log(pos+" "+S.Players().OverlayHighest("frontier", pos));
                    if (S.Players().OverlayHighest("frontier", pos) == bra.player)
                        return true;
                }
                return false;
            }
            
            var p = S.Players().OverlayHighest("frontier", bra.pos);

            switch (bra.sett)
            {
                case "own":
                    return p == bra.player;
                case "enemy":
                    return p != null && p != bra.player;
                case "none":
                    return p == null;
                default:
                    throw new ArgumentException(bra.sett + " is unknown.");
            }
        }

        protected override bool FinalIntern(BaseReqArgument bra)
        {
            return false;
        }

        protected override string DescIntern(BaseReqArgument bra)
        {
            string mess = S.T("frontier"+TextHelper.Cap(bra.sett));
            
            if (bra.NeedMap())
            {
                var p = S.Players().OverlayHighest("frontier", bra.pos);
                if (p == bra.player)
                    mess = S.T("frontierOwnHere",mess);
                else if (p != null && p != bra.player)
                    mess = S.T("frontierEnemyHere",mess);
                else if (p == null)
                    mess = S.T("frontierNoneHere",mess);
            }

            return mess;
        }
    }
}