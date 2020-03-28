using Buildings;
using Game;
using Libraries;
using Libraries.Buildings;
using Players;
using UnityEngine;

namespace reqs
{
    
    public class ReqUpgradeCan : BaseReqOnlyPlayer
    {
        public override bool Check(Player player, string sett)
        {
            //is a building
            if (L.b.buildings.ContainsKey(sett))
            {
                return L.b.buildings[sett].req.Check(player, true);
            }
            return L.b.units[sett].req.Check(player, true);
        }

        public override bool Final(Player player, string sett)
        {
            return true;
        }

        public override string Desc(Player player, string sett)
        {
            BaseDataBuildingUnit e = L.b.buildings.ContainsKey(sett) ? (BaseDataBuildingUnit) L.b.buildings[sett] : L.b.units[sett];
            return $"Need to known {e.name}. Actual:{e.req.Desc(player)}";
        }
    }
}