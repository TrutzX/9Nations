using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class WindowPanelBuilder : MonoBehaviour
{
    public PanelBuilder panel;
    public static WindowPanelBuilder Create(string title)
    {
        GameObject act = Instantiate(UIElements.Get().panelWindow, GameObject.Find("WindowsMgmt").transform);
        act.name = title;
        act.transform.GetComponentInChildren<Text>().text = title;
        
        act.GetComponent<WindowPanelBuilder>().panel = PanelBuilder.Create(act.transform.GetChild(0).GetChild(2).transform);
        return act.GetComponent<WindowPanelBuilder>();
    }

    public void Finish(int w)
    {
        transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(w,panel.GetComponent<PanelBuilder>().GetEleCount()*32+40);
        gameObject.SetActive(true);
    }
    
    public void Finish()
    {
        Finish(300);
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
