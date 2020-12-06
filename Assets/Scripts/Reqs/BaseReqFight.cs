using System;
using System.Linq;
using Buildings;
using Game;
using MapElements;
using Players;
using Tools;
using Towns;
using UI;
using UnityEngine;

namespace reqs
{
    
    public abstract class BaseReqFight : BaseReqOnlyMapElement
    {
        public override bool Check(Player player, MapElementInfo onMap, string sett, NVector pos)
        {
            var s = SplitHelper.Split(sett);
            return CheckMapElement(onMap, s.key) && CheckMapElement(S.MapElement(pos), s.value);
        }

        public override bool Final(Player player, MapElementInfo onMap, string sett, NVector pos)
        {
            return false;
        }

        public override string Desc(Player player, string sett)
        {
            var s = SplitHelper.Split(sett);
            return $"You needs the {Name()} {s.key} and your enemy {s.value}.";
        }

        public override string Desc(Player player, MapElementInfo onMap, string sett, NVector pos)
        {
            return Desc(player, sett);
        }

        protected abstract string Name();

        protected abstract bool CheckMapElement(MapElementInfo mapElement, string sett);
    }
}