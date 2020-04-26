using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_2018_1_OR_NEWER

// Used When Sorting Objects
public class LightingBufferTile : LightingBufferBase{

	static public void Shadow(LightingBuffer2D buffer, LightingTile tile, Vector2D polyOffset, LightingTilemapCollider2D tilemap, float lightSizeSquared, float z) {
		if (tile.GetPairs(tilemap).Count < 1) {
			return;
		}

		polygons = tile.GetShapePolygons();
		polygonPairs = tile.GetPairs(tilemap);

		scale.x = tilemap.transform.lossyScale.x;
		scale.y = tilemap.transform.lossyScale.y;

		LightingBufferShadow.Draw(buffer, polygons, polygonPairs, lightSizeSquared, z, polyOffset, scale);
	}
	
	public class WithAtlas {
		
		static public void MaskSprite(LightingBuffer2D buffer, LightingTile tile, LayerSetting layerSetting, LightingTilemapCollider2D id, Vector2D offset, float z) {
			if (id.maskType != LightingTilemapCollider2D.MaskType.Sprite) {
				return;
			}

			if (id.maskType == LightingTilemapCollider2D.MaskType.None) {
				return;
			}

			if (tile.GetOriginalSprite() == null) {
				return;
			}

			Sprite sprite = tile.GetAtlasSprite();

			scale.x = id.transform.lossyScale.x;
			scale.y = id.transform.lossyScale.y;

			if (sprite == null) {
				Sprite reqSprite = SpriteAtlasManager.RequestSprite(tile.GetOriginalSprite(), SpriteAtlasRequest.Type.WhiteMask);
				if (reqSprite == null) {
					PartiallyBatchedTilemap batched = new PartiallyBatchedTilemap();

					batched.virtualSpriteRenderer = new VirtualSpriteRenderer();
					batched.virtualSpriteRenderer.sprite = tile.GetOriginalSprite();

					batched.polyOffset = offset.ToVector2();

					batched.tileSize = scale;

					batched.tilemap = id;

					buffer.partiallyBatchedList_Tilemap.Add(batched);
					return;
				} else {
					tile.SetAtlasSprite(reqSprite);
					sprite = reqSprite;
				}
			}

			spriteRenderer.sprite = sprite;

			LightingGraphics.WithAtlas.DrawSprite(spriteRenderer, layerSetting, id.maskMode, offset.ToVector2(), scale, id.transform.rotation.eulerAngles.z, z);

			LightingDebug.maskGenerations ++;		
		}
	}

	public class WithoutAtlas {
		static public void MaskSprite(LightingBuffer2D buffer, LightingTile tile, LightingLayerEffect maskEffect, Material materialA, Material materialB, Vector2D polyOffset, LightingTilemapCollider2D tilemap, float lightSizeSquared, float z) {
			spriteRenderer.sprite = tile.GetOriginalSprite();

			if (spriteRenderer.sprite == null) {
				return;
			}

			scale.x = tilemap.transform.lossyScale.x;
			scale.y = tilemap.transform.lossyScale.y;
			
			Material material = materialA;
			if (tilemap.maskMode == LightingMaskMode.Invisible || (maskEffect == LightingLayerEffect.InvisibleBellow && polyOffset.y < 0)) {
				material = materialB;
			}

			material.mainTexture = spriteRenderer.sprite.texture;

			LightingGraphics.WithoutAtlas.DrawSprite(material, spriteRenderer, polyOffset.ToVector2(), scale, 0, z);
			
			material.mainTexture = null;

			LightingDebug.maskGenerations ++;
		}
	}

	/*
	// Lighting Buffer TILE
	static public void MaskShape(LightingBuffer2D buffer, LightingTile tile, LightRenderingOrder lightSourceOrder, LightingTilemapCollider2D id, Vector2D offset, float z) {
		Mesh tileMesh = null;

		if (id.maskType == LightingTilemapCollider2D.MaskType.Tile) {
			tileMesh = LightingTile.GetStaticTileMesh();
		} else if (id.maskType == LightingTilemapCollider2D.MaskType.SpriteCustomPhysicsShape) {
			tileMesh = tile.GetTileDynamicMesh();
		}

		if (tileMesh == null) {
			return;
		}

		// Set Color Black Or White?
		GL.Color(Color.white);
		
		int triangleCount = tileMesh.triangles.GetLength (0);
		for (int i = 0; i < triangleCount; i = i + 3) {
			vecA = tileMesh.vertices [tileMesh.triangles [i]];
			vecB = tileMesh.vertices [tileMesh.triangles [i + 1]];
			vecC = tileMesh.vertices [tileMesh.triangles [i + 2]];
			Max2DMatrix.DrawTriangle(vecA, vecB, vecC, offset.ToVector2(), z, new Vector2D(1, 1));
		}

		LightingDebug.maskGenerations ++;			
	} */
}

#endif