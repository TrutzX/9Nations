using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Non Batched Objects While Batching Is Enabled

public class PartiallyBatchedTilemap {
	public VirtualSpriteRenderer virtualSpriteRenderer;
	public Vector2 polyOffset; 
	public Vector2 tileSize;

	#if UNITY_2018_1_OR_NEWER
		public LightingTilemapCollider2D tilemap;
	#endif
}

public class PartiallyBatchedCollider {
	public LightingCollider2D collider;
}
