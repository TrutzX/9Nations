using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WindowUIHelper : ScriptableObject
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public static void SetWindowTitle(GameObject window, string title)
        {
            window.transform.Find("Header").GetComponent<Text>().text = title;
        }
    
        //public static void 
    }
}
