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
        protected abstract int Value(Player player, GameObject onMap, string element, string sett, int x, int y);
        
        protected abstract int Value(Player player, string element, string sett);
        
        protected abstract string Name(string element, string sett);
        
        public override bool Check(Player player, GameObject onMap, string sett, int x, int y)
        {
            bool min = sett[0] == '>';

            if (min)
            {
                return Value(player, onMap, GetElement(sett), sett, x, y) >= GetAmount(sett);
            }
            return Value(player, onMap, GetElement(sett), sett, x, y) <= GetAmount(sett);
        }

        protected string GetElement(string sett)
        {
            return sett.Substring(1).Split(',')[1];
        }

        protected int GetAmount(string sett)
        {
            return Int32.Parse(sett.Substring(1).Split(',')[0]);
        }
        
        public override bool Check(Player player, string sett)
        {
            bool min = sett[0] == '>';

            if (min)
            {
                return Value(player, GetElement(sett), sett) >= GetAmount(sett);
            }
            return Value(player, GetElement(sett), sett) <= GetAmount(sett);
        }

        public override bool Final(Player player, GameObject onMap, string sett, int x, int y)
        {
            return false;
        }

        public override string Desc(Player player, GameObject onMap, string sett, int x, int y)
        {
            return Desc(sett) + $" You have {Value(player, onMap, GetElement(sett), sett, x, y)}x {Name(GetElement(sett),sett)}.";
        }

        public override string Desc(string sett)
        {
            bool min = sett[0] == '>';

            return min ? $"Need at least {GetAmount(sett)}x {Name(GetElement(sett),sett)}." : $"Need maximal {GetAmount(sett)}x {Name(GetElement(sett),sett)}.";
        }
    }
}