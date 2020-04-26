using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoLightSpriteSpawner : MonoBehaviour
{
    public GameObject prefab;

    void Start()
    {
        for(int i = 0; i < 50; i++) {
            GameObject gameObject = Instantiate(prefab);

            float size = Camera.main.orthographicSize * 4;
            gameObject.transform.position = new Vector3(Random.Range(-size, size), Random.Range(-size, size));

            DemoLightSpriteInstance sprite = gameObject.GetComponent<DemoLightSpriteInstance>();
            float speed = Random.Range(0.5f, 4f);
            sprite.velocity = new Vector3(speed, speed, 0);

            LightingSpriteRenderer2D renderer = gameObject.GetComponent<LightingSpriteRenderer2D>();
            renderer.color = Random.ColorHSV();
            renderer.alpha = 0.5f;

            gameObject.transform.parent = transform;
        }
    }

}
