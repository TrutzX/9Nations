using System;
using Audio;
using Tools;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class UIElements : MonoBehaviour
    {
        public GameObject button;
        public GameObject imageTextButton;
        public GameObject label;
        public GameObject panelLabel;
        public GameObject panelLabelDesc;
        public GameObject richlabel;
        public GameObject input;
        public InputField inputRandom;
        public GameObject window;
        public GameObject splitWindow;
        public GameObject imageButton;
        public GameObject panelBuilder;
        public GameObject imageLabel;
        public GameObject headerLabel;
        public GameObject panelWindow;
        public GameObject tabWindow;
        public GameObject checkBox;
        public GameObject panelSplit;
        public Dropdown dropdown;
        public Slider slider;
        public Image panelImage;
    

        public static UIElements Get()
        {
            return GameObject.Find("UI").GetComponent<UIElements>();
        }
    
        public static GameObject CreateImageButton(string icon, Transform parent, Action action, string sound="click")
        {
            GameObject act = CreateImageButton(SpriteHelper.Load(icon), parent, action, sound);
            act.name = icon;
            return act;
        }
    
        public static GameObject CreateImageButton(Sprite icon, Transform parent, Action action, string sound="click")
        {
            GameObject act = Instantiate(Get().imageButton, parent);
            act.transform.Find("Image").GetComponent<Image>().sprite = icon;
            AddButtonSound(act, sound);
            if (action != null)
                act.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        try
                        {
                            action();
                        }
                        catch (Exception e)
                        {
                            ExceptionHelper.ShowException(e);
                        }
                    }
                );
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
            return CreateImageLabel(parent, title, SpriteHelper.Load(icon));
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

        public static void AddButtonSound(GameObject g, string sound)
        {
            if (sound != null)
                g.GetComponent<Button>().onClick.AddListener(() => { NAudio.Play(sound); });
        }
    }
}