using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoFollowMouse : MonoBehaviour {
	LightingSource2D lightSource;

	void Start() {
		lightSource = GetComponent<LightingSource2D>();
	}

	void Update () {
		Vector3 pos = GetMousePosition().ToVector2();
		pos.z = transform.position.z;

		lightSource.movement.SetPosition(pos);
	}

	public static Vector2D GetMousePosition() {
		return(new Vector2D (Camera.main.ScreenToWorldPoint (Input.mousePosition)));
	}
}
