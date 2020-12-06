using System;
using System.Linq;
using Buildings;
using Game;
using Players;
using Tools;
using Towns;
using Units;
using UnityEngine;
using MapElementInfo = MapElements.MapElementInfo;
using Object = UnityEngine.Object;

namespace reqs
{
    public class ReqBuildingOwn : BaseReqNeg
    {
        protected override bool CheckIntern(BaseReqArgument bra)
        {
            bra.NeedMapException();

            //if (bra.onMap == null)
                bra.onMap = S.Building().At(bra.pos);

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
                    mess = "The building muss be your own building.";
                    break;
                case "enemy":
                    mess = "The building muss not be your own building.";
                    break;
                default:
                    mess = bra.sett + " is unknown.";
                    break;
            }

            if (bra.NeedMap())
            {
                //if (bra.onMap == null)
                    bra.onMap = S.Building().At(bra.pos);

                if (bra.onMap == null)
                {
                    mess += $" Here is no building.";
                }
                else
                {
                    mess += $" {bra.onMap.name} is " + (bra.player == bra.onMap.Player() ? "" : "not") +
                            " your own building";
                }
                
                
            }
            
            return mess;
        }
    }
}