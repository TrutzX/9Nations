using System;
using Buildings;
using Game;
using MapElements;
using Maps;
using Players;
using Tools;
using UnityEngine;

namespace reqs
{
    
    public class ReqTerrainCategory : BaseReqOnlyMapElement
    {
        public override bool Check(Player player, MapElementInfo onMap, string sett, NVector pos)
        {

            string[] terr = SplitHelper.Separator(sett);
            foreach (string terrain in terr)
            {
                    if (GameMgmt.Get().newMap.Terrain(pos).category == terrain)
                    {
                        return true;
                    }
            }
            
            return false;
        }

        public override bool Final(Player player, MapElementInfo onMap, string sett, NVector pos)
        {
            return false;
        }

        public override string Desc(Player player, MapElementInfo onMap, string sett, NVector pos)
        {
            return Desc(player,sett)+$" Here is {GameMgmt.Get().newMap.Terrain(pos).category}";
        }

        public override string Desc(Player player, string sett)
        {
            return $"Needs the base terrain {sett}.";
        }
    }
}