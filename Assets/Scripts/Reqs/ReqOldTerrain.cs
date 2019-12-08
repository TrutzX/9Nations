using System;
using Maps;
using Players;
using UnityEngine;

namespace reqs
{
    
    public class ReqOldTerrain : BaseReqOld
    {
        public override bool Check(Player player, GameObject onMap, string sett, int x, int y)
        {
            
            string[] terr = sett.Split(',');
            foreach (string terrain in terr)
            {
                    if (GameMapMgmt.Get().GetTerrain(x, y).Category == terrain)
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
            return Desc(sett)+$" Here is {GameMapMgmt.Get().GetTerrain(x,y).Name}";
        }

        public override string Desc(string sett)
        {
            return $"Needs the terrain {sett}.";
        }
    }
}