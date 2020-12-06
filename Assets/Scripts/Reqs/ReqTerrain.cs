using System;
using System.Linq;
using Buildings;
using Game;
using Libraries;
using MapElements;
using Maps;
using Players;
using Tools;
using UnityEngine;

namespace reqs
{
    
    public class ReqTerrain : BaseReqOnlyMapElement
    {
        public override bool Check(Player player, MapElementInfo onMap, string sett, NVector pos)
        {
            string[] terr = SplitHelper.Separator(sett);
            return terr.Any(terrain => GameMgmt.Get().newMap.Terrain(pos).id == terrain);
        }

        public override bool Final(Player player, MapElementInfo onMap, string sett, NVector pos)
        {
            return false;
        }

        public override string Desc(Player player, MapElementInfo onMap, string sett, NVector pos)
        {
            return Desc(player,sett)+$" Here is {GameMgmt.Get().newMap.Terrain(pos).Name()}";
        }

        public override string Desc(Player player, string sett)
        {
            var l = SplitHelper.Separator(sett);
            return S.T(LSys.tem.translations.GetPlural("reqTerrain",l.Length), L.b.terrains.NameList(l));
        }
    }
}