using System;
using System.Collections;
using Classes;
using GameButtons;
using Libraries;
using Loading;
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
            yield return L.Init(GameObject.Find("UICanvas").GetComponentInChildren<LoadingScreen>(true));

            yield return L.b.Load.ShowMessage("Prepare main menu");
            version.text =
                $"{Application.productName} V{Application.version}-{Application.platform} - {Application.companyName}";
            warning.text = Data.help.beta.text;
        
            L.b.gameButtons.BuildMenu(null, "title", null, true, panel.transform);
        
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
            
            StartCoroutine(CheckUpdate());
            
        }

        IEnumerator CheckUpdate()
        {
            if (!Data.features.update.Bool()) yield break;
            
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
