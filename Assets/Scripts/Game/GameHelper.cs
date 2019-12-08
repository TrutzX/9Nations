using Buildings;
using Units;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            return !(y < 0 || x < 0 || y >= GameMgmt.Get().data.map.height || x >= GameMgmt.Get().data.map.width);
        }

        public static bool Valide(Vector3Int pos)
        {
            return Valide(pos.x, pos.y);
        }

        public static bool IsGame()
        {
            return SceneManager.GetActiveScene().buildIndex == 2;
        }
    }
}