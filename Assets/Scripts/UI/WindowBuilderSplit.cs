using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WindowBuilderSplit : MonoBehaviour
    {
        public GameObject splitElementButtonPanel;
        public GameObject infoPanel;
        public GameObject buttonPanel;
        public string selectButtonText;
        public GameObject selectButton;

        private List<SplitElement> elements;
        private SplitElement selectedElement;

        /// <summary>
        /// Use the create method
        /// </summary>
        private WindowBuilderSplit(){}
        
        public static WindowBuilderSplit Create(string title, string buttonText)
        {
            GameObject act = Instantiate(UIElements.Get().splitWindow, GameObject.Find("WindowsMgmt").transform);

            act.name = title;
            act.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = title;

            WindowBuilderSplit w = act.GetComponent<WindowBuilderSplit>();
            w.elements = new List<SplitElement>();
        
            //has a button?
            if (buttonText != null)
            {
                w.SetButtonText(buttonText);
            
            }
        
            return w;
        }

        public void SetButtonText(string buttonText)
        {
            if (buttonText == null)
            {
                return;
            }

            this.selectButtonText = buttonText;
            selectButton = UIHelper.CreateButton("Select an element first",buttonPanel.transform,()=>
            {
                selectedElement.Perform();
                CloseWindow();
            });
            selectButton.GetComponent<Button>().enabled = false;
        }

        public void CloseWindow()
        {
            Destroy(gameObject);
        }

        public void AddElement(SplitElement ele)
        {
            elements.Add(ele);
            ele.button = UIHelper.CreateImageTextButton(ele.title, ele.icon, splitElementButtonPanel.transform,() =>
                {
                    //disabled?
                    if (ele.disabled != null)
                    {
                        //has a button?
                        if (selectButton != null)
                        {
                            UIHelper.UpdateButtonText(selectButton,ele.disabled);
                            selectButton.GetComponent<Button>().enabled = false;
                        }

                        selectedElement = ele;
                        ShowDetail();
                        return;
                    }
             
                    //same element?
                    if (ele == selectedElement && selectButton != null)
                    {
                        selectedElement.Perform();
                        CloseWindow();
                        return;
                    }

                    //show infos
                    selectedElement = ele;
                    if (selectButton != null)
                    {
                        selectButton.GetComponent<Button>().enabled = true;
                        UIHelper.UpdateButtonText(selectButton, $"{selectButtonText} {ele.title}");
                    }

                    //create panel
                    ShowDetail();
                }
            );
            //splitElementButtonPanel.GetComponent<RectTransform>().o
            ele.window = this;

        }

        private void ShowDetail()
        {
            if (infoPanel.transform.childCount > 0)
            {
                foreach (Transform child in infoPanel.transform) {
                    GameObject.Destroy(child.gameObject);
                };
            }

            PanelBuilder p = PanelBuilder.Create(infoPanel.transform);
            selectedElement.ShowDetail(p);
            p.CalcSize();
            NAudio.Play(selectedElement.audioSwitch);
        }
        
        public void Finish()
        {
            splitElementButtonPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0,(elements.Count-1)*32);
            gameObject.SetActive(true);
        }

        public abstract class SplitElement
        {
            public string title;
            public string audioSwitch;
            public Sprite icon;
            public GameObject button;
            public WindowBuilderSplit window;
        
            /// <summary>
            /// content = error message to display
            /// </summary>
            public string disabled;


            protected SplitElement(string title, Sprite icon)
            {
                this.title = title;
                this.icon = icon;
                audioSwitch = "switch";
            }
            
            protected SplitElement(string title, string icon) : this (title, SpriteHelper.LoadIcon(icon)){}

            public abstract void ShowDetail(PanelBuilder panel);

            public abstract void Perform();
        }
    }
}
