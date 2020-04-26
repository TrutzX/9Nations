using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingBufferSprite : LightingBufferBase {

	public class WithAtlas {

		public static void Mask(LightingBuffer2D buffer, LightingCollider2D id, float z) {
			if (id.isVisibleForLight(buffer) == false) {
				return;
			}

			if (id.shape.GetOriginalSprite() == null || id.spriteRenderer == null) {
				return;
			}

			Sprite sprite = id.shape.GetAtlasSprite();
			if (sprite == null) {
				Sprite reqSprite = SpriteAtlasManager.RequestSprite(id.shape.GetOriginalSprite(), SpriteAtlasRequest.Type.WhiteMask);
				if (reqSprite == null) {
					PartiallyBatchedCollider batched = new PartiallyBatchedCollider();

					batched.collider = id;

					buffer.partiallyBatchedList_Collider.Add(batched);
					return;
				} else {
					id.shape.SetAtlasSprite(reqSprite);
					sprite = reqSprite;
				}
			}

			offset.x = -buffer.lightSource.transform.position.x;
			offset.y = -buffer.lightSource.transform.position.y;
			
			p.x = id.transform.position.x + (float)offset.x;
			p.y = id.transform.position.y + (float)offset.y;

			scale.x = id.transform.lossyScale.x;
			scale.y = id.transform.lossyScale.y;

			spriteRenderer.sprite = sprite;

			LightingGraphics.WithAtlas.DrawSprite(spriteRenderer, buffer.lightSource.layerSetting[0], id.maskMode, p, scale, id.transform.rotation.eulerAngles.z, z);
			
			LightingDebug.maskGenerations ++;		
		}
	}

	public class WithoutAtlas {
			
        public static void Mask(LightingBuffer2D buffer, LightingCollider2D id, Material material, float z) {
            if (id.isVisibleForLight(buffer) == false) {
                return;
            }

            Sprite sprite = id.shape.GetOriginalSprite();
            if (sprite == null || id.spriteRenderer == null) {
                return;
            }

			offset.x = -buffer.lightSource.transform.position.x;
			offset.y = -buffer.lightSource.transform.position.y;

            p.x = id.transform.position.x + (float)offset.x;
            p.y = id.transform.position.y + (float)offset.y;

            scale.x = id.transform.lossyScale.x;
            scale.y = id.transform.lossyScale.y;

            material.mainTexture = sprite.texture;
        
            LightingGraphics.WithoutAtlas.DrawSprite(material, id.spriteRenderer, p, scale, id.transform.rotation.eulerAngles.z, z);

            material.mainTexture = null;
            
            LightingDebug.maskGenerations ++;		
        }
    }
}
