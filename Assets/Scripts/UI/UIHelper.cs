using System;
using Buildings;
using Tools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class UIHelper : ScriptableObject
    {
    

        public static void HoverEnter(MonoBehaviour button, Action enter, Action exit)
        {
            //hoverenter
            EventTrigger.Entry eventtype = new EventTrigger.Entry();
            eventtype.eventID = EventTriggerType.PointerEnter;
            eventtype.callback = new EventTrigger.TriggerEvent();
            eventtype.callback.AddListener((eventData) => enter());
            button.GetComponent<EventTrigger>().triggers.Add(eventtype);
        
            //hoverexit
            EventTrigger.Entry eventtype2 = new EventTrigger.Entry();
            eventtype2.eventID = EventTriggerType.PointerExit;
            eventtype2.callback.AddListener((eventData) => exit());
            button.GetComponent<EventTrigger>().triggers.Add(eventtype2);
        }
    
        public static Button CreateButton(string title, Transform parent, Action action, string sound="click")
        {
            Button act = Instantiate(UIElements.Get().button, parent).GetComponent<Button>();
            UpdateButtonText(act, title);
            //act.transform.Find("Label").GetComponent<Text>().text = TextHelper.Cap(title);
            act.onClick.AddListener(() => { 
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    ExceptionHelper.ShowException(e);
                }
            });
            UIElements.AddButtonSound(act, sound);
            return act;
        }
    
        public static void UpdateButtonText(Button button, string title)
        {
            button.name = title;
            button.transform.GetChild(0).GetComponent<Text>().text = TextHelper.Cap(title);
        }
        
        public static void UpdateButtonImage(Button button, Sprite sprite)
        {
            button.transform.GetChild(1).GetComponent<Image>().sprite = sprite;
        }
        
        public static void UpdateButtonImageColor(Button button, Color color)
        {
            button.transform.GetChild(1).GetComponent<Image>().color = color;
        }

        public static Button CreateImageTextButton(string title, Sprite icon, Transform parent, Action action, string sound="click")
        {
            Button act = Instantiate(UIElements.Get().imageTextButton, parent).GetComponent<Button>();
            UpdateButtonText(act, title);
            //act.name = title;
            //act.transform.GetChild(0).GetComponent<Text>().text = TextHelper.Cap(title);
            act.transform.GetChild(1).GetComponent<Image>().sprite = icon;
            act.onClick.AddListener(() => { 
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    ExceptionHelper.ShowException(e);
                }
            });
            UIElements.AddButtonSound(act, sound);
            return act;
        }

        public static void ClearChild(GameObject go)
        {
            Transform[] old = go.transform.GetComponentsInChildren<Transform>();
            for (int i = 1; i < old.Length; i++)
            {
                Destroy(old[i].gameObject);
            }
        }

        public static void ShowOk(string title, string desc)
        {
            WindowPanelBuilder wpb = WindowPanelBuilder.Create(title);
            wpb.panel.RichText(desc);
            wpb.AddClose();
            wpb.Finish();
        }
    }
}
