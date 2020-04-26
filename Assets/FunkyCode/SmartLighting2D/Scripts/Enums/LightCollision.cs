using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCollision2D {
	public enum CollisionType {W, Tilemap}

	public LightingSource2D lightSource;

	public LightingCollider2D collider;

	public List<Vector2D> pointsColliding;
	public LightingEventState lightingEventState;
}

public class LightTilemapCollision2D {
	public enum CollisionType {W, Tilemap}

	public LightingSource2D lightSource;

	public LightingTilemapCollider2D tilemapCollider;

	public List<Vector2D> pointsColliding;
	public LightingEventState lightingEventState;
}