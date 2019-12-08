using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Loading
{
    public class LoadingScreen : MonoBehaviour, ILoadScreen
    {
        public Text mainText;
        public Text subText;
        
        private int _loadScene = 0;
        
        void Update()
        {
            // If the new scene has started loading...
            if (_loadScene == 1) {
                // ...then pulse the transparency of the loading text to let the player know that the computer is still working.
                mainText.color = new Color(mainText.color.r, mainText.color.g, mainText.color.b, Mathf.PingPong(Time.time, 1));
                subText.color = new Color(subText.color.r, subText.color.g, subText.color.b, Mathf.PingPong(Time.time, 1));
            }
        }
        
        public IEnumerator ShowMessage(string text)
        {
            _loadScene = 1;
            Debug.Log(text);
            gameObject.SetActive(true);
            mainText.text = text;
            subText.text = "";
            yield return new WaitForSeconds(0.5f);
        }

        public IEnumerator ShowSubMessage(string text)
        {
            subText.text = text;
            yield return new WaitForFixedUpdate();
        }
        
        public void FinishLoading()
        {
            _loadScene = 2;
            gameObject.SetActive(false);
        }
    }
}