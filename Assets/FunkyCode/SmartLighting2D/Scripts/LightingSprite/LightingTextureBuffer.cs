using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingTextureBuffer {
	static List<LightingTextureRenderer2D> textureRendererList;
	static LightingTextureRenderer2D id;
	static LightingManager2D manager;
	static Vector2 size;
	static Material material;

    public static void Draw(Camera camera, Vector2D offset, float z) {
		manager = LightingManager2D.Get();
		
		textureRendererList = LightingTextureRenderer2D.GetList();

		if (Lighting2D.atlasSettings.lightingSpriteAtlas) {

		} else {
			WithoutAtlas.Draw(camera, offset, z);
		}
    }

	public class WithoutAtlas {
		static public void Draw(Camera camera, Vector2D offset, float z) {
			for(int i = 0; i < textureRendererList.Count; i++) {
				id = textureRendererList[i];

				if (id.InCamera(camera) == false) {
					continue;
				}

				material = manager.materials.GetAdditive();
				material.SetColor ("_TintColor", id.color);

				material.mainTexture = id.texture;
				LightingGraphics.WithoutAtlas.DrawTexture(material, offset.ToVector3() + id.transform.position, id.size, 0, z);
				material.mainTexture = null;
			}
		}
	}
}