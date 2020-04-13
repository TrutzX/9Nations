using System;
using Buildings;
using Game;
using Libraries;
using Libraries.Terrains;
using Players;
using Tools;
using Towns;
using Units;
using UnityEngine;
using MapElementInfo = Buildings.MapElementInfo;

namespace reqs
{
    
    public class ReqTerrainRes : BaseReqMinMax
    {
        protected override int ValueMax(Player player, MapElementInfo onMap, string element, string sett, NVector pos)
        {
            DataTerrain dataTerrain = S.Map().Terrain(pos);
            return dataTerrain.ResChance(element);
        }

        protected override int ValueMax(Player player, string element, string sett)
        {
            throw new NotImplementedException();
        }

        protected override int ValueAct(Player player, MapElementInfo onMap, string element, string sett, NVector pos)
        {
            return GameMgmt.Get().data.map.levels[pos.level].ResGen(pos.x, pos.y, element);
        }

        protected override int ValueAct(Player player, string element, string sett)
        {
            throw new NotImplementedException();
        }

        protected override string Name(string element, string sett)
        {
            return element;
        }
    }
}