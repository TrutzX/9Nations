using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startgame : MonoBehaviour
{
    public void LoadGame()
    {
        GameObject.Find("Sounds").GetComponents<AudioSource>()[0].Play();
        Debug.Log(GameObject.Find("Sounds").GetComponents<AudioSource>().Length);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //GetComponents<AudioSource>()
    }
}
