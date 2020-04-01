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
    

        public static void HoverEnter(GameObject button, Action enter, Action exit)
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

        public static Button CreateButton(GameObject button, string title, Transform parent, Action action)
        {
            GameObject act = Instantiate(button, parent);
            act.transform.Find("Label").GetComponent<Text>().text = title;
            act.GetComponent<Button>().onClick.AddListener(() => { 
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    ExceptionHelper.ShowException(e);
                }
            });
            return act.GetComponent<Button>();
        }
    
        public static Button CreateButton(string title, Transform parent, Action action)
        {
            return CreateButton(UIElements.Get().button, title,parent,action);
        }
    
        [Obsolete("Please use UpdateButtonText with Button.")]
        public static void UpdateButtonText(GameObject button, string title)
        {
            button.transform.GetChild(0).GetComponent<Text>().text = title;
        }
    
        public static void UpdateButtonText(Button button, string title)
        {
            button.transform.GetChild(0).GetComponent<Text>().text = title;
        }
    
        [Obsolete("Please use UpdateButtonImage with Button.")]
        public static void UpdateButtonImage(GameObject button, Sprite sprite)
        {
            button.transform.GetChild(1).GetComponent<Image>().sprite = sprite;
        }
        public static void UpdateButtonImage(Button button, Sprite sprite)
        {
            button.transform.GetChild(1).GetComponent<Image>().sprite = sprite;
        }

        public static Button CreateImageTextButton(string title, Sprite icon, Transform parent, Action action, string sound="click")
        {
            GameObject act = Instantiate(UIElements.Get().imageTextButton, parent);
            act.name = title;
            act.transform.GetChild(0).GetComponent<Text>().text = title;
            act.transform.GetChild(1).GetComponent<Image>().sprite = icon;
            act.GetComponent<Button>().onClick.AddListener(() => { action(); });
            UIElements.AddButtonSound(act, sound);
            return act.GetComponent<Button>();
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
            wpb.panel.AddLabel(desc);
            wpb.AddClose();
            wpb.Finish();
        }
    }
}
