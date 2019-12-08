using System.Collections;
using Libraries;
using Loading;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class StartGame : MonoBehaviour
    {
        public Text warning;
        public Text version;
        public GameObject panel;

        public void Start()
        {
            StartCoroutine(Start2());
        }
    
        IEnumerator Start2()
        {
            NAudio.PlayMusic("title",true);
            //load database
            yield return L.Init(GameObject.Find("UICanvas").GetComponentInChildren<LoadingScreen>(true));

            yield return L.b.Load.ShowMessage("Prepare main menu");
            version.text =
                $"{Application.productName} V{Application.version}-{Application.platform} - {Application.companyName}";
            warning.text = Data.help.beta.text;
        
            GameButtonHelper.BuildMenu(null, "title", null, true, panel.transform);
        
            L.b.Load.FinishLoading();
        
            //show message?
            if (PlayerPrefs.GetString("lastVersion", "x") != Application.version)
            {
                PlayerPrefs.SetString("lastVersion",Application.version);
                PlayerPrefs.Save();

                DataTypes.Help h = Data.help._new;
            
                WindowPanelBuilder w = WindowPanelBuilder.Create(h.name);
                w.panel.AddLabel(h.text);
                w.AddClose();
                w.Finish();
            }
        }
    }
}
