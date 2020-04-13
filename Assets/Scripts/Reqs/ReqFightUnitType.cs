using Buildings;
using Players;
using Units;
using UnityEngine;

namespace reqs
{
    public class ReqFightUnitType : BaseReqFight
    {
        protected override string Name()
        {
            return "unit type";
        }

        protected override bool CheckMapElement(MapElementInfo mapElement, string sett)
        {
            return !mapElement.IsBuilding() && ((UnitInfo) mapElement).dataUnit.type == sett;
        }
    }
}