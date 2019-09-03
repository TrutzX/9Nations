using Buildings;
using UnityEngine;

namespace Game
{
    public class GameHelper
    {
        public static IMapElement GetMapElement(GameObject game)
        {
            if (game.GetComponent<BuildingInfo>())
            {
                return game.GetComponent<BuildingInfo>();
            }

            return game.GetComponent<UnitInfo>();
        }
    }
}