using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LightingTextureRenderer2D : MonoBehaviour {
    public Texture texture;
	public Color color = Color.white;
    public Vector2 size = Vector2.one;

	public static List<LightingTextureRenderer2D> list = new List<LightingTextureRenderer2D>();

	public void OnEnable() {
		list.Add(this);
	}

	public void OnDisable() {
		list.Remove(this);
	}

    static public List<LightingTextureRenderer2D> GetList() {
		return(list);
	}

	public bool InCamera(Camera camera) {
		float cameraSize = camera.orthographicSize * 1.5f;

        float horizontalSize = cameraSize + size.x;
		float verticalSize = cameraSize + size.y;

		return(Vector2.Distance(transform.position, camera.transform.position) < Mathf.Sqrt((verticalSize) * (horizontalSize)) );
	}
}
