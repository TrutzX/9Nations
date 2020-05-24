using System;
using System.Collections.Generic;
using Audio;
using Buildings;
using Game;
using Libraries;
using Libraries.Modifiers;
using Players;
using reqs;
using Tools;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;
using Toggle = UnityEngine.UI.Toggle;

namespace UI
{
    public class PanelBuilder : MonoBehaviour
    {
        public GameObject panel;

        public static PanelBuilder Create(Transform transform)
        {
            GameObject p = Instantiate(UIElements.Get().panelBuilder, transform);
            return p.GetComponent<PanelBuilder>();
        }

        public void AddSubLabel(string title, string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return;
            }

            AddLabel($"{title}: {data}");
        }

        public void AddSubLabel(string title, string data, string icon)
        {
            if (string.IsNullOrEmpty(data))
            {
                return;
            }

            AddImageLabel($"{title}: {data}", icon);
        }

        public void AddSubLabel(string title, string data, Sprite icon)
        {
            if (string.IsNullOrEmpty(data))
            {
                return;
            }

            AddImageLabel($"{title}: {data}", icon);
        }

        public Text AddLabel(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return null;
            }
            
            GameObject button = Instantiate(UIElements.Get().panelLabel, panel.transform);
            button.name = title.Substring(0,Math.Min(title.Length,20));
            button.GetComponent<Text>().text = TextHelper.Cap(title);
            button.GetComponent<RectTransform>().sizeDelta = new Vector2(button.GetComponent<RectTransform>().sizeDelta.x,(title.Length/35+1)*16);

            return button.GetComponent<Text>();
        }

        public void AddLabelT(string key)
        {
            AddLabel(S.T(key));
        }

        public void RichText(string desc)
        {
            //skip it?
            if (string.IsNullOrWhiteSpace(desc))
            {
                return;
            }

            if (desc.StartsWith("@F@"))
            {
                desc = Resources.Load<TextAsset>(desc.Substring(3)).text;
            }
        
            string[] lines = desc.Split(new string[]{";;",Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                if (line.StartsWith("# "))
                {
                    AddHeaderLabel(line.Substring(2));
                    continue;
                }
                
                if (!line.StartsWith("@"))
                {
                    AddLabel(line);
                    continue;
                }

                if (line.StartsWith("@IL@"))
                {
                    string[] p = line.Split('@');
                    AddImageLabel(p[3], p[2]);
                    continue;
                }
            
                AddLabel("Error: "+line);
            }
        }
    
        public GameObject AddDesc(string title)
        {
            GameObject button = Instantiate(UIElements.Get().panelLabelDesc, panel.transform);
            button.GetComponent<Text>().text = title;

            return button;
        }

        public void AddHeaderLabelT(string key)
        {
            AddHeaderLabel(S.T(key));
        }
        
        public GameObject AddHeaderLabel(string title)
        {
            return UIElements.CreateHeaderLabel(panel.transform, TextHelper.Cap(title));
        }

        public GameObject AddImageLabel(string title, Sprite icon)
        {
            return UIElements.CreateImageLabel(panel.transform, title, icon);
        }

        public Image AddImage(Sprite icon)
        {
            Image act = Instantiate(UIElements.Get().panelImage, panel.transform).GetComponent<Image>();
            act.sprite = icon;
            //TODO dyn 300
            int h = (int) (300.0 / icon.texture.width * icon.texture.height);
            act.GetComponent<RectTransform>().sizeDelta = new Vector2(0,h);
            return act;
        }

        public Button AddButton(string title, Action action, string sound = "click")
        {
            Button g = UIHelper.CreateButton(title,panel.transform,action, sound);
            return g;
        }

        public Button AddButtonT(string title, Action action, string sound = "click")
        {
            return AddButton(S.T(title), action, sound);
        }


        public Button AddImageTextButton(string title, string icon, Action action, string sound = "click")
        {
            return AddImageTextButton(title, SpriteHelper.Load(icon), action, sound);
        }
        public Button AddImageTextButton(string title, Sprite icon, Action action, string sound = "click")
        {
            Button g = UIHelper.CreateImageTextButton(title, icon, panel.transform, action, sound);
        
            return g;
        }

        public GameObject AddImageLabel(string title, string icon)
        {
            return UIElements.CreateImageLabel(panel.transform, title, icon);
        }

        public GameObject AddImageLabel(string title, bool icon)
        {
            return AddImageLabel(title, icon?"yes":"no");
        }
    
        public void AddRes(string title, Dictionary<string, int> res)
        {
            //addHeader
            if (res.Count > 0)
                AddHeaderLabel(title);
        
            //add ress
            foreach (KeyValuePair<string, int> r in res)
            {
                L.b.res[r.Key].AddImageLabel(this, r.Value);
            }
        }
    
        public void AddModi(Dictionary<string, string> modi)
        {
            //addHeader
            if (modi.Count > 0)
                AddHeaderLabel(S.T(L.b.modifiers.Id(),modi.Count));
        
            //add ress
            foreach (KeyValuePair<string, string> p in modi)
            {
                Modifier m = L.b.modifiers[p.Key];
                AddImageLabel($"{m.Name()}: {m.Classes(p.Value).Desc(p.Value)}", m.Sprite());
            }
        }
    
        public GameObject AddInput(string title, string def, UnityAction<string> save)
        {
            GameObject button = Instantiate(UIElements.Get().input, panel.transform);
            button.transform.GetChild(0).GetComponent<Text>().text = $"Enter {title}...";
            button.GetComponent<InputField>().text = def;
            button.GetComponent<InputField>().onValueChanged.AddListener(val => { save(val); });
            return button;
        }
    
        public GameObject AddInput(string title, int def, UnityAction<int> save)
        {
            GameObject button = Instantiate(UIElements.Get().input, panel.transform);
            button.transform.GetChild(0).GetComponent<Text>().text = $"Enter {title}...";
            button.GetComponent<InputField>().text = def.ToString();
            button.GetComponent<InputField>().onValueChanged.AddListener((val) => { save(Int32.Parse(val)); });
            return button;
        }
    
        public InputField AddInputRandom(string title, string def, UnityAction<string> save, Func<string> random)
        {
            InputField input = Instantiate(UIElements.Get().inputRandom, panel.transform);
            input.transform.GetChild(0).GetComponent<Text>().text = $"Enter {title}...";
            input.text = def;
            input.onValueChanged.AddListener(val => { save(val); });

            Button button = input.transform.GetChild(2).GetComponent<Button>();
            button.onClick.AddListener(() => { NAudio.Play("random"); });
            button.onClick.AddListener(() => { input.text = random(); });
            
            return input;
        }
    
        public Slider AddSlider(int min, int max, int def, UnityAction<int> save)
        {
            Slider slider = Instantiate(UIElements.Get().slider, panel.transform);
            slider.minValue = min;
            slider.value = def;
            slider.maxValue = max;
            slider.onValueChanged.AddListener((val) =>
            {
                save((int)val);
                if (S.Advanced())
                    slider.gameObject.GetComponentInChildren<Text>().text = ((int) val).ToString();
            });
            if (S.Advanced())
                slider.gameObject.GetComponentInChildren<Text>().text = def.ToString();
            return slider;
        }
    
        public Dropdown AddDropdown(string[] values, string def, string[] titles, UnityAction<string> save)
        {
            Dropdown dropdown = Instantiate(UIElements.Get().dropdown.gameObject, panel.transform).GetComponent<Dropdown>();
        
            foreach(string t in titles)
            {
                dropdown.options.Add(new Dropdown.OptionData(t));
            }

            dropdown.value = Array.IndexOf(values, def);
            dropdown.onValueChanged.AddListener((pos) => { save(values[pos]); });
        
            return dropdown;
        }
    
        public Dropdown AddDropdown<T>(List<T> items, string def, UnityAction<string> save) where T : BaseData
        {
            List<string> ids = new List<string>();
            List<string> titles = new List<string>();
            foreach (var item in items)
            {
                if (item.Hidden)
                    continue;
                
                ids.Add(item.id);
                titles.Add(item.Name());
            }

            return AddDropdown(ids.ToArray(), def, titles.ToArray(), save);
        }
    
        public Toggle AddCheckbox(bool value, string title, UnityAction<bool> save, string audio="checkBoxClick")
        {
            Toggle button = UIElements.CreateCheckBox(panel.transform, title, save).GetComponent<Toggle>();
            button.isOn = value;
            button.onValueChanged.AddListener((pos) => { NAudio.Play(audio); });
            return button;
        }

        public void AddReq(string title, Dictionary<string, string> reqs)
        {
            //addHeader
            if (reqs.Count == 0)
                return;
            
            AddHeaderLabel(title);
        
            //add req
            foreach (KeyValuePair<string, string> req in reqs)
            {
                AddLabel(OLib.GetReq(req.Key).Desc(null, req.Value));
            }
        }

        public void AddReqCheck(string title, Dictionary<string, string> reqs)
        {
            //addHeader
            if (reqs.Count == 0)
                return;
            
            AddHeaderLabel(title);
        
            //add req
            foreach (KeyValuePair<string, string> req in reqs)
            {
                BaseReq r = OLib.GetReq(req.Key);
                AddImageLabel(r.Desc(null, req.Value), r.Check(S.ActPlayer(), req.Value) ? "ui:yes" : "ui:no");
            }
        }

        public void AddReq(string title, Dictionary<string, string> reqs,MapElementInfo onMap, NVector pos)
        {
            //addHeader
            if (reqs.Count > 0)
                AddHeaderLabel(title);
        
            //add req
            foreach (KeyValuePair<string, string> req in reqs)
            {
                BaseReq r = OLib.GetReq(req.Key);
                AddImageLabel(r.Desc(S.ActPlayer(),onMap, req.Value, pos), r.Check(S.ActPlayer(), onMap, req.Value, pos) ? "ui:yes" : "ui:no");
            }
        }

        public int Count()
        {
            return panel.transform.childCount;
        }

        public void CalcSize()
        {
            RectTransform rt = panel.GetComponent<RectTransform>();
     
            int h = 0;
            foreach (RectTransform crt in rt.GetComponentsInChildren<RectTransform>() ) {
                if ( crt == rt ) continue;
                if ( crt.parent != rt ) continue;
        
                h += (int)crt.sizeDelta.y;
            }
            panel.GetComponent<RectTransform>().sizeDelta = new Vector2(0,h);
        }
    }
}
