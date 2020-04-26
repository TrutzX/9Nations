using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LightingRoom2D : MonoBehaviour {
	public enum RoomType {Collider};
	public Color color = Color.black;
	public RoomType roomType = RoomType.Collider;

	public LightingColliderShape shape = new LightingColliderShape();
	public LightingRoomColliderMovement movement = new LightingRoomColliderMovement();

	public static List<LightingRoom2D> list = new List<LightingRoom2D>();

	public void OnEnable() {
		list.Add(this);
	}

	public void OnDisable() {
		list.Remove(this);
	}

	public void Update() {
		movement.Update(this);

		if (movement.moved == true) {
			shape.ResetLocal();
		}

		shape.maskType = LightingCollider2D.MaskType.Collider;
	}

	static public List<LightingRoom2D> GetList() {
		return(list);
	}
}
