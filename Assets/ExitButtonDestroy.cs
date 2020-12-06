using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButtonDestroy : MonoBehaviour
{
    public GameObject window;

    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(DestroyWindow);
    }

    void DestroyWindow()
    {
        Destroy(window);
    }
}
