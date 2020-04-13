using System;
using System.Collections;
using Audio;
using Classes;
using GameButtons;
using Libraries;
using Libraries.Helps;
using Loading;
using ModIO;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.Networking;
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
            LClass.Init();
            StartCoroutine(Start2());
        }
    
        IEnumerator Start2()
        {
            NAudio.PlayMusic("title",true);
            //load database
            yield return LSys.Init(GameObject.Find("UICanvas").GetComponentInChildren<LoadingScreen>(true));
            yield return L.Init();

            yield return LSys.tem.Load.ShowMessage("Prepare main menu");
            version.text =
                $"{Application.productName} V{Application.version}-{Application.platform} - {Application.companyName}";
            warning.text = LSys.tem.helps["beta"].Desc;
        
            L.b.gameButtons.BuildMenu(null, "title", null, true, panel.transform);
        
            LSys.tem.Load.FinishLoading();
        
            //show message?
            if (PlayerPrefs.GetString("lastVersion", "x") != Application.version)
            {
                PlayerPrefs.SetString("lastVersion",Application.version);
                PlayerPrefs.Save();

                NHelp h = LSys.tem.helps["new"];
            
                WindowPanelBuilder w = WindowPanelBuilder.Create(h.name);
                w.panel.RichText(h.Desc);
                w.AddClose();
                w.Finish();
            }
            
            StartCoroutine(CheckUpdate());
            
        }

        IEnumerator CheckUpdate()
        {
            if (!LSys.tem.options["update"].Bool()) yield break;
            
            int time = PlayerPrefs.GetInt("update.time", 0);
            int hours = (int) (DateTime.Now - new DateTime(2020, 01, 01)).TotalHours;

            if (hours <= time) yield break;
            PlayerPrefs.SetInt("update.time", hours+24);
            
            UnityWebRequest www = UnityWebRequest.Get($"http://9nations.de/files/updateN.php?ver={Application.version}&typ={Application.platform}&a=c");
            yield return www.SendWebRequest();

            if(www.isNetworkError || www.isHttpError) {
                Debug.LogError(www.error);
                yield break;
            }
            PlayerPrefs.SetString("update.txt", www.downloadHandler.text);
            PlayerPrefs.Save();
        }
    }
}
