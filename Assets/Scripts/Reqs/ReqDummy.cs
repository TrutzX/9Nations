using Buildings;

using Game;
using Players;
using Tools;
using Units;
using UnityEngine;
using MapElementInfo = MapElements.MapElementInfo;

namespace reqs
{
    
    public class ReqDummy : BaseReq
    {
        public override bool Check(Player player, MapElementInfo onMap, string sett, NVector pos)
        {
            return true;
        }

        public override bool Check(Player player, string sett)
        {
            return true;
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
            return Desc(player, sett);
        }

        public override string Desc(Player player, string sett)
        {
            return $"Dummy element, because {sett} is missing";
        }
        
    } 
}