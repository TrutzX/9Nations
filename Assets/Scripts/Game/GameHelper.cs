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

        public static bool Valide(int x, int y)
        {
            return !(y < 0 || x < 0 || y >= GameMgmt.Get().data.mapheight || x >= GameMgmt.Get().data.mapwidth);
        }
    }
}