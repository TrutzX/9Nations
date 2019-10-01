using System;
using System.Collections;
using System.Collections.Generic;
using DataTypes;
using Players;
using reqs;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PanelBuilder : MonoBehaviour
{
    public GameObject panel;
    
    public static PanelBuilder Create(Transform transform)
    {
        GameObject p = Instantiate(UIElements.Get().panelBuilder, transform);
        return p.GetComponent<PanelBuilder>();
    }
    
    public GameObject AddLabel(string title)
    {
        GameObject button = Instantiate(UIElements.Get().panelLabel, panel.transform);
        button.name = title;
        button.GetComponent<Text>().text = title;

        return button;
    }
    
    public GameObject AddDesc(string title)
    {
        GameObject button = Instantiate(UIElements.Get().panelLabelDesc, panel.transform);
        button.GetComponent<Text>().text = title;

        return button;
    }
    public GameObject AddHeaderLabel(string title)
    {
        return UIElements.CreateHeaderLabel(panel.transform, title);
    }

    public GameObject AddImageLabel(string title, Sprite icon)
    {
        return UIElements.CreateImageLabel(panel.transform, title, icon);
    }

    public GameObject AddButton(string title, Action action, string sound = "click")
    {
        GameObject g = UIHelper.CreateButton(title,panel.transform,action);
        if (sound != null)
            g.GetComponent<Button>().onClick.AddListener(() => { NAudio.Play(sound); });
        return g;
    }

    public GameObject AddImageButton(string title, Sprite icon, Action action, string sound = "click")
    {
        GameObject g = UIHelper.CreateImageTextButton(title, icon, panel.transform, action, sound);
        
        return g;
    }

    public GameObject AddImageLabel(string title, string icon)
    {
        return UIElements.CreateImageLabel(panel.transform, title, icon);
    }
    
    public void AddRess(string title, Dictionary<string, int> ress)
    {
        //addHeader
        if (ress.Count > 0)
        AddHeaderLabel(title);
        
        //add ress
        foreach (KeyValuePair<string, int> r in ress)
        {
            AddImageLabel($"{r.Value}x {Data.ress[r.Key].name}", Data.ress[r.Key].icon);
        }
    }
    
    public GameObject AddInput(string title, string def, UnityAction<string> save)
    {
        GameObject button = Instantiate(UIElements.Get().input, panel.transform);
        button.transform.GetChild(0).GetComponent<Text>().text = $"Enter {title}...";
        button.GetComponent<InputField>().text = def;
        button.GetComponent<InputField>().onValueChanged.AddListener((val) => { save(val); });
        return button;
    }
    
    public Slider AddSlider(int min, int max, int def, UnityAction<int> save)
    {
        Slider slider = Instantiate(UIElements.Get().slider, panel.transform);
        slider.minValue = min;
        slider.value = def;
        slider.maxValue = max;
        slider.onValueChanged.AddListener((val) => { save((int)val); });
        return slider;
    }
    
    public GameObject AddDropdown(string[] values, string def, string[] titles, UnityAction<string> save)
    {
        GameObject button = Instantiate(UIElements.Get().dropdown.gameObject, panel.transform);
        
        foreach(string t in titles)
        {
            button.GetComponent<Dropdown>().options.Add(new Dropdown.OptionData(t));
        }

        button.GetComponent<Dropdown>().value = Array.IndexOf(values, def);
        button.GetComponent<Dropdown>().onValueChanged.AddListener((pos) => { save(values[pos]); });
        
        return button;
    }
    
    public Toggle AddCheckbox(bool value, string title, UnityAction<bool> save, string audio="checkBoxClick")
    {
        Toggle button = UIElements.CreateCheckBox(panel.transform, title, save).GetComponent<Toggle>();
        button.isOn = value;
        button.onValueChanged.AddListener((pos) => { NAudio.Play(audio); });
        return button;
    }

    public void AddAction(string title, Dictionary<string, string> reqs)
    {
        //addHeader
        if (reqs.Count == 0)
            return;
            
        AddHeaderLabel(title);
        
        //add req
        foreach (KeyValuePair<string, string> req in reqs)
        {
            NAction n = Data.nAction[req.Key];
            
            AddImageLabel(n.desc,SpriteHelper.LoadIcon(n.icon));
        }
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
            AddLabel(NLib.GetReq(req.Key).Desc(req.Value));
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
            BaseReq r = NLib.GetReq(req.Key);
            AddImageLabel(r.Desc(req.Value), r.Check(PlayerMgmt.ActPlayer(), req.Value) ? "ui:yes" : "ui:no");
        }
    }

    public void AddReq(string title, Dictionary<string, string> reqs,GameObject onMap, int x, int y)
    {
        //addHeader
        if (reqs.Count > 0)
        AddHeaderLabel(title);
        
        //add req
        foreach (KeyValuePair<string, string> req in reqs)
        {
            BaseReq r = NLib.GetReq(req.Key);
            AddImageLabel(r.Desc(PlayerMgmt.ActPlayer(),onMap, req.Value, x, y), r.Check(PlayerMgmt.ActPlayer(), onMap, req.Value, x, y) ? "ui:yes" : "ui:no");
        }
    }

    public int Count()
    {
        return panel.transform.childCount;
    }

    public void CalcSize()
    {
        panel.GetComponent<RectTransform>().sizeDelta = new Vector2(0,(Count())*32);
    }
}
