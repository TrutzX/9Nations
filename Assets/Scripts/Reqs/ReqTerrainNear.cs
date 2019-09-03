using System;
using Players;
using UnityEngine;

namespace reqs
{
    
    public class ReqTerrainNear : BaseReq
    {
        public override bool Check(Player player, GameObject onMap, string sett, int x, int y)
        {
            
            string[] terr = sett.Split(',');
            foreach (string terrain in terr)
            {
                    if (MapMgmt.Get().GetTerrain(x, y).id == terrain || MapMgmt.Get().GetTerrain(x+1, y).id == terrain || MapMgmt.Get().GetTerrain(x, y-1).id == terrain || MapMgmt.Get().GetTerrain(x, y+1).id == terrain)
                    {
                        return true;
                    }
            }
            
            return false;
        }

        public override bool Check(Player player, string sett)
        {
            Debug.LogWarning("Not implemented");
            return false;
        }

        public override bool Final(Player player, GameObject onMap, string sett, int x, int y)
        {
            return false;
        }

        public override string Desc(Player player, GameObject onMap, string sett, int x, int y)
        {
            return Desc(sett)+$" Here is {MapMgmt.Get().GetTerrain(x,y).name}";
        }

        public override string Desc(string sett)
        {
                return $"Needs near the terrain {sett}.";
        }
    }
}