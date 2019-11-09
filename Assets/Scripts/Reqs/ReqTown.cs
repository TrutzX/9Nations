using System;
using Buildings;
using Players;
using UnityEngine;

namespace reqs
{
    public class ReqTown : ReqMinMax
    {
        protected override int ValueMax(Player player, MapElementInfo onMap, string element, string sett, int x, int y)
        {
            return ValueMax(player, element, sett);
        }

        protected override int ValueMax(Player player, string element, string sett)
        {
            //TODO
            return 10;
        }

        protected override int ValueAct(Player player, MapElementInfo onMap, string element, string sett, int x, int y)
        {
            return ValueAct(player, element, sett);
        }

        protected override int ValueAct(Player player, string element, string sett)
        {
            return TownMgmt.Get().GetByPlayer(player.id).Count;
        }

        protected override string Name(string element, string sett)
        {
            return "Town";
        }
    }
    
    public class ReqOldTownMin : BaseReqOld
    {
        public override bool Check(Player player, GameObject onMap, string sett, int x, int y)
        {
            return Check(player, sett);
        }

        public override bool Check(Player player, string sett)
        {
            return TownMgmt.Get().GetByPlayer(player.id).Count >= Int32.Parse(sett);
        }

        public override bool Final(Player player, GameObject onMap, string sett, int x, int y)
        {
            return false;
        }

        public override string Desc(Player player, GameObject onMap, string sett, int x, int y)
        {
            return Desc(sett)+$" You have only {TownMgmt.Get().GetByPlayer(player.id).Count} town.";
        }

        public override string Desc(string sett)
        {
            return $"Need at least {sett} town.";
        }
    }
    
    
    public class ReqOldTownMax : BaseReqOld
    {
        public override bool Check(Player player, GameObject onMap, string sett, int x, int y)
        {
            return Check(player, sett);
        }

        public override bool Check(Player player, string sett)
        {
            return TownMgmt.Get().GetByPlayer(player.id).Count <= Int32.Parse(sett);
        }

        public override bool Final(Player player, GameObject onMap, string sett, int x, int y)
        {
            return false;
        }

        public override string Desc(Player player, GameObject onMap, string sett, int x, int y)
        {
            return Desc(sett)+$" You have already {TownMgmt.Get().GetByPlayer(player.id).Count} town.";
        }

        public override string Desc(string sett)
        {
            return $"Need maximal {sett} town.";
        }
    }
}