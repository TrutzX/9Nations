using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoCollisionEvents : MonoBehaviour {
    static public Texture texture;
    
    LightingCollider2D collider;
    List<Vector2D> collisionPoints = null;
    Transform lightTransform;

    void Start() {
        collider = GetComponent<LightingCollider2D>();
        collider.AddEvent(collisionEvent);
    }

    void collisionEvent(LightCollision2D collision) {
        if (collision.pointsColliding != null) {
            if (collisionPoints == null) {
                collisionPoints = collision.pointsColliding;
                lightTransform = collision.lightSource.transform;
            } else {
                if (collision.pointsColliding.Count >= collisionPoints.Count || lightTransform == collision.lightSource.transform) {
                    collisionPoints = collision.pointsColliding;
                    lightTransform = collision.lightSource.transform;
                }
            }
        } else {
            collisionPoints = null;
            lightTransform = null;
        }
    }

	static public Texture GetTexture() {
		if (texture == null) {
			texture = Resources.Load<Texture>("Textures/dot");
		}
		return(texture);
	}

    void OnGUI() {
        Vector2 middlePoint = Camera.main.WorldToScreenPoint(transform.position);
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;

        if (collisionPoints == null) {
            GUI.Label(new Rect(middlePoint.x - 50, Screen.height - middlePoint.y - 50, 100, 100), "Hiden");
            return;
        }

        foreach(Vector2D point in collisionPoints) {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(point.ToVector3() + lightTransform.transform.position);
            GUI.DrawTexture(new Rect(screenPoint.x - 5, Screen.height - screenPoint.y - 5, 10, 10), GetTexture());
        }

        int percentage = (int)(((float)collisionPoints.Count / collider.shape.GetPolygons_Collider_Local(transform)[0].pointsList.Count) * 100);
        GUI.Label(new Rect(middlePoint.x - 50, Screen.height - middlePoint.y - 50, 100, 100), percentage.ToString() + "%");
    }
}
