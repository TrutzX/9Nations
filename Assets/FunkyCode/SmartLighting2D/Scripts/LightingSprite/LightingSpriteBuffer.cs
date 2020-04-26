using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingSpriteBuffer {
	static List<LightingSpriteRenderer2D> spriteRendererList;
	static LightingManager2D manager;
	static VirtualSpriteRenderer spriteRenderer = new VirtualSpriteRenderer();
	static LightingSpriteRenderer2D id;
	static Material material;
	static Vector2 position, scale;
	static float rot;
	static Color color;

    public static void Draw(Camera camera, Vector2D offset, float z) {
		manager = LightingManager2D.Get();
		
		spriteRendererList = LightingSpriteRenderer2D.GetList();

		if (Lighting2D.atlasSettings.lightingSpriteAtlas) {
			WithAtlas.Draw(camera, offset, z);
		} else {
			WithoutAtlas.Draw(camera, offset, z);
		}
    }

	public class WithAtlas {

		static public void Draw(Camera camera, Vector2D offset, float z) {
			material = manager.materials.GetAdditive();
			material.SetColor ("_TintColor", Color.white);
			material.mainTexture = SpriteAtlasManager.GetAtlasPage().GetTexture();

			material.SetPass (0);

			GL.Begin (GL.TRIANGLES);
			
			for(int i = 0; i < spriteRendererList.Count; i++) {
				id = spriteRendererList[i];

				if (id.type != LightingSpriteRenderer2D.Type.Particle) {
					continue;
				}

				if (id.GetSprite() == null) {
					continue;
				}

				if (id.InCamera(camera) == false) {
					continue;
				}

				position = id.transform.position;

				scale = id.transform.lossyScale;
				scale.x *= id.offsetScale.x;
				scale.y *= id.offsetScale.y;

				rot = id.offsetRotation;
				if (id.applyTransformRotation) {
					rot += id.transform.rotation.eulerAngles.z;
				}

				color = id.color;
				color.a = id.alpha;

				GL.Color(color);

				spriteRenderer.sprite = SpriteAtlasManager.RequestSprite(id.GetSprite(), SpriteAtlasRequest.Type.Normal);
				
				LightingGraphics.WithAtlas.DrawSpriteDay(spriteRenderer, offset.ToVector2() + position + id.offsetPosition, scale, rot, z);

				LightingDebug.SpriteRenderersDrawn ++;
			}

			GL.End();

			material = manager.materials.GetAtlasMaterial();
			material.SetPass (0);

			GL.Begin (GL.TRIANGLES);
			
			for(int i = 0; i < spriteRendererList.Count; i++) {
				id = spriteRendererList[i];

				if (id.type == LightingSpriteRenderer2D.Type.Particle) {
					continue;
				}

				if (id.GetSprite() == null) {
					continue;
				}

				if (id.InCamera(camera) == false) {
					continue;
				}

				position = id.transform.position;

				scale = id.transform.lossyScale;
				scale.x *= id.offsetScale.x;
				scale.y *= id.offsetScale.y;

				rot = id.offsetRotation;
				if (id.applyTransformRotation) {
					rot += id.transform.rotation.eulerAngles.z;
				}

				switch(id.type) {
					case LightingSpriteRenderer2D.Type.WhiteMask:
						GL.Color(Color.white);

						spriteRenderer.sprite = SpriteAtlasManager.RequestSprite(id.GetSprite(), SpriteAtlasRequest.Type.WhiteMask);
						
						LightingGraphics.WithAtlas.DrawSpriteDay(spriteRenderer, offset.ToVector2() + position + id.offsetPosition, scale, rot, z);
						break;

					case LightingSpriteRenderer2D.Type.BlackMask:
						GL.Color(Color.black);

						spriteRenderer.sprite = SpriteAtlasManager.RequestSprite(id.GetSprite(), SpriteAtlasRequest.Type.WhiteMask);
						
						LightingGraphics.WithAtlas.DrawSpriteDay(spriteRenderer, offset.ToVector2() + position + id.offsetPosition, scale, rot, z);

						break;
				}

				LightingDebug.SpriteRenderersDrawn ++;
			}

			GL.End();
		}
	}

	public class WithoutAtlas {
		static public void Draw(Camera camera, Vector2D offset, float z) {
			for(int i = 0; i < spriteRendererList.Count; i++) {
				id = spriteRendererList[i];

				if (id.GetSprite() == null) {
					continue;
				}

				if (id.InCamera(camera) == false) {
					continue;
				}

				LightingDebug.SpriteRenderersDrawn ++;

				position = id.transform.position;

				scale = id.transform.lossyScale;
				scale.x *= id.offsetScale.x;
				scale.y *= id.offsetScale.y;

				rot = id.offsetRotation;
				if (id.applyTransformRotation) {
					rot += id.transform.rotation.eulerAngles.z;
				}

				switch(id.type) {
					case LightingSpriteRenderer2D.Type.Particle: 

						color = id.color;
						color.a = id.alpha;

						material = manager.materials.GetAdditive();
						material.SetColor ("_TintColor", color);

						material.mainTexture = id.GetSprite().texture;
						LightingGraphics.WithoutAtlas.DrawSprite(material, id.spriteRenderer, offset.ToVector2() + position + id.offsetPosition, scale, rot, z);
						material.mainTexture = null;
					
						break;

					case LightingSpriteRenderer2D.Type.WhiteMask:

						material = manager.materials.GetWhiteSprite();
						
						material.mainTexture = id.GetSprite().texture;
						LightingGraphics.WithoutAtlas.DrawSprite(material, id.spriteRenderer, offset.ToVector2() + position + id.offsetPosition, scale, rot, z);
						material.mainTexture = null;
					
						break;

					case LightingSpriteRenderer2D.Type.BlackMask:

						material = manager.materials.GetBlackSprite();

						material.mainTexture = id.GetSprite().texture;
						LightingGraphics.WithoutAtlas.DrawSprite(material, id.spriteRenderer, offset.ToVector2() + position + id.offsetPosition, scale, rot, z);
						material.mainTexture = null;
					
						break;
				}
			}
		}
	}
}