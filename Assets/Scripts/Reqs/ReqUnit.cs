using System;
using System.Linq;
using Buildings;
using Players;
using Towns;
using UnityEngine;

namespace reqs
{
    
    public class ReqUnit : BaseReq
    {
        public override bool Check(Player player, MapElementInfo onMap, string sett, int x, int y)
        {
            switch (sett)
            {
                case "own":
                    return onMap.Player() == player;
                case "enemy":
                    return onMap.Player() != player;
                default:
                    throw new ArgumentException(sett +" is unknown.");
            }
        }

        public override bool Check(Player player, string sett)
        {
            throw new NotImplementedException();
        }

        public override bool Final(Player player, MapElementInfo onMap, string sett, int x, int y)
        {
            return false;
        }

        public override bool Final(Player player, string sett)
        {
            throw new NotImplementedException();
        }

        public override string Desc(Player player, MapElementInfo onMap, string sett, int x, int y)
        {
            string mess = $"{Desc(sett)} {onMap.data.name} is "+(player == onMap.Player() ? "" : "not") +
                          " your own unit";
            return mess;
        }

        public override string Desc(string sett)
        {
            switch (sett)
            {
                case "own":
                    return "The unit muss be your own unit.";
                case "enemy":
                    return "The unit muss not be your own unit.";
                default:
                    throw new ArgumentException(sett +" is unknown.");
            }
        }
    }
}