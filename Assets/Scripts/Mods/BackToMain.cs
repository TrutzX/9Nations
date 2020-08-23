using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mods
{
    public class BackToMain : MonoBehaviour
    {
        // Start is called before the first frame update
        public void Back()
        {
            SceneManager.LoadScene(0);
        }
        // Start is called before the first frame update
        public static void Back2()
        {
            SceneManager.LoadScene(0);
        }
    }
}
