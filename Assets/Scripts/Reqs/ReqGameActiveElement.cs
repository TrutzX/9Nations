using System;
using Game;
using Players;
using UI;
using UnityEngine;

namespace reqs
{
    public class ReqGameActiveElement : BaseReqOnlyPlayer
    {

        public override bool Check(Player player, string sett)
        {
            if (sett == "true")
                return S.InputAction().active != null;
            if (sett == "unit")
                return S.InputAction().active != null && !S.InputAction().active.IsBuilding();
            if (sett == "building")
                return S.InputAction().active != null && S.InputAction().active.IsBuilding();
            throw new MissingMemberException(sett + " is not known.");
        }

        public override bool Final(Player player, string sett)
        {
            return true;
        }

        public override string Desc(Player player, string sett)
        {
            if (sett == "true")
                return "Need a selected unit or building.";
            if (sett == "unit")
                return "Need a selected unit.";
            if (sett == "building")
                return "Need a selected building.";
            throw new MissingMemberException(sett + " is not known.");
        }
    }
}