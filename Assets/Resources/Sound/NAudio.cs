using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NAudio : MonoBehaviour
{
    public AudioClip titleMusic;
    

    public static NAudio Get()
    {
        return FindObjectOfType<NAudio>();
    }
    
    public static void PlayTitleMusic()
    {
        Get().PlayMusic(Get().titleMusic);
    }
    
    void PlayMusic(AudioClip clip)
    {
        AudioSource a = GetComponents<AudioSource>()[0];
        a.clip = titleMusic;
        a.Play();
    }
    
    public static void Play(AudioClip clip)
    {
        AudioSource a = Get().GetComponents<AudioSource>()[1];
        a.clip = clip;
        a.Play();
    }
    
    public static void Play(string name)
    {
        Play(Resources.Load<AudioClip>("Sound/"+name));
    }
    
    public static void PlayClick()
    {
        Play("click");
    }
}
