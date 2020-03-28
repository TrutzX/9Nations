using Buildings;
using Tools;
using Units;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameHelper
    {
        public static bool Valid(int x, int y)
        {
            return !(y < 0 || x < 0 || y >= GameMgmt.Get().data.map.height || x >= GameMgmt.Get().data.map.width);
        }

        public static bool Valid(NVector pos)
        {
            return Valid(pos.x, pos.y);
        }

        public static bool IsGame()
        {
            return SceneManager.GetActiveScene().buildIndex == 2;
        }
    }
}