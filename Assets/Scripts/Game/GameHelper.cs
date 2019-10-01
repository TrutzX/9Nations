using Buildings;
using Units;
using UnityEngine;

namespace Game
{
    public class GameHelper
    {
        public static MapElementInfo GetMapElement(GameObject game)
        {
            if (game.GetComponent<BuildingInfo>())
            {
                return game.GetComponent<BuildingInfo>();
            }

            return game.GetComponent<UnitInfo>();
        }
    }
}