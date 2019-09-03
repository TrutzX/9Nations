using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    public bool IsWindow;
    
    // Start is called before the first frame update
    public void GameObjectDestroy()
    {
        Destroy(gameObject);
    }

    public void DestroyWindow()
    {
        Destroy(gameObject);
        NAudio.Play("windowClose");
    }
}
