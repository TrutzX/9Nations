using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColliderDepth : System.Collections.Generic.IComparer<ColliderDepth> {
	public enum Type {Collider, Tile};

	// Lighting Collider
	public LightingCollider2D collider;

	public Vector2D polyOffset = Vector2D.Zero();
	public Type type;
	public float distance;

	#if UNITY_2018_1_OR_NEWER
		// Lighting Tilemap Tile
		public LightingTile tile;
	#endif

	#if UNITY_2018_1_OR_NEWER
		public LightingTilemapCollider2D tilemap;
	#endif

	public int Compare(ColliderDepth a, ColliderDepth b) {
		ColliderDepth c1 = (ColliderDepth)a;
		ColliderDepth c2 = (ColliderDepth)b;

		if (c1.distance > c2.distance) {
			return 1;
		}
	
		if (c1.distance < c2.distance) {
			return -1;
		} else {
			return 0;
		}
	}

	public static System.Collections.Generic.IComparer<ColliderDepth> Sort() {      
		return (System.Collections.Generic.IComparer<ColliderDepth>) new ColliderDepth();
	}
}

public class ColliderDepthList {
    public ColliderDepth[] list = new ColliderDepth[1024];

    public int count = 0;

    public ColliderDepthList() {
        for(int i = 0; i < list.Length; i++) {
            list[i] = new ColliderDepth();
        }
    }

    public void Add(LightingCollider2D collider2D, float dist) {
		if (count + 1 < list.Length) {
			list[count].type = ColliderDepth.Type.Collider;
			list[count].collider = collider2D;
			list[count].distance = dist;
			count++;
		} else {
			Debug.LogError("Collider Depth Overhead!");
		}
    }

	#if UNITY_2018_1_OR_NEWER
		public void Add(LightingTilemapCollider2D tilemap, LightingTile tile2D, float dist, Vector2D polyOffset) {
			if (count + 1 < list.Length) {
				list[count].type = ColliderDepth.Type.Tile;
				list[count].tile = tile2D;
				list[count].tilemap = tilemap;
				list[count].distance = dist;
				list[count].polyOffset.x = polyOffset.x;
				list[count].polyOffset.y = polyOffset.y;
				// Tile Size?

				count++;
			} else {
				Debug.LogError("Tile Depth Overhead!");
			}
		}
	#endif

    public void Reset() {
        count = 0;
    }

    public void Sort() {
        Array.Sort<ColliderDepth>(list, 0, count, ColliderDepth.Sort());
    }
}