using System.Collections.Generic;
using Game;
using Tools;
using UI.Show;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WindowTabBuilder : MonoBehaviour, IWindow
    {
        private List<Tab> _tabs;
        private string _title;

        public Text header;
        public GameObject content;
        public GameObject tabList;
        
        /// <summary>
        /// Use the create method
        /// </summary>
        private WindowTabBuilder(){}
        public static WindowTabBuilder Create(string title)
        {
            title = TextHelper.Cap(title);
            GameObject act = Instantiate(UIElements.Get().tabWindow, GameObject.Find("WindowsMgmt").transform);
            act.name = title;
            act.GetComponent<WindowTabBuilder>().Init(title);
        
            return act.GetComponent<WindowTabBuilder>();
        }
        public static WindowTabBuilder CreateT(string title)
        {
            return Create(S.T(title));
        }

        private void Init(string title)
        {
            _tabs = new List<Tab>();
            _title = title;
            header.text = title;
        }
        
        public void Add(Tab tab)
        {
            _tabs.Add(tab);
            tab.window = this;
        }

        public void Finish()
        {
            //build tabs
            foreach (Tab t in _tabs)
            {
                UIElements.CreateImageButton(t.Icon(), tabList.transform, () => { ShowTab(t); });
            }

            //show first tab?
            if (_tabs.Count > 0)
            {
                ShowTab(_tabs[0]);
            }

            gameObject.SetActive(true);
        }

        public int Count()
        {
            return _tabs.Count;
        }
        
        public void CloseWindow()
        {
            Destroy(gameObject);
        }

        public void ShowTab(Tab t)
        {
            header.text = $"{_title} | {t.Name()}";
            UIHelper.ClearChild(content);
            t.Show(content.transform);
        }

        public void ShowTab(int t)
        {
            ShowTab(_tabs[t]);
        }
    }
}