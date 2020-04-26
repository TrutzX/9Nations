using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoPlayerController : MonoBehaviour
{
    public Transform body;
    public bool flying = false;
    public Vector2 velocity = Vector2.zero;

    SpriteRenderer spriteRenderer;

    LightingCollider2D collider;
    
    void Start()
    {
        spriteRenderer = body.GetComponent<SpriteRenderer>();

        collider = GetComponent<LightingCollider2D>();

        Physics2D.gravity = Vector2.zero;
    }

    void OnDestroy() {
        Physics2D.gravity = new Vector2(0, -10);
    }

   
    void FixedUpdate()
    {
        if (flying) {
            Vector3 pos = body.transform.localPosition;

            pos.y += velocity.y * Time.deltaTime;

            velocity.y -= 32 * Time.deltaTime;

            if (pos.y < 0) {
                pos.y = 0;

                body.transform.rotation = Quaternion.Euler(0, 0, 0);

                body.transform.localPosition = pos;
              
                velocity.y = 0;
                
                flying = false;
            } else {
                if (spriteRenderer.flipX == true) {
                    body.transform.Rotate(0, 0, 360 * Time.deltaTime);
                } else {
                    body.transform.Rotate(0, 0, -360 * Time.deltaTime);
                }
                
                body.transform.localPosition = pos;
            }
            collider.enabled = false;
        } else {
            collider.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                flying = true;
                velocity.y = 16;
            }
        }

        if (Input.GetKey(KeyCode.A)) {
            transform.Translate(-5f * Time.deltaTime, 0, 0);
            spriteRenderer.flipX = true;
        }

         if (Input.GetKey(KeyCode.D)) {
            transform.Translate(5f * Time.deltaTime, 0, 0);
            spriteRenderer.flipX = false;
        }

        if (Input.GetKey(KeyCode.W)) {
            transform.Translate(0, 2.5f * Time.deltaTime, 0);
        }

         if (Input.GetKey(KeyCode.S)) {
            transform.Translate(0, -2.5f * Time.deltaTime, 0, 0);
        }
    }
}
