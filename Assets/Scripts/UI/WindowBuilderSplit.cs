using System.Collections.Generic;
using System.Linq;
using Audio;
using UI.Show;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public partial class WindowBuilderSplit : MonoBehaviour, ISplitManager
    {
        public GameObject splitElementButtonPanel;
        public GameObject infoPanel;
        public GameObject buttonPanel;
        public string selectButtonText;
        public Button selectButton;

        private List<SplitElement> elements;
        private SplitElement selectedElement;
        private bool _finish;

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
                NAudio.Play(selectedElement.audioPerform);
                selectedElement.Perform();
                CloseWindow();
            }, null);
            selectButton.GetComponent<Button>().enabled = false;
        }

        public void CloseWindow()
        {
            Destroy(gameObject);
        }

        public int Count()
        {
            return elements.Count;
        }

        public void Close()
        {
            CloseWindow();
        }

        public void Add(SplitElement ele, bool first = false)
        {
            if (first)
                elements.Insert(0, ele);
            else
                elements.Add(ele);

            ele.window = this;

            //add button?
            if (_finish)
            {
                AddSplitButton(ele);
            }
        }

        private void ClickButton(SplitElement ele)
        {
            //disabled?
            if (ele.disabled != null)
            {
                //has a button?
                if (selectButton != null)
                {
                    UIHelper.UpdateButtonText(selectButton, ele.disabled);
                    selectButton.GetComponent<Button>().enabled = false;
                }

                selectedElement = ele;
                ShowDetail();
                return;
            }

            //same element?
            if (ele == selectedElement && selectButton != null)
            {
                NAudio.Play(selectedElement.audioPerform);
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

        private void ShowDetail()
        {
            if (infoPanel.transform.childCount > 0)
            {
                foreach (Transform child in infoPanel.transform) {
                    Destroy(child.gameObject);
                };
            }

            PanelBuilder p = PanelBuilder.Create(infoPanel.transform);
            selectedElement.ShowDetail(p);
            p.CalcSize();
            NAudio.Play(selectedElement.audioSwitch);
        }

        public void Reload()
        {
            ShowDetail();
        }

        private void AddSplitButton(SplitElement ele)
        {
            ele.button = UIHelper.CreateImageTextButton(ele.title, ele.icon, splitElementButtonPanel.transform,() =>
                {
                    ClickButton(ele);
                }
                ,null);
        }
        
        public void Finish()
        {
            splitElementButtonPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0,(elements.Count-1)*32);

            foreach (SplitElement ele in elements)
            {
                AddSplitButton(ele);
            }
            
            //select first element
            if (elements.Count > 0)
            {
                ClickButton(elements.First());
            }
            
            gameObject.SetActive(true);
            _finish = true;
        }
    }
}
