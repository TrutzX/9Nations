using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public partial class PanelBuilderSplit : MonoBehaviour
    {
        public GameObject splitElementButtonPanel;
        public GameObject infoPanel;
        public GameObject buttonPanel;

        private List<SplitElement> elements;
        private SplitElement selectedElement;

        /// <summary>
        /// Use the create method
        /// </summary>
        private PanelBuilderSplit(){}
        
        public static PanelBuilderSplit Create(Transform transform, List<SplitElement> elements)
        {
            GameObject act = Instantiate(UIElements.Get().panelSplit, transform);

            PanelBuilderSplit w = act.GetComponent<PanelBuilderSplit>();
            w.elements = elements;
            
            //build ui
            foreach (SplitElement ele  in elements)
            {
                w.AddElement(ele);
            }

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

        private void ClickButton(SplitElement ele)
        {
            //show infos
            selectedElement = ele;

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
