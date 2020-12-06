using System;
using Buildings;
using Game;
using Libraries;
using Players;
using Tools;
using Towns;
using Units;
using UnityEngine;
using MapElementInfo = MapElements.MapElementInfo;

namespace reqs
{
    
    public class ReqSpellCount : BaseReqMinMax
    {
        protected override int ValueMax(Player player, MapElementInfo onMap, string element, string sett, NVector pos)
        {
            return L.b.spells.Length;
        }

        protected override int ValueMax(Player player, string element, string sett)
        {
            throw new NotImplementedException();
        }

        protected override int ValueAct(Player player, MapElementInfo onMap, string element, string sett, NVector pos)
        {
            return 10;//onMap.data.spells.known.Count;
        }

        protected override int ValueAct(Player player, string element, string sett)
        {
            throw new NotImplementedException();
        }

        protected override string Name(string element, string sett)
        {
            return S.T("spellKnown");
        }
    }
}