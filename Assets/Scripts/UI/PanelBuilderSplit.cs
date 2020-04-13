using System.Collections.Generic;
using System.Linq;
using Audio;
using UI.Show;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PanelBuilderSplit : MonoBehaviour
    {
        public GameObject splitElementButtonPanel;
        public GameObject infoPanel;
        protected Button selectButton;
        public string selectButtonText;
        public GameObject buttonPanel;
        protected IWindow window;

        private List<SplitElement> elements;
        private SplitElement selectedElement;

        /// <summary>
        /// Use the create method
        /// </summary>
        private PanelBuilderSplit() { }
        
        public static PanelBuilderSplit Create(Transform transform, IWindow window, List<SplitElement> elements, string buttonText = null)
        {
            GameObject act = Instantiate(UIElements.Get().panelSplit, transform);

            PanelBuilderSplit w = act.GetComponent<PanelBuilderSplit>();
            w.elements = elements;
            w.window = window;
            
            //build ui
            foreach (SplitElement ele  in elements)
            {
                w.AddElement(ele);
            }

            w.SetButtonText(buttonText);
            w.Finish();
            
            return w;
        }

        public int ElementCount()
        {
            return elements.Count;
        }

        public void AddElement(SplitElement ele)
        {
            ele.button = UIHelper.CreateImageTextButton(ele.title, ele.icon, splitElementButtonPanel.transform,() =>
                {
                    ClickButton(ele);
                }
            ,null);
        }

        public void SetButtonText(string buttonText)
        {
            if (buttonText == null)
            {
                return;
            }

            selectButtonText = buttonText;
            selectButton = UIHelper.CreateButton("Select an element first",buttonPanel.transform,()=>
            {
                NAudio.Play(selectedElement.audioPerform);
                selectedElement.Perform();
            }).GetComponent<Button>();
            selectButton.enabled = false;
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
                UIHelper.ClearChild(infoPanel);
            }

            PanelBuilder p = PanelBuilder.Create(infoPanel.transform);
            selectedElement.ShowDetail(p);
            p.CalcSize();
            NAudio.Play(selectedElement.audioSwitch);
        }

        public void Finish()
        {
            splitElementButtonPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0,(elements.Count-1)*32);
            
            //select first element
            if (elements.Count > 0)
            {
                ClickButton(elements.First());
            }
            
            gameObject.SetActive(true);
        }
    }
}
