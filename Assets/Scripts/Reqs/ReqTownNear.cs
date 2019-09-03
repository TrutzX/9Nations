using System;
using Players;
using UnityEngine;

namespace reqs
{
    
    public class ReqTownNear : BaseReq
    {
        public override bool Check(Player player, GameObject onMap, string sett, int x, int y)
        {
            return TownMgmt.Get().NearstTown(player, x,y,true)!=null;
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
            return Desc(sett);
        }

        public override string Desc(string sett)
        {
                return $"Needs on the next field a town building.";
        }
    }
}