using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingBufferShadow {
	static Vector2D vA = Vector2D.Zero(), pA = Vector2D.Zero(), vB = Vector2D.Zero(), pB = Vector2D.Zero(), vC = Vector2D.Zero(), vD = Vector2D.Zero();
	static Vector2D mA = Vector2D.Zero();
	static public Vector2D inverseOffset = Vector2D.Zero();

	static public bool penumbra = false;
	static public bool drawAbove = false;

	static float uvRectX = 0;
	static float uvRectY = 0;
	static float uvRectWidth = 0;
	static float uvRectHeight = 0;

	static float angleA, angleB;
	static double rot;
			
	static List<Polygon2D> polygons = null;
	static List<List<Pair2D>> polygonPairs = null;
	static List<Pair2D> pairList = null;
	static Pair2D p;

	public class LWRP {
		public static void Draw(LightingBuffer2D buffer, List<Polygon2D> polygons, List<List<Pair2D>> polygonPairs, float z, Vector2D offset, Vector2 scale, BufferShadowRenderer shadowRenderer) {
			uvRectX = Penumbra.uvRect.x;
			uvRectY =  Penumbra.uvRect.y;
			uvRectWidth = Penumbra.uvRect.width;
			uvRectHeight = Penumbra.uvRect.height;

			inverseOffset.x = -offset.x;
			inverseOffset.y = -offset.y;

			penumbra = true;

			FillBlack.Calculate();
		
			float lightSizeSquared = Mathf.Sqrt(buffer.lightSource.lightSize * buffer.lightSource.lightSize + buffer.lightSource.lightSize * buffer.lightSource.lightSize); /// Not Finished

			for(int i = 0; i < polygons.Count; i++) {

				if (drawAbove && polygons[i].PointInPoly (inverseOffset)) {
					continue;
				}

				LightingDebug.shadowGenerations ++;

				pairList = polygonPairs[i];

				if (penumbra) {
					for(int x = 0; x < pairList.Count; x++) {
						p = pairList[x];
						
						vA.x = p.A.x * scale.x + offset.x;
						vA.y = p.A.y * scale.y + offset.y;

						pA.x = p.A.x * scale.x + offset.x;
						pA.y = p.A.y * scale.y + offset.y;

						vB.x = p.B.x * scale.x + offset.x;
						vB.y = p.B.y * scale.y + offset.y;

						pB.x = p.B.x * scale.x + offset.x;
						pB.y = p.B.y * scale.y + offset.y;

						vC.x = p.A.x * scale.x + offset.x;
						vC.y = p.A.y * scale.y + offset.y;

						vD.x = p.B.x * scale.x + offset.x;
						vD.y = p.B.y * scale.y + offset.y;

						angleA = (float)System.Math.Atan2 (vA.y, vA.x);
						angleB = (float)System.Math.Atan2 (vB.y, vB.x);

						vA.x += System.Math.Cos(angleA) * lightSizeSquared;
						vA.y += System.Math.Sin(angleA) * lightSizeSquared;

						vB.x += System.Math.Cos(angleB) * lightSizeSquared;
						vB.y += System.Math.Sin(angleB) * lightSizeSquared;

						rot = angleA - Mathf.Deg2Rad * buffer.lightSource.occlusionSize;
						pA.x += System.Math.Cos(rot) * lightSizeSquared;
						pA.y += System.Math.Sin(rot) * lightSizeSquared;

						rot = angleB + Mathf.Deg2Rad * buffer.lightSource.occlusionSize;
						pB.x += System.Math.Cos(rot) * lightSizeSquared;
						pB.y += System.Math.Sin(rot) * lightSizeSquared;
	
						shadowRenderer.verticesList.Add(new Vector3((float)vC.x, (float)vC.y, 0));
						shadowRenderer.verticesList.Add(new Vector3((float)vA.x, (float)vA.y, 0));
						shadowRenderer.verticesList.Add(new Vector3((float)pA.x, (float)pA.y, 0));

						shadowRenderer.verticesList.Add(new Vector3((float)vD.x, (float)vD.y, 0));
						shadowRenderer.verticesList.Add(new Vector3((float)vB.x, (float)vB.y, 0));
						shadowRenderer.verticesList.Add(new Vector3((float)pB.x, (float)pB.y, 0));

						shadowRenderer.uvList.Add(new Vector2(uvRectX, uvRectY));
						shadowRenderer.uvList.Add(new Vector2(uvRectWidth, uvRectY));
						shadowRenderer.uvList.Add(new Vector2(uvRectX, uvRectHeight));

						shadowRenderer.uvList.Add(new Vector2(uvRectX, uvRectY));
						shadowRenderer.uvList.Add(new Vector2(uvRectWidth, uvRectY));
						shadowRenderer.uvList.Add(new Vector2(uvRectX, uvRectHeight));

						shadowRenderer.trianglesList.Add(0 + shadowRenderer.triangleCount);
						shadowRenderer.trianglesList.Add(1 + shadowRenderer.triangleCount);
						shadowRenderer.trianglesList.Add(2 + shadowRenderer.triangleCount);

						shadowRenderer.trianglesList.Add(3 + shadowRenderer.triangleCount);
						shadowRenderer.trianglesList.Add(4 + shadowRenderer.triangleCount);
						shadowRenderer.trianglesList.Add(5 + shadowRenderer.triangleCount);
						
						shadowRenderer.triangleCount += 6;
					}
				}
				
				for(int x = 0; x < pairList.Count; x++) {
					p = pairList[x];

					vA.x = p.A.x * scale.x + offset.x;
					vA.y = p.A.y * scale.y + offset.y;

					vB.x = p.B.x * scale.x + offset.x;
					vB.y = p.B.y * scale.y + offset.y;

					vC.x = p.A.x * scale.x + offset.x;
					vC.y = p.A.y * scale.y + offset.y;

					vD.x = p.B.x * scale.x + offset.x;
					vD.y = p.B.y * scale.y + offset.y;
					
					rot = System.Math.Atan2 (vA.y, vA.x);
					vA.x += System.Math.Cos(rot) * lightSizeSquared;
					vA.y += System.Math.Sin(rot) * lightSizeSquared;

					rot = System.Math.Atan2 (vB.y, vB.x);
					vB.x += System.Math.Cos(rot) * lightSizeSquared;
					vB.y += System.Math.Sin(rot) * lightSizeSquared;

					// Detailed Shadow
					mA.x = (vC.x + vD.x) / 2;
					mA.y = (vC.y + vD.y) / 2;
					rot = System.Math.Atan2 (mA.y, mA.x);
					mA.x += System.Math.Cos(rot) * lightSizeSquared;
					mA.y += System.Math.Sin(rot) * lightSizeSquared;

					shadowRenderer.verticesList.Add(new Vector3((float)vA.x, (float)vA.y, 0));
					shadowRenderer.verticesList.Add(new Vector3((float)vB.x, (float)vB.y, 0));
					shadowRenderer.verticesList.Add(new Vector3((float)vC.x, (float)vC.y, 0));

					shadowRenderer.verticesList.Add(new Vector3((float)vB.x, (float)vB.y, 0));
					shadowRenderer.verticesList.Add(new Vector3((float)vD.x, (float)vD.y, 0));
					shadowRenderer.verticesList.Add(new Vector3((float)vC.x, (float)vC.y, 0));

					shadowRenderer.uvList.Add(new Vector2(Max2DMatrix.c_x, Max2DMatrix.c_y));
					shadowRenderer.uvList.Add(new Vector2(Max2DMatrix.c_x, Max2DMatrix.c_y));
					shadowRenderer.uvList.Add(new Vector2(Max2DMatrix.c_x, Max2DMatrix.c_y));

					shadowRenderer.uvList.Add(new Vector2(Max2DMatrix.c_x, Max2DMatrix.c_y));
					shadowRenderer.uvList.Add(new Vector2(Max2DMatrix.c_x, Max2DMatrix.c_y));
					shadowRenderer.uvList.Add(new Vector2(Max2DMatrix.c_x, Max2DMatrix.c_y));

					shadowRenderer.trianglesList.Add(0 + shadowRenderer.triangleCount);
					shadowRenderer.trianglesList.Add(1 + shadowRenderer.triangleCount);
					shadowRenderer.trianglesList.Add(2 + shadowRenderer.triangleCount);

					shadowRenderer.trianglesList.Add(3 + shadowRenderer.triangleCount);
					shadowRenderer.trianglesList.Add(4 + shadowRenderer.triangleCount);
					shadowRenderer.trianglesList.Add(5 + shadowRenderer.triangleCount);

					shadowRenderer.triangleCount += 6;
				}
			}
		}
	}

	public static void Draw(LightingBuffer2D buffer, List<Polygon2D> polygons, List<List<Pair2D>> polygonPairs, float lightSizeSquared, float z, Vector2D offset, Vector2 scale) {
		uvRectX = Penumbra.uvRect.x;
		uvRectY =  Penumbra.uvRect.y;
		uvRectWidth = Penumbra.uvRect.width;
		uvRectHeight = Penumbra.uvRect.height;

		inverseOffset.x = -offset.x;
		inverseOffset.y = -offset.y;

		for(int i = 0; i < polygons.Count; i++) {

			if (drawAbove && polygons[i].PointInPoly (inverseOffset)) {
				continue;
			}

			LightingDebug.shadowGenerations ++;

			pairList = polygonPairs[i];
			
			if (penumbra) {
				GL.Color(Color.white);
				for(int x = 0; x < pairList.Count; x++) {
					p = pairList[x];
					
					vA.x = p.A.x * scale.x + offset.x;
					vA.y = p.A.y * scale.y + offset.y;

					pA.x = p.A.x * scale.x + offset.x;
					pA.y = p.A.y * scale.y + offset.y;

					vB.x = p.B.x * scale.x + offset.x;
					vB.y = p.B.y * scale.y + offset.y;

					pB.x = p.B.x * scale.x + offset.x;
					pB.y = p.B.y * scale.y + offset.y;

					vC.x = p.A.x * scale.x + offset.x;
					vC.y = p.A.y * scale.y + offset.y;

					vD.x = p.B.x * scale.x + offset.x;
					vD.y = p.B.y * scale.y + offset.y;

					angleA = (float)System.Math.Atan2 (vA.y, vA.x);
					angleB = (float)System.Math.Atan2 (vB.y, vB.x);

					vA.x += System.Math.Cos(angleA) * lightSizeSquared;
					vA.y += System.Math.Sin(angleA) * lightSizeSquared;

					vB.x += System.Math.Cos(angleB) * lightSizeSquared;
					vB.y += System.Math.Sin(angleB) * lightSizeSquared;

					rot = angleA - Mathf.Deg2Rad * buffer.lightSource.occlusionSize;
					pA.x += System.Math.Cos(rot) * lightSizeSquared;
					pA.y += System.Math.Sin(rot) * lightSizeSquared;

					rot = angleB + Mathf.Deg2Rad * buffer.lightSource.occlusionSize;
					pB.x += System.Math.Cos(rot) * lightSizeSquared;
					pB.y += System.Math.Sin(rot) * lightSizeSquared;

					GL.TexCoord3(uvRectX, uvRectY, 0);
					GL.Vertex3((float)vC.x,(float)vC.y, z);

					GL.TexCoord3(uvRectWidth, uvRectY, 0);
					GL.Vertex3((float)pA.x, (float)pA.y, z);
					
					GL.TexCoord3(uvRectX, uvRectHeight, 0);
					GL.Vertex3((float)vA.x,(float)vA.y, z);
					
					
					GL.TexCoord3(uvRectX, uvRectY, 0);
					GL.Vertex3((float)vD.x,(float)vD.y, z);

					GL.TexCoord3(uvRectWidth, uvRectY, 0);
					GL.Vertex3((float)pB.x, (float)pB.y, z);
					
					GL.TexCoord3(uvRectX, uvRectHeight, 0);
					GL.Vertex3((float)vB.x, (float)vB.y, z);
				}
			}
			
			GL.Color(Color.black);
			for(int x = 0; x < pairList.Count; x++) {
				p = pairList[x];

				vA.x = p.A.x * scale.x + offset.x;
				vA.y = p.A.y * scale.y + offset.y;

				vB.x = p.B.x * scale.x + offset.x;
				vB.y = p.B.y * scale.y + offset.y;

				vC.x = p.A.x * scale.x + offset.x;
				vC.y = p.A.y * scale.y + offset.y;

				vD.x = p.B.x * scale.x + offset.x;
				vD.y = p.B.y * scale.y + offset.y;
				
				rot = System.Math.Atan2 (vA.y, vA.x);
				vA.x += System.Math.Cos(rot) * lightSizeSquared;
				vA.y += System.Math.Sin(rot) * lightSizeSquared;

				rot = System.Math.Atan2 (vB.y, vB.x);
				vB.x += System.Math.Cos(rot) * lightSizeSquared;
				vB.y += System.Math.Sin(rot) * lightSizeSquared;

				// Detailed Shadow
				mA.x = (vC.x + vD.x) / 2;
				mA.y = (vC.y + vD.y) / 2;
				rot = System.Math.Atan2 (mA.y, mA.x);
				mA.x += System.Math.Cos(rot) * lightSizeSquared;
				mA.y += System.Math.Sin(rot) * lightSizeSquared;

				if (FillWhite.highQuality) {
					GL.TexCoord3(Max2DMatrix.c_x, Max2DMatrix.c_y, 0);
					GL.Vertex3((float)vC.x, (float)vC.y, z);

					GL.TexCoord3(Max2DMatrix.c_x, Max2DMatrix.c_y, 0);
					GL.Vertex3((float)vD.x, (float)vD.y, z);

					GL.TexCoord3(Max2DMatrix.c_x, Max2DMatrix.c_y, 0);
					GL.Vertex3((float)mA.x, (float)mA.y, z);

					// Regular Shadow
					GL.TexCoord3(Max2DMatrix.c_x, Max2DMatrix.c_y, 0);
					GL.Vertex3((float)vC.x, (float)vC.y, z);

					GL.TexCoord3(Max2DMatrix.c_x, Max2DMatrix.c_y, 0);
					GL.Vertex3((float)mA.x, (float)mA.y, z);

					GL.TexCoord3(Max2DMatrix.c_x, Max2DMatrix.c_y, 0);
					GL.Vertex3((float)vA.x, (float)vA.y, z);



					GL.TexCoord3(Max2DMatrix.c_x, Max2DMatrix.c_y, 0);
					GL.Vertex3((float)mA.x, (float)mA.y, z);

					GL.TexCoord3(Max2DMatrix.c_x, Max2DMatrix.c_y, 0);
					GL.Vertex3((float)vB.x, (float)vB.y, z);

					GL.TexCoord3(Max2DMatrix.c_x, Max2DMatrix.c_y, 0);
					GL.Vertex3((float)vD.x, (float)vD.y, z);
					
				} else {
					GL.TexCoord3(Max2DMatrix.c_x, Max2DMatrix.c_y, 0);
					GL.Vertex3((float)vA.x, (float)vA.y, z);

					GL.TexCoord3(Max2DMatrix.c_x, Max2DMatrix.c_y, 0);
					GL.Vertex3((float)vB.x, (float)vB.y, z);

					GL.TexCoord3(Max2DMatrix.c_x, Max2DMatrix.c_y, 0);
					GL.Vertex3((float)vC.x, (float)vC.y, z);


					GL.TexCoord3(Max2DMatrix.c_x, Max2DMatrix.c_y, 0);
					GL.Vertex3((float)vB.x, (float)vB.y, z);

					GL.TexCoord3(Max2DMatrix.c_x, Max2DMatrix.c_y, 0);
					GL.Vertex3((float)vD.x, (float)vD.y, z);

					GL.TexCoord3(Max2DMatrix.c_x, Max2DMatrix.c_y, 0);
					GL.Vertex3((float)vC.x, (float)vC.y, z);
				}
			}
		}
	}

    public class FillWhite {
		static public Rect uvRect = new Rect();
		static public bool highQuality = true;

		static public void Calculate() {
			LightingManager2D manager = LightingManager2D.Get();
			
			Sprite fillSprite = manager.materials.GetAtlasWhiteMaskSprite();
		
			highQuality = Lighting2D.commonSettings.drawHighQualityShadows;

			if (fillSprite != null) {
				uvRect.x = (float)fillSprite.rect.x / fillSprite.texture.width;
				uvRect.y = (float)fillSprite.rect.y / fillSprite.texture.height;
				uvRect.width = (float)fillSprite.rect.width / fillSprite.texture.width;
				uvRect.height = (float)fillSprite.rect.height / fillSprite.texture.height;

				uvRect.x += uvRect.width / 2;
				uvRect.y += uvRect.height / 2;
				
				Max2DMatrix.c_x = uvRect.x;
				Max2DMatrix.c_y = uvRect.y;
			}
		}
	}

	public class FillBlack {
		static public Rect uvRect = new Rect();
		static public bool highQuality = true;

		static public void Calculate() {
			LightingManager2D manager = LightingManager2D.Get();
			
			Sprite fillSprite = manager.materials.GetAtlasBlackMaskSprite();
		
			highQuality = Lighting2D.commonSettings.drawHighQualityShadows;

			if (fillSprite != null) {
				uvRect.x = (float)fillSprite.rect.x / fillSprite.texture.width;
				uvRect.y = (float)fillSprite.rect.y / fillSprite.texture.height;
				uvRect.width = (float)fillSprite.rect.width / fillSprite.texture.width;
				uvRect.height = (float)fillSprite.rect.height / fillSprite.texture.height;

				uvRect.x += uvRect.width / 2;
				uvRect.y += uvRect.height / 2;
				
				Max2DMatrix.c_x = uvRect.x;
				Max2DMatrix.c_y = uvRect.y;
			}
		}
	}

	static public class Penumbra {
		static public Rect uvRect = new Rect();
		static Sprite sprite = null;

		public static void Calculate() {
			LightingManager2D manager = LightingManager2D.Get();
			
			sprite = manager.materials.GetAtlasPenumbraSprite();

			if (sprite == null || sprite.texture == null) {
				return;
			}		

			uvRect.x = sprite.rect.x / sprite.texture.width;
			uvRect.y = sprite.rect.y / sprite.texture.height;
			uvRect.width = sprite.rect.width / sprite.texture.width;
			uvRect.height = sprite.rect.height / sprite.texture.height;

			uvRect.width += uvRect.x;
			uvRect.height += uvRect.y;

			int atlasSize = SpriteAtlasManager.GetAtlasPage().atlasSize / 2;

			uvRect.x += 1f / atlasSize;
			uvRect.y += 1f / atlasSize;
			uvRect.width -= 1f / atlasSize;
			uvRect.height -= 1f / atlasSize;
		}
	}

    static public class Setup {
        public static void Calculate(LightingBuffer2D buffer) {	
            penumbra = Lighting2D.commonSettings.drawPenumbra;
            drawAbove = buffer.lightSource.whenInsideCollider == LightingSource2D.WhenInsideCollider.DrawAbove;
        }
    }
}