using DataTypes;
using Players;
using Units;
using UnityEngine;

namespace reqs
{
    
    public class ReqEmpty : BaseReq
    {
        public override bool Check(Player player, GameObject onMap, string sett, int x, int y)
        {
            if (sett == "building")
            {
                return BuildingMgmt.At(x, y) == null;
            }
            return UnitMgmt.At(x, y) == null;
        }

        public override bool Check(Player player, string sett)
        {
            Debug.LogWarning("Not implemented");
            return false;
        }

        public override bool Final(Player player, GameObject onMap, string sett, int x, int y)
        {
            return true;
        }

        public override string Desc(Player player, GameObject onMap, string sett, int x, int y)
        {
            if (sett == "building")
            {
                return Desc(sett)+$" Here is {BuildingMgmt.At(x, y).name}";
            }
            return Desc(sett)+$" Here is {UnitMgmt.At(x, y).name}";
        }

        public override string Desc(string sett)
        {
            return $"Needs an empty field with no {sett}";
        }
    }
}