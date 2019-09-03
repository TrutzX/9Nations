using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIElements : MonoBehaviour
{
    public GameObject button;
    public GameObject imageTextButton;
    public GameObject label;
    public GameObject panelLabel;
    public GameObject panelLabelDesc;
    public GameObject richlabel;
    public GameObject input;
    public GameObject window;
    public GameObject splitWindow;
    public GameObject imageButton;
    public GameObject panelBuilder;
    public GameObject imageLabel;
    public GameObject headerLabel;
    public GameObject panelWindow;
    public GameObject checkBox;
    public Dropdown dropdown;
    

    public static UIElements Get()
    {
        return GameObject.Find("UI").GetComponent<UIElements>();
    }
    
    public static GameObject CreateImageButton(string icon, Transform parent)
    {
        
        GameObject act = Instantiate(Get().imageButton, parent);
        act.name = icon;
        act.transform.Find("Image").GetComponent<Image>().sprite = SpriteHelper.Load("Icons/"+icon);
        return act;
    }
    
    public static GameObject CreateImageLabel(Transform parent, string title, Sprite icon)
    {
        GameObject act = Instantiate(Get().imageLabel, parent);
        act.name = title;
        act.transform.GetChild(0).GetComponent<Image>().sprite = icon;
        act.transform.GetChild(1).GetComponent<Text>().text = title;
        return act;
    }
    
    public static GameObject CreateImageLabel(Transform parent, string title, string icon)
    {
        return CreateImageLabel(parent, title, SpriteHelper.LoadIcon(icon));
    }
    
    public static GameObject CreateHeaderLabel(Transform parent, string title)
    {
        GameObject act = Instantiate(Get().headerLabel, parent);
        act.name = title;
        act.transform.GetChild(1).GetComponent<Text>().text = title;
        return act;
    }

    public static GameObject CreateCheckBox(Transform parent, string title, UnityAction<bool> action)
    {
        GameObject act = Instantiate(Get().checkBox, parent);
        act.name = title;
        act.transform.GetChild(1).GetComponent<Text>().text = title;
        act.GetComponent<Toggle>().onValueChanged.AddListener(action);
        return act;
    }
}