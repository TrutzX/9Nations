using System;
using Game;
using Players;
using UnityEngine;

namespace reqs
{
    
    public class ReqDayOfYear : BaseReqMinMaxPlayer
    {

        protected override int ValueMax(Player player, string element, string sett)
        {
            return DateTime.IsLeapYear(DateTime.Today.Year)?366:365;
        }

        protected override int ValueAct(Player player, string element, string sett)
        {
            return DateTime.Today.DayOfYear;
        }

        protected override string Name(string element, string sett)
        {
            return "day of the year";
        }
    }
}