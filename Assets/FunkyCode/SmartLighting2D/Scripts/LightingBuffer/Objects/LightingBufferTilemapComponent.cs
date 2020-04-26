using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_2018_1_OR_NEWER

public class LightingBufferTilemapComponent : LightingBufferBase {

	static public void Shadow(LightingBuffer2D buffer, LightingTilemapCollider2D id, float lightSizeSquared, float z) {
		if (id.colliderType != LightingTilemapCollider2D.ColliderType.Collider) {
			return;
		}

		polyOffset.x = -buffer.lightSource.transform.position.x;
		polyOffset.y = -buffer.lightSource.transform.position.y;

		ShadowEdge(buffer, id, lightSizeSquared, z);
		ShadowPolygon(buffer, id, lightSizeSquared, z);
	}

	static public void ShadowPolygon(LightingBuffer2D buffer, LightingTilemapCollider2D id, float lightSizeSquared, float z) {
		polygonPairs = new List<List<Pair2D>>();
		
		for(int i = 0; i < id.polygonColliders.Count; i++) {
			polygon = id.polygonColliders[i];

			pairList = Pair2D.GetList(polygon.pointsList);
			polygonPairs.Add(pairList);
		}

		LightingBufferShadow.Draw(buffer, id.polygonColliders, polygonPairs, lightSizeSquared, z, polyOffset, scale);
	}

	static public void ShadowEdge(LightingBuffer2D buffer, LightingTilemapCollider2D id, float lightSizeSquared, float z) {
		polygonPairs = new List<List<Pair2D>>();

		for(int i = 0; i < id.edgeColliders.Count; i++) {
			polygon = id.edgeColliders[i];

			pairList = Pair2D.GetList(polygon.pointsList, false);
			polygonPairs.Add(pairList);
		}

		LightingBufferShadow.Draw(buffer, id.edgeColliders, polygonPairs, lightSizeSquared, z, polyOffset, scale);
	}
}

#endif