using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingBufferSTE : LightingBufferBase {

	static public void Shadow(LightingBuffer2D buffer, LightingTilemapCollider2D id, float lightSizeSquared, float z) {
		if (id.mapType != LightingTilemapCollider2D.MapType.SuperTilemapEditor) {
			return;
		}

		if (id.colliderType != LightingTilemapCollider2D.ColliderType.Tile) {
			return;
		}

		if (id.map == null) {
			return;
		}

		SetupLocation(buffer, id);

		Vector3 rot2 = Math2D.GetPitchYawRollRad(id.transform.rotation);
		float rotationXScale = Mathf.Sin(rot2.y + Mathf.PI / 2);
		float rotationYScale = Mathf.Sin(rot2.x + Mathf.PI / 2);

		posScale.x = id.cellSize.x * id.transform.lossyScale.x * rotationXScale;
		posScale.y = id.cellSize.y * id.transform.lossyScale.y * rotationYScale;

		scale.x = id.transform.lossyScale.x;
		scale.y = id.transform.lossyScale.y;

        //  for(int x = newPositionInt.x - sizeInt; x < newPositionInt.x + sizeInt; x++) {
		//  for(int y = newPositionInt.y - sizeInt; y < newPositionInt.y + sizeInt; y++) {

        foreach(LightingTilemapCollider2D.RectangleTile rectTile in id.rectangleMap.mapTiles) {
            LightingTile tile = rectTile.tile;
            int x = rectTile.position.x;
            int y = rectTile.position.y;
        
            polygons = tile.GetPolygons(id);

            if (polygons == null || polygons.Count < 1) {
                continue;
            }

            polyOffset.x = (x + tilemapOffset.x) * posScale.x;
            polyOffset.y = (y + tilemapOffset.y) * posScale.y;
    
            polyOffset2.x = (float)polyOffset.x;
            polyOffset2.y = (float)polyOffset.y;

            if (LightingManager2D.culling && tile.InRange(polyOffset2, buffer.lightSource.transform.position, 2 + buffer.lightSource.lightSize)) {
                LightingDebug.culled ++;
                continue;
           }

            /*
            if (x-1 > 0 && y-1 > 0 && x + 1 < id.area.size.x && y + 1 < id.area.size.y) {
                if (polyOffset2.x > buffer.lightSource.transform.position.x && polyOffset2.y > buffer.lightSource.transform.position.y) {
                    LightingTile tileA = id.map[x-1, y];
                    LightingTile tileB = id.map[x, y-1];
                    LightingTile tileC = id.map[x-1, y-1];
                    if (tileA != null && tileB != null && tileC != null) {
                        continue;
                    }
                } else if (polyOffset2.x < buffer.lightSource.transform.position.x && polyOffset2.y > buffer.lightSource.transform.position.y) {
                    LightingTile tileA = id.map[x+1, y];
                    LightingTile tileB = id.map[x, y-1];
                    LightingTile tileC = id.map[x+1, y-1];
                    if (tileA != null && tileB != null && tileC != null) {
                        continue;
                    }
                } else if (polyOffset2.x > buffer.lightSource.transform.position.x && polyOffset2.y < buffer.lightSource.transform.position.y) {
                    LightingTile tileA = id.map[x-1, y];
                    LightingTile tileB = id.map[x, y+1];
                    LightingTile tileC = id.map[x-1, y+1];
                    if (tileA != null && tileB != null && tileC != null) {
                        continue;
                    }
                } else if (polyOffset2.x < buffer.lightSource.transform.position.x && polyOffset2.y < buffer.lightSource.transform.position.y) {
                    LightingTile tileA = id.map[x+1, y];
                    LightingTile tileB = id.map[x, y+1];
                    LightingTile tileC = id.map[x+1, y+1];
                    if (tileA != null && tileB != null && tileC != null) {
                        continue;
                    }
                }
            }*/

            polyOffset.x += offset.x;
            polyOffset.y += offset.y;

            polygonPairs = tile.GetPairs(id);

            LightingBufferShadow.Draw(buffer, polygons, polygonPairs, lightSizeSquared, z, polyOffset, scale);
			
		}
	}





    
	public class WithoutAtlas {
		static public void MaskSprite(LightingBuffer2D buffer, LightingTilemapCollider2D id, Material materialA, float z) {
			if (id.mapType != LightingTilemapCollider2D.MapType.SuperTilemapEditor) {
				return;
			}

			if (id.maskType != LightingTilemapCollider2D.MaskType.Sprite) {
				return;
			}

			if (id.map == null) {
				return;
			}

			Material material = materialA;

			SetupLocation(buffer, id);

			Vector3 rot2 = Math2D.GetPitchYawRollRad(id.transform.rotation);
			float rotationXScale = Mathf.Sin(rot2.y + Mathf.PI / 2);
			float rotationYScale = Mathf.Sin(rot2.x + Mathf.PI / 2);

			posScale.x = id.cellSize.x * id.transform.lossyScale.x * rotationXScale;
			posScale.y = id.cellSize.y * id.transform.lossyScale.y * rotationYScale;

			scale.x = id.transform.lossyScale.x;
			scale.y = id.transform.lossyScale.x;

			bool maskEffect = (buffer.lightSource.layerSetting[0].effect == LightingLayerEffect.InvisibleBellow);
			bool invisible = (id.maskMode == LightingMaskMode.Invisible);

			offset.x = -buffer.lightSource.transform.position.x;
			offset.y = -buffer.lightSource.transform.position.y;

			foreach(LightingTilemapCollider2D.RectangleTile rectTile in id.rectangleMap.mapTiles) {
                LightingTile tile = rectTile.tile;
                int x = rectTile.position.x;
                int y = rectTile.position.y; 

                polyOffset.x = x + tilemapOffset.x;
                polyOffset.y = y + tilemapOffset.y;

                polyOffset.x *= posScale.x;
                polyOffset.y *= posScale.y;

                polyOffset2.x = (float)polyOffset.x;
                polyOffset2.y = (float)polyOffset.y;
                
                if (LightingManager2D.culling && tile.InRange(polyOffset2, buffer.lightSource.transform.position, 2 + buffer.lightSource.lightSize)) {
                    LightingDebug.culled ++;
                    continue;
                }

                polyOffset.x += offset.x;
                polyOffset.y += offset.y;

                polyOffset2.x = (float)polyOffset.x;
                polyOffset2.y = (float)polyOffset.y;


                // material.mainTexture = id.tilemapSTE.Tileset.AtlasTexture;

                LightingGraphics.WithoutAtlas.DrawTextureRect(material, polyOffset2, scale / 2, 0, rectTile.uv, z);
                
                material.mainTexture = null;

                LightingDebug.maskGenerations ++;
            }	
		}
	}

	static public void MaskShape(LightingBuffer2D buffer, LightingTilemapCollider2D id, float z) {
		if (id.mapType != LightingTilemapCollider2D.MapType.SuperTilemapEditor) {
			return;
		}
		
		if (false == (id.maskType == LightingTilemapCollider2D.MaskType.SpriteCustomPhysicsShape || id.maskType == LightingTilemapCollider2D.MaskType.Tile)) {
			return;
		}

		if (id.map == null) {
			return;
		}

	

		SetupLocation(buffer, id);

		Vector2 vecA, vecB, vecC;

		tileMesh = null;	

		int triangleCount;

		Vector3 rot2 = Math2D.GetPitchYawRollRad(id.transform.rotation);
		float rotationXScale = Mathf.Sin(rot2.y + Mathf.PI / 2);
		float rotationYScale = Mathf.Sin(rot2.x + Mathf.PI / 2);
		
		posScale.x = id.cellSize.x * id.transform.lossyScale.x * rotationXScale;
		posScale.y = id.cellSize.y * id.transform.lossyScale.y * rotationYScale;

		if (id.maskType == LightingTilemapCollider2D.MaskType.SpriteCustomPhysicsShape) {
			scale2D.x = id.transform.lossyScale.x;
			scale2D.y = id.transform.lossyScale.x;
		} else {
			scale2D.x = id.cellSize.x * id.transform.lossyScale.x;
			scale2D.y = id.cellSize.y * id.transform.lossyScale.x;
		}

		if (id.maskType == LightingTilemapCollider2D.MaskType.Tile) {
			tileMesh = LightingTile.GetStaticTileMesh(id);
		}

		GL.Color(Color.white);

		offset.x = -buffer.lightSource.transform.position.x;
		offset.y = -buffer.lightSource.transform.position.y;

		foreach(LightingTilemapCollider2D.RectangleTile rectTile in id.rectangleMap.mapTiles) {
            LightingTile tile = rectTile.tile;
            int x = rectTile.position.x;
            int y = rectTile.position.y;
    
            polyOffset.x = x + tilemapOffset.x;
            polyOffset.y = y + tilemapOffset.y;

            polyOffset.x *= posScale.x;
            polyOffset.y *= posScale.y;

            polyOffset2.x = (float)polyOffset.x;
            polyOffset2.y = (float)polyOffset.y;

            if (LightingManager2D.culling && tile.InRange(polyOffset2, buffer.lightSource.transform.position, 2 + buffer.lightSource.lightSize)) {
                LightingDebug.culled ++;
                continue;
            }

            polyOffset.x += offset.x;
            polyOffset.y += offset.y;

            if (id.maskType == LightingTilemapCollider2D.MaskType.SpriteCustomPhysicsShape) {
                tileMesh = null;
                tileMesh = tile.GetTileDynamicMesh();
            }

            if (tileMesh == null) {
                continue;
            }

            polyOffset2.x = (float)polyOffset.x;
            polyOffset2.y = (float)polyOffset.y;

            // Batch and Optimize???
            triangleCount = tileMesh.triangles.GetLength (0);
            for (int i = 0; i < triangleCount; i = i + 3) {
                vecA = tileMesh.vertices [tileMesh.triangles [i]];
                vecB = tileMesh.vertices [tileMesh.triangles [i + 1]];
                vecC = tileMesh.vertices [tileMesh.triangles [i + 2]];

                Max2DMatrix.DrawTriangle(vecA, vecB, vecC, polyOffset2, z, scale2D);
            }

            LightingDebug.maskGenerations ++;				
		}
	}
	















	static public void SetupLocation(LightingBuffer2D buffer, LightingTilemapCollider2D id) {
		sizeInt = LightTilemapSize(id, buffer);

		LightTilemapOffset(id, buffer);
		
		offset.x = -buffer.lightSource.transform.position.x;
		offset.y = -buffer.lightSource.transform.position.y;

		tilemapOffset.x = id.transform.position.x + id.area.position.x + id.cellAnchor.x;
		tilemapOffset.y = id.transform.position.y + id.area.position.y + id.cellAnchor.y;

		if (id.mapType == LightingTilemapCollider2D.MapType.SuperTilemapEditor) {
			tilemapOffset.x -= id.area.size.x / 2;
			tilemapOffset.y -= id.area.size.y / 2;
		}
	}

	static public int LightTilemapSize(LightingTilemapCollider2D id, LightingBuffer2D buffer) {
		Vector3 rot = Math2D.GetPitchYawRollRad(id.transform.rotation);

		float rotationXScale = Mathf.Sin(rot.y + Mathf.PI / 2);
		float rotationYScale = Mathf.Sin(rot.x + Mathf.PI / 2);
	
		float sx = 1f;
		sx /= id.cellSize.x;
		sx /= id.transform.lossyScale.x;
		sx /= rotationXScale;

		float sy = 1f;
		sy /= id.cellSize.y;
		sy /= id.transform.localScale.y;
		sy /= rotationYScale;

		float size = buffer.lightSource.lightSize + 1;
		size *= Mathf.Max(sx, sy);

		if (id.mapType == LightingTilemapCollider2D.MapType.SuperTilemapEditor) {
			return((int) size);
		} else {
			return((int) size);
		}
	}

	static public void LightTilemapOffset(LightingTilemapCollider2D id, LightingBuffer2D buffer) {
		Vector2 newPosition = Vector2.zero;
		newPosition.x = buffer.lightSource.transform.position.x;
		newPosition.y = buffer.lightSource.transform.position.y;

		Vector3 rot = Math2D.GetPitchYawRollRad(id.transform.rotation);

		float rotationXScale = Mathf.Sin(rot.y + Mathf.PI / 2);
		float rotationYScale = Mathf.Sin(rot.x + Mathf.PI / 2);
	

		float sx = 1; 
		sx /= id.cellSize.x;
		sx /= id.transform.lossyScale.x;
		sx /= rotationXScale;


		float sy = 1;
		sy /= id.cellSize.y;
		sy /= id.transform.lossyScale.y;
		sy /= rotationYScale;


		newPosition.x *= sx;
		newPosition.y *= sy;

		Vector2 tilemapPosition = Vector2.zero;

		tilemapPosition.x -= id.area.position.x;
		tilemapPosition.y -= id.area.position.y;
		
		tilemapPosition.x -= id.transform.position.x;
		tilemapPosition.y -= id.transform.position.y;
			
		tilemapPosition.x -= id.cellAnchor.x;
		tilemapPosition.y -= id.cellAnchor.y;

		// Cell Size Is Not Calculated Correctly

		if (id.mapType == LightingTilemapCollider2D.MapType.SuperTilemapEditor) {
			tilemapPosition.x += id.area.size.x / 2;
			tilemapPosition.y += id.area.size.y / 2;
		} else {
			tilemapPosition.x += 1;
			tilemapPosition.y += 1;
		}

		newPosition.x += tilemapPosition.x;
		newPosition.y += tilemapPosition.y;

		newPositionInt.x = (int)newPosition.x;
		newPositionInt.y = (int)newPosition.y;
	}
    
}
