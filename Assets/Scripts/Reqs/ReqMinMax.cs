using System;
using Buildings;
using Game;
using Players;
using Towns;
using Units;
using UnityEngine;

namespace reqs
{
    
    public abstract class ReqMinMax : BaseReq
    {
        protected abstract int ValueMax(Player player, MapElementInfo onMap, string element, string sett, int x, int y);
        
        /// <summary>
        /// Maximal value, relevant for %
        /// </summary>
        /// <param name="player"></param>
        /// <param name="element"></param>
        /// <param name="sett"></param>
        /// <returns></returns>
        protected abstract int ValueMax(Player player, string element, string sett);
        protected abstract int ValueAct(Player player, MapElementInfo onMap, string element, string sett, int x, int y);
        
        /// <summary>
        /// Actual value
        /// </summary>
        /// <param name="player"></param>
        /// <param name="element"></param>
        /// <param name="sett"></param>
        /// <returns></returns>
        protected abstract int ValueAct(Player player, string element, string sett);
        
        protected abstract string Name(string element, string sett);
        
        public override bool Check(Player player, MapElementInfo onMap, string sett, int x, int y)
        {
            //%?
            if (sett.Split(',')[0].EndsWith("%"))
            {
                return CheckProcent(player, onMap, sett, x, y);
            }
            
            if (sett[0] == '>')
            {
                return ValueAct(player, onMap, GetElement(sett), sett, x, y) >= GetAmount(sett);
            }
            return ValueAct(player, onMap, GetElement(sett), sett, x, y) <= GetAmount(sett);
        }

        private bool CheckProcent(Player player, MapElementInfo onMap, string sett, int x, int y)
        {
            float goal = (ValueAct(player, onMap, GetElement(sett), sett, x, y)*1f /
                         ValueMax(player, onMap, GetElement(sett), sett, x, y))*100;
            
            if (sett[0] == '>')
            {
                return goal >= GetAmount(sett);
            }
            
            return goal <= GetAmount(sett);
        }

        protected string GetElement(string sett)
        {
            //has an element?
            string[] s = sett.Substring(1).Split(',');
            
            return (s.Length == 2)?s[1]:null;
        }

        /// <summary>
        /// Get the amount value
        /// </summary>
        /// <param name="sett"></param>
        /// <returns></returns>
        protected int GetAmount(string sett)
        {
            sett = sett.Substring(1).Split(',')[0];
            
            //cut last element?
            if (sett.EndsWith("%"))
                sett = sett.Substring(0, sett.Length - 1);
            
            return Int32.Parse(sett);
        }
        
        public override bool Check(Player player, string sett)
        {
            //%?
            if (sett.Split(',')[0].EndsWith("%"))
            {
                return CheckProcent(player, sett);
            }

            if (sett[0] == '>')
            {
                return ValueAct(player, GetElement(sett), sett) >= GetAmount(sett);
            }
            return ValueAct(player, GetElement(sett), sett) <= GetAmount(sett);
        }

        private bool CheckProcent(Player player, string sett)
        {
            float goal = (ValueAct(player, GetElement(sett), sett)*1f /
                          ValueMax(player, GetElement(sett), sett))*100;
            
            if (sett[0] == '>')
            {
                return goal >= GetAmount(sett);
            }
            
            return goal <= GetAmount(sett);
        }

        public override bool Final(Player player, string sett)
        {
            return false;
        }

        public override bool Final(Player player, MapElementInfo onMap, string sett, int x, int y)
        {
            return false;
        }

        public override string Desc(Player player, MapElementInfo onMap, string sett, int x, int y)
        {
            //TODO update for %
            return Desc(sett) + $" You have {ValueAct(player, onMap, GetElement(sett), sett, x, y)}x {Name(GetElement(sett),sett)}.";
        }

        public override string Desc(string sett)
        {
            //TODO update for %
            bool min = sett[0] == '>';

            return min ? $"Need at least {GetAmount(sett)}x {Name(GetElement(sett),sett)}." : $"Need maximal {GetAmount(sett)}x {Name(GetElement(sett),sett)}.";
        }
    }
}