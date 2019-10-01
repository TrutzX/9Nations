using System;
using System.Collections;
using System.Collections.Generic;
using Players;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Startgame : MonoBehaviour
{
    public Text warning;
    public Text version;
    public GameObject panel;

    public void Start()
    {
        version.text =
            $"{Application.productName} V{Application.version}-{Application.platform} - {Application.companyName}";
        warning.text = Data.help.beta.text;
        NAudio.PlayMusic("title",true);
        
        GameButtonHelper.buildMenu(null, "title", null, true, panel.transform);
    }

    public void LoadGame()
    {
        GameObject.Find("Sounds").GetComponents<AudioSource>()[0].Play();
        Debug.Log(GameObject.Find("Sounds").GetComponents<AudioSource>().Length);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //GetComponents<AudioSource>()
    }
}
