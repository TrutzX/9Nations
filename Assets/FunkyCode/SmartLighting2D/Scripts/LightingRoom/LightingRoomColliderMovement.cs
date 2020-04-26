using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LightingRoomColliderMovement {
	public bool moved = false;

	private Vector2 movedPosition = Vector2.zero;
	private Vector2 movedScale = Vector3.zero;
	private float movedRotation = 0;
	
	private bool flipX = false;
	private bool flipY = false;

	public void Reset() {
		movedPosition = Vector2.zero;
		movedRotation = 0;
		movedScale = Vector3.zero;
	}

	public void Update(LightingRoom2D collider) {
		Vector2 position = collider.transform.position;
		Vector2 scale = collider.transform.lossyScale;
		float rotation = collider.transform.rotation.eulerAngles.z;

		moved = false;

		if (movedPosition != position) {
			movedPosition = position;
			moved = true;
		}
				
		if (movedScale != scale) {
			movedScale = scale;
			moved = true;
		}

		if (movedRotation != rotation) {
			movedRotation = rotation;
			moved = true;
		}
	}
}