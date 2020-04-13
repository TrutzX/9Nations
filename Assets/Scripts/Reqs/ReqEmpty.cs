using Buildings;

using Game;
using Players;
using Tools;
using Units;
using UnityEngine;
using MapElementInfo = Buildings.MapElementInfo;

namespace reqs
{
    
    public class ReqEmpty : BaseReq
    {
        public override bool Check(Player player, MapElementInfo onMap, string sett, NVector pos)
        {
            if (sett == "building")
            {
                return S.Building().Free(pos);
            }

            return S.Unit().Free(pos);
        }

        public override bool Check(Player player, string sett)
        {
            Debug.LogWarning("Not implemented");
            return false;
        }

        public override bool Final(Player player, MapElementInfo onMap, string sett, NVector pos)
        {
            return false;
        }

        public override bool Final(Player player, string sett)
        {
            return false;
        }

        public override string Desc(Player player, MapElementInfo onMap, string sett, NVector pos)
        {
            if (sett == "building")
            {
                if (S.Building().Free(pos)) return Desc(player, sett);
                
                return Desc(player, sett)+$" Here is {S.Building().At(pos).name}";
            }
            if (S.Unit().Free(pos)) return Desc(player, sett);
            return Desc(player, sett)+$" Here is {S.Unit().At(pos).name}";
        }

        public override string Desc(Player player, string sett)
        {
            return $"Needs an empty field with no {sett}";
        }
    }

    public class ReqNotEmpty : ReqEmpty
    {
        public override bool Check(Player player, MapElementInfo onMap, string sett, NVector pos)
        {
            return !base.Check(player, onMap, sett, pos);
        }

        public override string Desc(Player player, string sett)
        {
            return $"Needs a not empty field with {sett}";
        }
        
    } 
}