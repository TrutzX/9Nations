using System;
using Buildings;
using Game;
using Maps;
using Players;
using Tools;
using UnityEngine;

namespace reqs
{
    
    public class ReqTerrainNear : BaseReqOnlyMapElement
    {
        public override bool Check(Player player, MapElementInfo onMap, string sett, NVector pos)
        {
            
            string[] terr = sett.Split(',');
            foreach (string terrain in terr)
            {
                    if (GameMgmt.Get().newMap.Terrain(pos.DiffX(1)).category == terrain || 
                        GameMgmt.Get().newMap.Terrain(pos.DiffX(-1)).category == terrain || 
                        GameMgmt.Get().newMap.Terrain(pos.DiffY(1)).category == terrain || 
                        GameMgmt.Get().newMap.Terrain(pos.DiffY(-1)).category == terrain)
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
            return Desc(player, sett)+$" Here is {GameMgmt.Get().newMap.Terrain(pos).name}";
        }

        public override string Desc(Player player, string sett)
        {
                return $"Needs near the terrain {sett}.";
        }
    }
}