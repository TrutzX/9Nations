using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WindowUIHelper : ScriptableObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SetWindowTitle(GameObject window, string title)
    {
        window.transform.Find("Header").GetComponent<Text>().text = title;
    }
    
    //public static void 
}
