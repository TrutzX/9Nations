using UnityEngine;

namespace Audio
{
    public class NAudio : MonoBehaviour
    {
        public void Start()
        {
            UpdateAudio();
        }

        public static NAudio Get()
        {
            return FindObjectOfType<NAudio>();
        }
    
        public static void PlayMusic(string name, bool loop)
        {
            PlayMusic(Resources.Load<AudioClip>("Music/"+name), loop);
        }
    
        public static void PlayMusic(AudioClip clip, bool loop)
        {
            //Debug.Log("Play "+clip.name);
            AudioSource a = Get().GetComponents<AudioSource>()[0];
            a.clip = clip;
            a.loop = loop;
            a.Play();
        }
    
        public static void Play(AudioClip clip)
        {
            //Debug.Log("Play "+clip.name);
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
    
        public static void PlayBuzzer()
        {
            Play("buzzer");
        }
    
        public static void PlayCancel()
        {
            Play("cancel");
        }

        public void UpdateAudio()
        {
            GetComponents<AudioSource>()[0].volume = PlayerPrefs.GetInt("audio.music",50) / 100.0f;
            GetComponents<AudioSource>()[1].volume = PlayerPrefs.GetInt("audio.sound",75) / 100.0f;
        }
    }
}
