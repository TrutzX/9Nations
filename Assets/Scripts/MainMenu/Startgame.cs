using System;
using System.Collections;
using Audio;
using Classes;
using Game;
using GameButtons;
using Libraries;
using Libraries.Helps;
using Loading;
using ModIO;
using Options;
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
            
            Debug.Log($"Start main menu V{Application.version}-{Application.platform}");
            
            version.text =
                $"{Application.productName} V{Application.version}-{Application.platform} - {Application.companyName}";
            warning.text = LSys.tem.helps["beta"].Desc().Replace(";;",Environment.NewLine);
            
            OptionHelper.RunStartOptions();
            
            L.b.gameButtons.BuildMenu(null, "title", null, true, panel.transform);
        
            //show it
            LSys.tem.Load.FinishLoading();
            
        
            // first start?
            if (!PlayerPrefs.HasKey("lastVersion"))
            {
                //todo dynamic
                if (Application.platform == RuntimePlatform.Android)
                {
                    new AndroidStartUp().Run();
                }
                else
                {
                    new ComputerStartUp().Run();
                }
            }

            //show message?
            if (PlayerPrefs.GetString("lastVersion", "x") != Application.version)
            {
                PlayerPrefs.SetString("lastVersion",Application.version);
                PlayerPrefs.Save();
                
                WindowPanelBuilder w = WindowPanelBuilder.Create("Good to know");
                
                foreach (var help in LSys.tem.helps.GetAllByCategory("start"))
                {
                    if (!help.req.Check(null)) continue;
                    
                    w.panel.AddHeaderLabel(help.Name());
                    w.panel.RichText(help.Desc());
                }
                w.AddClose();
                w.Finish();
            }
            
            StartCoroutine(CheckUpdate());
            
        }

        IEnumerator CheckUpdate()
        {
            if (!LSys.tem.options["updateCheck"].Bool()) yield break;
            
            int time = PlayerPrefs.GetInt("update.time", 0);
            int hours = (int) (DateTime.Now - new DateTime(2020, 01, 01)).TotalHours;

            if (hours <= time) yield break;
            PlayerPrefs.SetInt("update.time", hours+24);
            
            UnityWebRequest www = UnityWebRequest.Get($"http://9nations.de/files/updateN.php?ver={Application.version}&typ={Application.platform}");
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
