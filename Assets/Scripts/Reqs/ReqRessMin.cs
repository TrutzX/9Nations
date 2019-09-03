using System;
using Players;
using Towns;
using UnityEngine;

namespace reqs
{
    
    public class ReqRessMin : BaseReq
    {
        public override bool Check(Player player, GameObject onMap, string sett, int x, int y)
        {
            string ress = sett.Split(',')[0];
            int amount = Int32.Parse(sett.Split(',')[1]);
            //is a unit?
            if (onMap.GetComponent<UnitInfo>() != null)
            {
                //has a town?
                Town t = TownMgmt.Get().NearstTown(player, x, y, false);

                return t != null && t.GetRess(ress) >= amount;
            }
            
            return (onMap.GetComponent<BuildingInfo>().GetTown()).GetRess(ress) >= amount;
        }

        public override bool Check(Player player, string sett)
        {
            string ress = sett.Split(',')[0];
            int amount = Int32.Parse(sett.Split(',')[1]);
            
            return player.GetRessTotal(ress) >= amount;
        }

        public override bool Final(Player player, GameObject onMap, string sett, int x, int y)
        {
            return false;
        }

        public override string Desc(Player player, GameObject onMap, string sett, int x, int y)
        {
            string ress = sett.Split(',')[0];
            
            //is a unit?
            if (onMap.GetComponent<UnitInfo>() != null)
            {
                //has a town?
                Town t = TownMgmt.Get().NearstTown(player, x, y, false);

                if (t != null)
                {
                    return Desc(sett)+$" You have {t.GetRess(ress)}x.";
                }

                return Desc(sett);
            }
            
            return Desc(sett)+$" You have {(onMap.GetComponent<BuildingInfo>().GetTown()).GetRess(ress)}x.";
        }

        public override string Desc(string sett)
        {
            string ress = sett.Split(',')[0];
            int amount = Int32.Parse(sett.Split(',')[1]);
            
            return $"Need at least {amount}x {Data.ress[ress].name}.";
        }
    }
}