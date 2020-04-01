using System;
using System.Linq;
using Buildings;
using Game;
using Players;
using Tools;
using Towns;
using Units;
using UnityEngine;
using MapElementInfo = Buildings.MapElementInfo;
using Object = UnityEngine.Object;

namespace reqs
{
    public class ReqUnitOwn : BaseReqNeg
    {
        protected override bool CheckIntern(BaseReqArgument bra)
        {
            bra.NeedMapException();
            
            Debug.Log(bra.sett);
            Debug.Log(bra.onMap);
            Debug.Log(bra.pos);

            if (bra.onMap == null)
                bra.onMap = S.Unit().At(bra.pos);

            if (bra.onMap == null) return false;
            
            switch (bra.sett)
            {
                case "own":
                    return bra.onMap.Player() == bra.player;
                case "enemy":
                    return bra.onMap.Player() != bra.player;
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
            string mess; 
            switch (bra.sett)
            {
                case "own":
                    mess = "The unit muss be your own unit.";
                    break;
                case "enemy":
                    mess = "The unit muss not be your own unit.";
                    break;
                default:
                    mess = bra.sett + " is unknown.";
                    break;
            }

            if (bra.NeedMap())
            {
                if (bra.onMap == null)
                    bra.onMap = S.Unit().At(bra.pos);

                if (bra.onMap == null)
                {
                    mess += $" Here is no unit.";
                }
                else
                {
                    mess += $" {bra.onMap.name} is " + (bra.player == bra.onMap.Player() ? "" : "not") +
                            " your own unit";
                }
                
                
            }
            
            return mess;
        }
    }
}