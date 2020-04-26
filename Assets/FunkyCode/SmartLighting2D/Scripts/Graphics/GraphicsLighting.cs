using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingGraphics {
	
	static Rect uvRect = new Rect();
	static Vector2 scale = new Vector2();
	static Vector2 pos1 = new Vector2();
	static Vector2 pos2 = new Vector2();
	static Vector2 pos3 = new Vector2();
	static Vector2 pos4 = new Vector2();
	static Color color = Color.white;

	static VirtualSpriteRenderer virtualSpriteRenderer = new VirtualSpriteRenderer();
	
	public static Mesh preRenderMesh = null;
	public static Vector2[] meshUV = null;

	public static Mesh GetRenderMesh() {
		if (preRenderMesh == null) {
			Mesh mesh = new Mesh();

			mesh.vertices = new Vector3[]{new Vector3(-1, -1), new Vector3(1, -1), new Vector3(1, 1), new Vector3(-1, 1)};
			mesh.triangles = new int[]{0, 1, 2, 2, 3, 0};
			mesh.uv = new Vector2[]{new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1)};
			meshUV = mesh.uv;

			preRenderMesh = mesh;
		}
		return(preRenderMesh);
	}

	public class WithoutAtlas {

		static public void DrawTexture(Material material, Vector2 pos, Vector2 size, float rotation, float z = 0f) {
			if (material.mainTexture == null) {
				return;
			}

			Mesh mesh = GetRenderMesh();
			meshUV[0].x = 0;
			meshUV[0].y = 0;

			meshUV[1].x = 1;
			meshUV[1].y = 0;

			meshUV[2].x = 1;
			meshUV[2].y = 1;

			meshUV[3].x = 0;
			meshUV[3].y = 1;
			mesh.uv = meshUV;

			Vector3 matrixPosition = Vector3.zero;
			matrixPosition.z = z;
			matrixPosition.x = pos.x;
			matrixPosition.y = pos.y;

			Quaternion matrixRotation = Quaternion.Euler(0, 0, rotation);

			Vector3 matrixScale = new Vector3(1, 1, 1);
			matrixScale.x = size.x;
			matrixScale.y = size.y;

			Matrix4x4 matrix = Matrix4x4.TRS(matrixPosition, matrixRotation, matrixScale);

			material.SetPass (0); 

			Graphics.DrawMeshNow(mesh, matrix);

			GL.End (); 
		}

		static public void DrawTextureRect(Material material, Vector2 pos, Vector2 size, float rotation, Rect rect, float z = 0f) {
			if (material.mainTexture == null) {
				return;
			}

			Mesh mesh = GetRenderMesh();
			meshUV[0].x = rect.x;
			meshUV[0].y = rect.y;

			meshUV[1].x = rect.x + rect.width;
			meshUV[1].y = rect.y;

			meshUV[2].x = rect.x + rect.width;
			meshUV[2].y = rect.y + rect.height;

			meshUV[3].x = rect.x;
			meshUV[3].y = rect.y + rect.height;
			mesh.uv = meshUV;

			Vector3 matrixPosition = Vector3.zero;
			matrixPosition.z = z;
			matrixPosition.x = pos.x;
			matrixPosition.y = pos.y;

			Quaternion matrixRotation = Quaternion.Euler(0, 0, rotation);

			Vector3 matrixScale = new Vector3(1, 1, 1);
			matrixScale.x = size.x;
			matrixScale.y = size.y;

			Matrix4x4 matrix = Matrix4x4.TRS(matrixPosition, matrixRotation, matrixScale);

			material.SetPass (0); 

			Graphics.DrawMeshNow(mesh, matrix);

			GL.End (); 
		}

		static public void DrawSprite(Material material, SpriteRenderer spriteRenderer, Vector2 pos, Vector2 size, float rot, float z = 0f) {
			virtualSpriteRenderer.Set(spriteRenderer);
			
			DrawSprite(material, virtualSpriteRenderer, pos, size, rot, z);
		}

		static public void DrawSprite(Material material, VirtualSpriteRenderer spriteRenderer, Vector2 pos, Vector2 size, float spriteRot, float z = 0f) {
			Sprite sprite = spriteRenderer.sprite;
			if (spriteRenderer == null || sprite == null || sprite.texture == null) {
				return;
			}

			Rect rect = sprite.rect;
			
			uvRect.x = rect.x / sprite.texture.width;
			uvRect.y = rect.y / sprite.texture.height;
			uvRect.width = rect.width / sprite.texture.width + uvRect.x;
			uvRect.height = rect.height / sprite.texture.height + uvRect.y;

			// Vertex Position Calculation
			scale.x = (float)sprite.texture.width / sprite.rect.width;
			scale.y = (float)sprite.texture.height / sprite.rect.height;

			size.x /= scale.x;
			size.y /= scale.y;

			size.x *= (float)sprite.texture.width / (sprite.pixelsPerUnit * 2);
			size.y *= (float)sprite.texture.height / (sprite.pixelsPerUnit * 2);

			if (spriteRenderer.flipX) {
				size.x = -size.x;
			}

			if (spriteRenderer.flipY) {
				size.y = -size.y;
			}

			float rectAngle = Mathf.Atan2(size.y, size.x);
			float dist = Mathf.Sqrt(size.x * size.x + size.y * size.y);
			float rot = spriteRot * Mathf.Deg2Rad + Mathf.PI;

			// Pivot Point Calculation
			Vector2 pivot = sprite.pivot;
			pivot.x /= sprite.rect.width;
			pivot.y /= sprite.rect.height;
			pivot.x -= 0.5f;
			pivot.y -= 0.5f;

			pivot.x *= size.x * 2;
			pivot.y *= size.y * 2;

			float pivotDist = Mathf.Sqrt(pivot.x * pivot.x + pivot.y * pivot.y);
			float pivotAngle = Mathf.Atan2(pivot.y, pivot.x);
			
			pos.x += Mathf.Cos(pivotAngle + rot) * pivotDist;
			pos.y += Mathf.Sin(pivotAngle + rot) * pivotDist;

			Mesh mesh = GetRenderMesh();
			meshUV[0].x = uvRect.x;
			meshUV[0].y = uvRect.y;

			meshUV[1].x = uvRect.width;
			meshUV[1].y = uvRect.y;

			meshUV[2].x = uvRect.width;
			meshUV[2].y = uvRect.height;

			meshUV[3].x = uvRect.x;
			meshUV[3].y = uvRect.height;
			mesh.uv = meshUV;

			Vector3 matrixPosition = Vector3.zero;
			matrixPosition.z = z;
			matrixPosition.x = pos.x;
			matrixPosition.y = pos.y;

			Quaternion matrixRotation = Quaternion.Euler(0, 0, spriteRot);

			Vector3 matrixScale = new Vector3(1, 1, 1);
			matrixScale.x = size.x;
			matrixScale.y = size.y;

			Matrix4x4 matrix = Matrix4x4.TRS(matrixPosition, matrixRotation, matrixScale);

			material.SetPass (0); 
		
			//GL.Begin(0);

			Graphics.DrawMeshNow(mesh, matrix);

			GL.End (); 
		}
	}
	

	public class WithAtlas {
		////////////// Sprite Atlas Sprite
		static public void DrawSprite(VirtualSpriteRenderer spriteRenderer, LayerSetting layerSetting,  LightingMaskMode maskMode, Vector2 pos, Vector2 size, float spriteRot, float z = 0f) {
			Sprite sprite = spriteRenderer.sprite;
			if (spriteRenderer == null || sprite == null || sprite.texture == null) {
				return;
			
			}
			// UV Coordinates Calculation
			//float spriteSheetUV_X = (float)(sprite.texture.width) / sprite.rect.width;
			//float spriteSheetUV_Y = (float)(sprite.texture.height) / sprite.rect.height;

			Rect rect = sprite.rect;

			uvRect.x = rect.x / sprite.texture.width;
			uvRect.y = rect.y / sprite.texture.height;
			uvRect.width = rect.width / sprite.texture.width + uvRect.x;
			uvRect.height = rect.height / sprite.texture.height + uvRect.y;

			//uvRect.x += 1f / sprite.texture.width;
			//uvRect.y += 1f / sprite.texture.height;
			//uvRect.width -= 2f / sprite.texture.width;
			//uvRect.height -= 2f / sprite.texture.height;

			// Vertex Position Calculation
			scale.x = (float)sprite.texture.width / sprite.rect.width;
			scale.y = (float)sprite.texture.height / sprite.rect.height;

			size.x /= scale.x;
			size.y /= scale.y;

			size.x *= (float)sprite.texture.width / (sprite.pixelsPerUnit * 2);
			size.y *= (float)sprite.texture.height / (sprite.pixelsPerUnit * 2);

			if (spriteRenderer.flipX) {
				size.x = -size.x;
			}

			if (spriteRenderer.flipY) {
				size.y = -size.y;
			}

			float rectAngle = Mathf.Atan2(size.y, size.x);
			float dist = Mathf.Sqrt(size.x * size.x + size.y * size.y);
			float rot = spriteRot * Mathf.Deg2Rad + Mathf.PI;

			// Pivot Point Calculation
			Vector2 pivot = sprite.pivot;
			pivot.x /= sprite.rect.width;
			pivot.y /= sprite.rect.height;
			pivot.x -= 0.5f;
			pivot.y -= 0.5f;

			pivot.x *= size.x * 2;
			pivot.y *= size.y * 2;

			float pivotDist = Mathf.Sqrt(pivot.x * pivot.x + pivot.y * pivot.y);
			float pivotAngle = Mathf.Atan2(pivot.y, pivot.x);
			
			pos.x += Mathf.Cos(pivotAngle + rot) * pivotDist;
			pos.y += Mathf.Sin(pivotAngle + rot) * pivotDist;

			// Vertext Coordinates
			pos1.x = pos.x + Mathf.Cos(rectAngle + rot) * dist;
			pos1.y = pos.y + Mathf.Sin(rectAngle + rot) * dist;

			pos2.x = pos.x + Mathf.Cos(-rectAngle + rot) * dist;
			pos2.y = pos.y + Mathf.Sin(-rectAngle + rot) * dist;

			pos3.x = pos.x + Mathf.Cos(rectAngle + Mathf.PI + rot) * dist;
			pos3.y = pos.y + Mathf.Sin(rectAngle + Mathf.PI + rot) * dist;

			pos4.x = pos.x + Mathf.Cos(-rectAngle + Mathf.PI + rot) * dist;
			pos4.y = pos.y + Mathf.Sin(-rectAngle + Mathf.PI + rot) * dist;

			if (maskMode == LightingMaskMode.Invisible) {
				GL.Color(Color.black);
			} else if (layerSetting.effect == LightingLayerEffect.InvisibleBellow) {
				//float lowestY = v1.y;
				//lowestY = Mathf.Min(lowestY, v2.y);
				//lowestY = Mathf.Min(lowestY, v3.y);
				//lowestY = Mathf.Min(lowestY, v3.y);

				float c = pos.y / layerSetting.maskEffectDistance + layerSetting.maskEffectDistance * 2; // + 1f // pos.y 
				if (c < 0) {
					c = 0;
				}

				color.r = c;
				color.g = c;
				color.b = c;
				color.a = 1;

				GL.Color(color);
			} else {
				GL.Color(Color.white);
			}

			GL.TexCoord3 (uvRect.x, uvRect.y, 0);
			GL.Vertex3 (pos1.x, pos1.y, z);

			GL.TexCoord3 (uvRect.x, uvRect.height, 0);
			GL.Vertex3 (pos2.x, pos2.y, z);

			GL.TexCoord3 (uvRect.width, uvRect.height, 0);
			GL.Vertex3 (pos3.x, pos3.y, z);

			GL.TexCoord3 (uvRect.width, uvRect.height, 0);
			GL.Vertex3 (pos3.x, pos3.y, z);

			GL.TexCoord3 (uvRect.width, uvRect.y, 0);
			GL.Vertex3 (pos4.x, pos4.y, z);

			GL.TexCoord3 (uvRect.x, uvRect.y, 0);
			GL.Vertex3 (pos1.x, pos1.y, z);
		}

		static public void DrawSpriteDay(VirtualSpriteRenderer spriteRenderer, Vector2 pos, Vector2 size, float rot, float z = 0f) {
			Sprite sprite = spriteRenderer.sprite;

			if (sprite == null || sprite.texture == null) {
				return;
			}

			Texture texture = sprite.texture;

			int textureWidth = texture.width;
			int textureHeight = texture.height;

			Rect rect = sprite.rect;

			// UV Coordinates Calculation
			//float spriteSheetUV_X = (float)(textureWidth) / rect.width;
			//f//loat spriteSheetUV_Y = (float)(textureHeight) / rect.height;

			uvRect.x = rect.x / textureWidth;
			uvRect.y = rect.y / textureHeight;
			uvRect.width = rect.width / textureWidth + uvRect.x;
			uvRect.height = rect.height / textureHeight + uvRect.y;
		
			// Vertex Position Calculation
			scale.x = (float)textureWidth / rect.width;
			scale.y = (float)textureHeight / rect.height;

			size.x /= scale.x;
			size.y /= scale.y;

			size.x *= (float)textureWidth / (sprite.pixelsPerUnit * 2);
			size.y *= (float)textureHeight / (sprite.pixelsPerUnit * 2);

			if (spriteRenderer.flipX) {
				size.x = -size.x;
			}

			if (spriteRenderer.flipY) {
				size.y = -size.y;
			}

			float rectAngle = Mathf.Atan2(size.y, size.x);
			float dist = Mathf.Sqrt(size.x * size.x + size.y * size.y);
			rot = rot * Mathf.Deg2Rad + Mathf.PI;

			// Pivot Point Calculation
			Vector2 pivot = sprite.pivot;
			pivot.x /= rect.width;
			pivot.y /= rect.height;
			pivot.x -= 0.5f;
			pivot.y -= 0.5f;

			pivot.x *= size.x * 2;
			pivot.y *= size.y * 2;

			float pivotDist = Mathf.Sqrt(pivot.x * pivot.x + pivot.y * pivot.y);
			float pivotAngle = (float)System.Math.Atan2(pivot.y, pivot.x);
			
			pos.x += Mathf.Cos(pivotAngle + rot) * pivotDist;
			pos.y += Mathf.Sin(pivotAngle + rot) * pivotDist;

			// Vertext Coordinates
			pos1.x = pos.x + Mathf.Cos(rectAngle + rot) * dist;
			pos1.y = pos.y + Mathf.Sin(rectAngle + rot) * dist;

			pos2.x = pos.x + Mathf.Cos(-rectAngle + rot) * dist;
			pos2.y = pos.y + Mathf.Sin(-rectAngle + rot) * dist;

			pos3.x = pos.x + Mathf.Cos(rectAngle + Mathf.PI + rot) * dist;
			pos3.y = pos.y + Mathf.Sin(rectAngle + Mathf.PI + rot) * dist;

			pos4.x = pos.x + Mathf.Cos(-rectAngle + Mathf.PI + rot) * dist;
			pos4.y = pos.y + Mathf.Sin(-rectAngle + Mathf.PI + rot) * dist;

			uvRect.x += 4f / textureWidth;
			uvRect.y += 4f / textureHeight;
			uvRect.width -= 8f / textureWidth;
			uvRect.height -= 8f / textureHeight;

			GL.TexCoord3 (uvRect.x, uvRect.y, 0);
			GL.Vertex3 (pos1.x, pos1.y, z);

			GL.TexCoord3 (uvRect.x, uvRect.height, 0);
			GL.Vertex3 (pos2.x, pos2.y, z);

			GL.TexCoord3 (uvRect.width, uvRect.height, 0);
			GL.Vertex3 (pos3.x, pos3.y, z);

			GL.TexCoord3 (uvRect.width, uvRect.height, 0);
			GL.Vertex3 (pos3.x, pos3.y, z);

			GL.TexCoord3 (uvRect.width, uvRect.y, 0);
			GL.Vertex3 (pos4.x, pos4.y, z);

			GL.TexCoord3 (uvRect.x, uvRect.y, 0);
			GL.Vertex3 (pos1.x, pos1.y, z);
		}
	}
}
