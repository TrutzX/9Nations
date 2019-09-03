using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class Version : MonoBehaviour
    {
        #pragma warning 649 disable
        [SerializeField]
        private Text warning;
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Text>().text =
                $"{Application.productName} V{Application.version}-{Application.platform} - {Application.companyName}";
            warning.text = Data.help.beta.text;
        }
    }
}
