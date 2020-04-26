using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayLightingTilemap {
    const float uv0 = 0;
	const float uv1 = 1;
	const float pi2 = Mathf.PI / 2;

    static LightingManager2D manager;

    static public void Shadow(Camera camera, Vector2D offset, float z) {
        #if UNITY_2018_1_OR_NEWER

        manager = LightingManager2D.Get();

		bool draw = false;

		Material materialWhite = manager.materials.GetWhiteSprite();
	
		// Day Soft Shadows
		foreach (LightingTilemapCollider2D id in LightingTilemapCollider2D.GetList()) {
			if (id.map == null) {
				continue;
			}

			if (id.dayHeight == false) {
				continue;
			}

			if (draw == false) {		
				////////////////////////////////////////////////////////////////////////////////////////Max2D.defaultMaterial.SetPass(0);

				GL.Begin(GL.TRIANGLES);
				GL.Color(Color.black);

				draw = true;
			}

			Vector2D tilesetOffset = new Vector2D(offset);
			tilesetOffset += new Vector2D(id.area.position.x, id.area.position.y);

			for(int x = 0; x < id.area.size.x; x++) {
				for(int y = 0; y < id.area.size.y; y++) {
					if (id.map[x, y] == null) {
						continue;
					}

					Vector2D tileOffset = tilesetOffset.Copy();
					tileOffset += new Vector2D(x, y); 

					TileFill(tileOffset, z, id.height);
				}
			}	
		}

		if (draw == true) {
			GL.End();
			
			draw = false;
		}

		
		// Day Soft Shadows Penumbra
		foreach (LightingTilemapCollider2D id in LightingTilemapCollider2D.GetList()) {
			if (id.map == null) {
				continue;
			}

			if (id.dayHeight == false) {
				continue;
			}

			
			if (draw == false) {		
				manager.materials.GetShadowBlur().SetPass (0);

				GL.Begin (GL.TRIANGLES);
				/////////////////////////////////////////////////////////////////////////////////Max2D.SetColor (Color.white);
				
				draw = true;
			}

			Vector2D tilesetOffset = new Vector2D(offset);
			tilesetOffset += new Vector2D(id.area.position.x, id.area.position.y);

			for(int x = 0; x < id.area.size.x; x++) {
				for(int y = 0; y < id.area.size.y; y++) {
					if (id.map[x, y] == null) {
						continue;
					}

					Vector2D tileOffset = tilesetOffset.Copy();
					tileOffset += new Vector2D(x , y); 

					TileBlur(tileOffset, z, id.height, Lighting2D.dayLightingSettings.sunPenumbra);	
				}
			}	
		}
	
		if (draw == true) {
			GL.End();
			
			draw = false;
		}

		// Tilemap Daylighting Masks
		foreach (LightingTilemapCollider2D id in LightingTilemapCollider2D.GetList()) {
			if (id.map == null) {
				continue;
			}

			if (id.dayHeight == false) {
				continue;
			}

			Vector3 rot = Math2D.GetPitchYawRollRad(id.transform.rotation);

			float rotationYScale = Mathf.Sin(rot.x + Mathf.PI / 2);
			float rotationXScale = Mathf.Sin(rot.y + Mathf.PI / 2);

			float scaleX = id.transform.lossyScale.x * rotationXScale * id.cellSize.x;
			float scaleY = id.transform.lossyScale.y * rotationYScale * id.cellSize.y;

			Vector2D tilesetOffset = new Vector2D(offset);
			tilesetOffset += new Vector2D(id.area.position.x, id.area.position.y);
			tilesetOffset += new Vector2D(id.cellAnchor.x,id.cellAnchor.y);

			for(int x = 0; x < id.area.size.x; x++) {
				for(int y = 0; y < id.area.size.y; y++) {
					LightingTile tile = id.map[x, y];
					if (tile == null) {
						continue;
					}

					if (tile.GetOriginalSprite() == null) {
						continue;
					}

					VirtualSpriteRenderer spriteRenderer = new VirtualSpriteRenderer();
					spriteRenderer.sprite = tile.GetOriginalSprite();

					materialWhite.mainTexture = tile.GetOriginalSprite().texture;

					Vector2D tileOffset = tilesetOffset.Copy();
					tileOffset += new Vector2D(x , y); 


					//LightingGraphics.WithoutAtlas.DrawSprite(materialWhite, spriteRenderer, tileOffset.ToVector2(), new Vector2(scaleX / id.cellSize.x, scaleY / id.cellSize.y), 0, z);
					
					materialWhite.mainTexture = null;				
				}
			}	
		}

		#endif
	}

	static void TileFill(Vector2D offset, float z, float height) {
		float sunDirection = LightingManager2D.GetSunDirection ();

		Polygon2D poly = Polygon2DList.CreateRect(new Vector2(1, 1) * 0.5f);
		poly = poly.ToOffset(new Vector2D(0.5f, 0.5f));

		foreach (Pair2D p in Pair2D.GetList(poly.pointsList)) {
			Vector2D vA = p.A.Copy();
			Vector2D vB = p.B.Copy();

			vA.Push (sunDirection, height);
			vB.Push (sunDirection, height);

			Max2DMatrix.DrawTriangle(p.A, p.B, vA, offset, z);
			Max2DMatrix.DrawTriangle(vA, vB, p.B, offset, z);
		}
	}

	static void TileBlur(Vector2D offset, float z, float height, float penumbra) {
		float sunDirection = LightingManager2D.GetSunDirection ();

		Polygon2D poly = Polygon2DList.CreateRect(new Vector2(1, 1) * 0.5f);
		offset += new Vector2D(0.5f, 0.5f);
		
		Polygon2D convexHull = Polygon2D.GenerateShadow(new Polygon2D(poly.pointsList), sunDirection, height);
		
		foreach (DoublePair2D p in DoublePair2D.GetList(convexHull.pointsList)) {
			Vector2D zA = new Vector2D (p.A + offset);
			Vector2D zB = new Vector2D (p.B + offset);
			Vector2D zC = zB.Copy();

			Vector2D pA = zA.Copy();
			Vector2D pB = zB.Copy();

			zA.Push (Vector2D.Atan2 (p.A, p.B) + pi2, penumbra);
			zB.Push (Vector2D.Atan2 (p.A, p.B) + pi2, penumbra);
			zC.Push (Vector2D.Atan2 (p.B, p.C) + pi2, penumbra);
			
			GL.TexCoord2 (uv0, uv0);
			Max2D.Vertex3 (pB, z);
			GL.TexCoord2 (0.5f - uv0, uv0);
			Max2D.Vertex3 (pA, z);
			GL.TexCoord2 (0.5f - uv0, uv1);
			Max2D.Vertex3 (zA, z);
		
			GL.TexCoord2 (uv0, uv1);
			Max2D.Vertex3 (zA, z);
			GL.TexCoord2 (0.5f - uv0, uv1);
			Max2D.Vertex3 (zB, z);
			GL.TexCoord2 (0.5f - uv0, uv0);
			Max2D.Vertex3 (pB, z);
			
			GL.TexCoord2 (uv0, uv1);
			Max2D.Vertex3 (zB, z);
			GL.TexCoord2 (0.5f - uv0, uv0);
			Max2D.Vertex3 (pB, z);
			GL.TexCoord2 (0.5f - uv0, uv1);
			Max2D.Vertex3 (zC, z);
		}
	}
}
