using Buildings;
using MapElements;
using MapElements.Units;
using Players;
using Units;
using UnityEngine;

namespace reqs
{
    public class ReqFightMovementType : BaseReqFight
    {
        protected override string Name()
        {
            return "movement type";
        }

        protected override bool CheckMapElement(MapElementInfo mapElement, string sett)
        {
            return !mapElement.IsBuilding() && ((UnitInfo) mapElement).dataUnit.movement == sett;
        }
    }
}