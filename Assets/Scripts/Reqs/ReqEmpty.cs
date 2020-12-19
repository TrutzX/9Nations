using Buildings;

using Game;
using Players;
using Tools;
using Units;
using UnityEngine;
using MapElementInfo = MapElements.MapElementInfo;

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
                
                return S.T("reqHere",Desc(player, sett),S.Building(pos).name);
            }
            if (S.Unit().Free(pos)) return Desc(player, sett);
            return S.T("reqHere",Desc(player, sett),S.Unit(pos).name);
        }

        public override string Desc(Player player, string sett)
        {
            return S.T("reqEmpty", sett);
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
            return S.T("reqNotEmpty", sett);
        }
        
    } 
}