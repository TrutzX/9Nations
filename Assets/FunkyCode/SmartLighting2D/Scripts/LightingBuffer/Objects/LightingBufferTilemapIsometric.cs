using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_2018_1_OR_NEWER

public class LightingBufferTilemapIsometric : LightingBufferBase {

	static public void Shadow(LightingBuffer2D buffer, LightingTilemapCollider2D id, float lightSizeSquared, float z) {
		if (id.mapType != LightingTilemapCollider2D.MapType.UnityEngineTilemapIsometric) {
			return;
		}

		if (id.colliderType == LightingTilemapCollider2D.ColliderType.None) {
			return;
		}

		if (id.colliderType == LightingTilemapCollider2D.ColliderType.Collider) {
			return;
		}
		
		if (id.isometricMap == null) {
			return;
		}

		offset.x = -buffer.lightSource.transform.position.x;
		offset.y = -buffer.lightSource.transform.position.y;

		foreach(LightingTilemapCollider2D.IsometricMapTile tile in id.isometricMap.mapTiles) {
			polygons = tile.tile.GetPolygons(id);

			if (polygons == null || polygons.Count < 1) {
				continue;
			}

			polygonPairs = tile.tile.GetPairs(id);

			polyOffset.x = offset.x;
			polyOffset.y = offset.y;

			polyOffset.x += tile.position.x * 0.5f;
			polyOffset.y += tile.position.x * 0.5f * id.cellSize.y;

			polyOffset.x += tile.position.y * -0.5f;
			polyOffset.y += tile.position.y * 0.5f * id.cellSize.y;

			if (id.colliderType == LightingTilemapCollider2D.ColliderType.SpriteCustomPhysicsShape) {
				polyOffset.y += 0.25f;
			}

			//polyOffset.x *= id.cellSize.x;

			LightingBufferShadow.Draw(buffer, polygons, polygonPairs, lightSizeSquared, z, polyOffset, Vector3.one);
		}
	}

	public class WithoutAtlas {
		static public void MaskSprite(LightingBuffer2D buffer, LightingTilemapCollider2D id, Material material, float z) {
			if (id.mapType != LightingTilemapCollider2D.MapType.UnityEngineTilemapIsometric) {
				return;
			}

			if (id.maskType != LightingTilemapCollider2D.MaskType.Sprite) {
				return;
			}

			if (id.isometricMap == null) {
				return;
			}

			tileMesh = LightingTile.GetStaticTileMesh(id);

			offset.x = -buffer.lightSource.transform.position.x;
			offset.y = -buffer.lightSource.transform.position.y;

			GL.Color(Color.white);

			foreach(LightingTilemapCollider2D.IsometricMapTile tile in id.isometricMap.mapTiles) {
				polyOffset.x = offset.x;
				polyOffset.y = offset.y;

				spriteRenderer.sprite = tile.tile.GetOriginalSprite();

				polyOffset2.x = (float)polyOffset.x;
				polyOffset2.y = (float)polyOffset.y;

				polyOffset2.y += 0.5f * id.cellSize.y;

				polyOffset2.x += tile.position.x * 0.5f;
				polyOffset2.y += tile.position.x * 0.5f * id.cellSize.y;

				polyOffset2.x += tile.position.y * -0.5f ;
				polyOffset2.y += tile.position.y * 0.5f * id.cellSize.y;

			
				
				
				material.mainTexture = spriteRenderer.sprite.texture;

				if (Vector2.Distance(Vector2.zero, polyOffset.ToVector2()) > buffer.lightSource.lightSize * 1.5f) {
					continue;
				}



				LightingGraphics.WithoutAtlas.DrawSprite(material, spriteRenderer, polyOffset2, scale, 0, z);
				
				material.mainTexture = null;
			}
		}
	}

	// Supports only static "tile" shape
	// No support for Custom Physics Shape
	static public void MaskShape(LightingBuffer2D buffer, LightingTilemapCollider2D id, float z) {
		if (id.mapType != LightingTilemapCollider2D.MapType.UnityEngineTilemapIsometric) {
			return;
		}

		if (id.maskType == LightingTilemapCollider2D.MaskType.None) {
			return;
		}

		if (id.maskType == LightingTilemapCollider2D.MaskType.Sprite) {
			return;
		}
		
		if (id.isometricMap == null) {
			return;
		}

		Vector2 vecA, vecB, vecC;

		tileMesh = LightingTile.GetStaticTileMesh(id);

		offset.x = -buffer.lightSource.transform.position.x;
		offset.y = -buffer.lightSource.transform.position.y;
		
		GL.Color(Color.white);

		foreach(LightingTilemapCollider2D.IsometricMapTile tile in id.isometricMap.mapTiles) {
			polygons = tile.tile.GetPolygons(id);
			polygonPairs = tile.tile.GetPairs(id);

			polyOffset.x = offset.x;
			polyOffset.y = offset.y;

			polyOffset2.x = (float)polyOffset.x;
			polyOffset2.y = (float)polyOffset.y;

			polyOffset2.y += 0.5f * id.cellSize.y;

			polyOffset2.x += tile.position.x * 0.5f;
			polyOffset2.y += tile.position.x * 0.5f * id.cellSize.y;

			polyOffset2.x += tile.position.y * -0.5f ;
			polyOffset2.y += tile.position.y * 0.5f * id.cellSize.y;

			if (id.colliderType == LightingTilemapCollider2D.ColliderType.SpriteCustomPhysicsShape) {
				polyOffset2.y += 0.25f;
			}

			if (Vector2.Distance(Vector2.zero, polyOffset2) > buffer.lightSource.lightSize * 1.5f) {
				continue;
			}

			// Batch and Optimize???
			int triangleCount = tileMesh.triangles.GetLength (0);
			for (int i = 0; i < triangleCount; i = i + 3) {
				vecA = tileMesh.vertices [tileMesh.triangles [i]];
				vecB = tileMesh.vertices [tileMesh.triangles [i + 1]];
				vecC = tileMesh.vertices [tileMesh.triangles [i + 2]];

				Max2DMatrix.DrawTriangle(vecA, vecB, vecC, polyOffset2, z, scale2D);
			}
		}
	}
}

#endif
