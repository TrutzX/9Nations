using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIHelper : ScriptableObject
{
    

    public static void HoverEnter(Text text, string title, GameObject button, Action exit)
    {
        //hoverenter
        EventTrigger.Entry eventtype = new EventTrigger.Entry();
        eventtype.eventID = EventTriggerType.PointerEnter;
        eventtype.callback = new EventTrigger.TriggerEvent();
        eventtype.callback.AddListener((eventData) =>
        {
            text.text = title;
        });
        button.GetComponent<EventTrigger>().triggers.Add(eventtype);
        
        //hoverexit
        EventTrigger.Entry eventtype2 = new EventTrigger.Entry();
        eventtype2.eventID = EventTriggerType.PointerExit;
        eventtype2.callback.AddListener((eventData) => exit());
        button.GetComponent<EventTrigger>().triggers.Add(eventtype2);
    }

    public static GameObject CreateButton(GameObject button, string title, Transform parent, Action action)
    {
        GameObject act = Instantiate(button, parent);
        act.transform.Find("Label").GetComponent<Text>().text = title;
        act.GetComponent<Button>().onClick.AddListener(() => { action(); });
        return act;
    }
    
    public static GameObject CreateButton(string title, Transform parent, Action action)
    {
        return CreateButton(UIElements.Get().button, title,parent,action);
    }

    public static void UpdateButtonText(GameObject button, string title)
    {
        button.transform.GetChild(0).GetComponent<Text>().text = title;
    }

    public static GameObject CreateImageTextButton(string title, Sprite icon, Transform parent, Action action)
    {
        GameObject act = Instantiate(UIElements.Get().imageTextButton, parent);
        act.name = title;
        act.transform.GetChild(0).GetComponent<Text>().text = title;
        act.transform.GetChild(1).GetComponent<Image>().sprite = icon;
        act.GetComponent<Button>().onClick.AddListener(() => { action(); });

        return act;
    }
}
