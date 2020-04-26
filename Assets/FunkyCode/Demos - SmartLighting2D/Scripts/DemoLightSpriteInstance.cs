using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoLightSpriteInstance : MonoBehaviour
{
    public Vector2 velocity = Vector2.zero;
  
    void Update()
    {
        transform.Translate(velocity * Time.deltaTime);

        Vector3 position = transform.position;
        Vector3 cameraPos = Camera.main.transform.position;

        float verticalSize = Camera.main.orthographicSize + 2.5f;
        float horizontalSize = Camera.main.orthographicSize * ((float)Screen.width / Screen.height) + 2.5f;

        if (position.x > cameraPos.x + horizontalSize) {
            position.x = cameraPos.x - horizontalSize ;
        }

        if (position.y > cameraPos.y + verticalSize) {
            position.y = cameraPos.y - verticalSize;
        }

        transform.position = position;
    }
}
