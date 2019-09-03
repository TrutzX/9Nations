using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscTest : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject window;

    public GameObject button;

    public GameObject dialogText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Debug.Log("Space key was pressed.");
                    CreateDialog("Dialog",
                        "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.");

                }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateMainWindow();
        }
    }

    private void CreateMainWindow()
    {
        GameObject win = CreateWindow("Main menu");
        GameObject content = win.transform.Find("Content").gameObject;
        content.AddComponent<VerticalLayoutGroup>();
        CreateButton("a", content.transform);
        CreateButton("a333", content.transform);
        CreateButton("aggg", content.transform);
        CreateButton("a444", content.transform);
        CreateButton("aeeee", content.transform);
        CreateButton("aeeee", content.transform);
        CreateButton("aeeee", content.transform);
        CreateButton("aeeee", content.transform);
        CreateButton("aeeee", content.transform);
    }

    GameObject CreateWindow(string title)
    {
        GameObject act = Instantiate(window, GameObject.Find("UICanvas").transform);
        act.transform.Find("Header").GetComponent<Text>().text = title;
        return act;
    }

    GameObject CreateButton(string title, Transform parent)
    {
        GameObject act = Instantiate(button, parent);
        act.transform.Find("Label").GetComponent<Text>().text = title;
        return act;
    }

    GameObject CreateDialog(string title, string text)
    {
        GameObject win = CreateWindow(title);
        GameObject content =  win.transform.Find("Content").gameObject;
        GameObject dText = Instantiate(dialogText, content.transform);
        dText.GetComponent<Text>().text = text;
        //dText.GetComponent<RectTransform>().setA
        GameObject button = CreateButton("Ok", content.transform);
        button.GetComponent<Button>().onClick.AddListener(delegate{Destroy(win);});
           
        return win;
    }
}
