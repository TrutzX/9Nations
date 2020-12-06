using DG.Tweening;
using Game;
using Tools;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class WindowPanelBuilder : MonoBehaviour
    {
        public PanelBuilder panel;
        public UnityAction onClose;

        public static WindowPanelBuilder Create(string title)
        {
            title = TextHelper.Cap(title);
            GameObject act = Instantiate(UIElements.Get().panelWindow, GameObject.Find("WindowsMgmt").transform);
            act.name = title;
            act.transform.GetComponentInChildren<Text>().text = title;
        
            act.GetComponent<WindowPanelBuilder>().panel = PanelBuilder.Create(act.transform.GetChild(0).GetChild(2).transform);
            return act.GetComponent<WindowPanelBuilder>();
        }

        public void Finish(int w)
        {
            panel.CalcSize();
            transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(w,panel.panel.GetComponent<RectTransform>().sizeDelta.y+48);
            gameObject.SetActive(true);
            gameObject.GetComponent<Image>().color = new Color(0,0,0,0);
            gameObject.GetComponent<Image>().DOFade(0.33f, 10);
        }
    
        public void Finish()
        {
            Finish(300);
        }

        public void AddClose()
        {
            panel.AddButton(S.T("close"),Close);
        }

        public void Close()
        {
            Destroy(gameObject);
            onClose?.Invoke();
        }
    }
}
