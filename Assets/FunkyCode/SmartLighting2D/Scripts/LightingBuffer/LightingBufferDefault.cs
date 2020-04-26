using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingBufferDefault {

	static List<LightingCollider2D> colliderList;
	#if UNITY_2018_1_OR_NEWER
        static List<LightingTilemapCollider2D> tilemapList;
    #endif

	static LightingManager2D manager;

	static bool drawMask = false;
	static bool drawShadows = false;
	
	static int layerID = 0;

	static LightingSource2D source2D;
	static float lightSizeSquared;
	static float z;
	static bool maskEffect;

	static Material materialWhite;
	static Material materialBlack;
	static Material material;

	public static void Draw(LightingBuffer2D buffer, LayerSetting layer) {
		// Layer ID
		layerID = layer.GetLayerID();
		if (layerID < 0) {
			return;
		}

		// Calculation Setup
		source2D = buffer.lightSource;
		lightSizeSquared = Mathf.Sqrt(source2D.lightSize * source2D.lightSize + source2D.lightSize * source2D.lightSize);
		z = buffer.transform.position.z;

		manager = LightingManager2D.Get();
		colliderList = LightingCollider2D.GetList();

		#if UNITY_2018_1_OR_NEWER
			tilemapList = LightingTilemapCollider2D.GetList();
		#endif

		maskEffect = (layer.effect == LightingLayerEffect.InvisibleBellow);

		// Draw Mask & Shadows?
		drawMask = (layer.type != LightingLayerType.ShadowOnly);
        drawShadows = (layer.type != LightingLayerType.MaskOnly);

		// Materials
		materialWhite = manager.materials.GetWhiteSprite();
		materialBlack = manager.materials.GetBlackSprite();

		// Drawing
		if (Lighting2D.atlasSettings.lightingSpriteAtlas && SpriteAtlasManager.GetAtlasPage() != null) {
			WithAtlas.Draw(buffer, layer);
		} else {
			WithoutAtlas.Draw(buffer, layer);
		}
	}

	public class WithAtlas {

		public static void Draw(LightingBuffer2D buffer, LayerSetting layer) {
			manager.materials.GetAtlasMaterial().SetPass(0);

			GL.Begin(GL.TRIANGLES);

			if (drawShadows) {

				for(int id = 0; id < colliderList.Count; id++) {
					if ((int)colliderList[id].lightingCollisionLayer != layerID) {
						continue;
					}

					switch(colliderList[id].shape.colliderType) {
						case LightingCollider2D.ColliderType.Collider:
						case LightingCollider2D.ColliderType.SpriteCustomPhysicsShape:
							LightingBufferCollider.Shadow(buffer, colliderList[id], lightSizeSquared, z);
						break;

						case LightingCollider2D.ColliderType.Mesh:
							LightingBufferMesh.Shadow(buffer, colliderList[id], lightSizeSquared, z);
						break;

						case LightingCollider2D.ColliderType.SkinnedMesh:
							LightingBufferSkinnedMesh.Shadow(buffer, colliderList[id], lightSizeSquared, z);
						break;
					}

				}

				#if UNITY_2018_1_OR_NEWER
					for(int id = 0; id < tilemapList.Count; id++) {
						if ((int)tilemapList[id].lightingCollisionLayer != layerID) {
							continue;
						}

						// Tilemap Shadow
						LightingBufferSTE.Shadow(buffer, tilemapList[id], lightSizeSquared, z);
						LightingBufferTilemapRectangle.Shadow(buffer, tilemapList[id], lightSizeSquared, z);
						LightingBufferTilemapComponent.Shadow(buffer, tilemapList[id], lightSizeSquared, z);
					}
				#endif 

			}

			if (drawMask) {
				if (colliderList.Count > 0) {
					for(int id = 0; id < colliderList.Count; id++) {
						if ((int)colliderList[id].lightingMaskLayer != layerID) {
							continue;
						}

						switch(colliderList[id].shape.maskType) {
							case LightingCollider2D.MaskType.Collider:
							case LightingCollider2D.MaskType.SpriteCustomPhysicsShape:
								LightingBufferCollider.Mask(buffer, colliderList[id], layer, z);

							break;

							case LightingCollider2D.MaskType.Sprite:
								LightingBufferSprite.WithAtlas.Mask(buffer, colliderList[id], z);

							break;
						}
						//LightingBufferMesh.Mask(buffer, colliderList[id], layer, offset, z);
						//LightingBufferSkinnedMesh.WithoutAtlas.Mask(buffer, colliderList[id], layer, offset, z);
					}
				}
				
				#if UNITY_2018_1_OR_NEWER		
					for(int id = 0; id < tilemapList.Count; id++) {
						if ((int)tilemapList[id].lightingMaskLayer != layerID) {
							continue;
						}

						// Tilemap Shape Mask
						LightingBufferTilemapRectangle.MaskShape(buffer, tilemapList[id], z);

						// Tilemap Sprite Mask
						LightingBufferTilemapRectangle.WithAtlas.MaskSprite(buffer, tilemapList[id], z);
					}
				#endif
			}
			
			GL.End();

			// Partialy Batched (Default Edition)
			if (buffer.partiallyBatchedList_Collider.Count > 0) {
				PartiallyBatchedCollider batch;			

				for(int i = 0; i < buffer.partiallyBatchedList_Collider.Count; i++) {
					batch = buffer.partiallyBatchedList_Collider[i];

					material = materialWhite;

					if (maskEffect && batch.collider.transform.position.y < source2D.transform.position.y) {
						material = materialBlack;
					}

					LightingBufferSprite.WithoutAtlas.Mask(buffer, batch.collider, material, z);
				}

				buffer.partiallyBatchedList_Collider.Clear();
			}

			if (buffer.partiallyBatchedList_Tilemap.Count > 0) {
				PartiallyBatchedTilemap batch;

				for(int i = 0; i < buffer.partiallyBatchedList_Tilemap.Count; i++) {
					batch = buffer.partiallyBatchedList_Tilemap[i];

					material = materialWhite;

					if (maskEffect && batch.polyOffset.y < 0) {
						material = materialBlack;
					}

					LightingGraphics.WithoutAtlas.DrawSprite(material, batch.virtualSpriteRenderer, batch.polyOffset, batch.tileSize, 0, z);
				}
				
				buffer.partiallyBatchedList_Tilemap.Clear();
			}
		}
	}

	public class WithoutAtlas {

		public static void Draw(LightingBuffer2D buffer, LayerSetting layer) {
			if (drawShadows) {
				Shadows(buffer, layer);
			}

			if (drawMask) {
				Mask(buffer, layer);
			}
		}

		public static void Shadows(LightingBuffer2D buffer, LayerSetting layer) {
			manager.materials.GetAtlasMaterial().SetPass(0);

			GL.Begin(GL.TRIANGLES);

			for(int id = 0; id < colliderList.Count; id++) {
				if ((int)colliderList[id].lightingCollisionLayer != layerID) {
					continue;
				}

				switch(colliderList[id].shape.colliderType) {
					case LightingCollider2D.ColliderType.Collider:
					case LightingCollider2D.ColliderType.SpriteCustomPhysicsShape:
						LightingBufferCollider.Shadow(buffer, colliderList[id], lightSizeSquared, z);
		
					break;

					case LightingCollider2D.ColliderType.Mesh:
						LightingBufferMesh.Shadow(buffer, colliderList[id], lightSizeSquared, z);
					break;

					case LightingCollider2D.ColliderType.SkinnedMesh:
						LightingBufferSkinnedMesh.Shadow(buffer, colliderList[id], lightSizeSquared, z);
					break;
				}
				
				//LightingBufferColliderAnima.Shadow(buffer, colliderList[id], lightSizeSquared, z, offset);
			}

			#if UNITY_2018_1_OR_NEWER
				for(int id = 0; id < tilemapList.Count; id++) {
					if ((int)tilemapList[id].lightingCollisionLayer != layerID) {
						continue;
					}
					LightingBufferSTE.Shadow(buffer, tilemapList[id], lightSizeSquared, z);
					LightingBufferTilemapIsometric.Shadow(buffer, tilemapList[id], lightSizeSquared, z);
					LightingBufferTilemapRectangle.Shadow(buffer, tilemapList[id], lightSizeSquared, z);
					LightingBufferTilemapComponent.Shadow(buffer, tilemapList[id], lightSizeSquared, z);
				}
			#endif 
	
			GL.End();
		}

		static public void Mask(LightingBuffer2D buffer, LayerSetting layer) {
			manager.materials.GetAtlasMaterial().SetPass(0);
			GL.Begin(GL.TRIANGLES);

			GL.Color(Color.white);

			// Collider Shape Mask
			for(int id = 0; id < colliderList.Count; id++) {
				if ((int)colliderList[id].lightingMaskLayer != layerID) {
					continue;
				}

				switch(colliderList[id].shape.maskType) {
					case LightingCollider2D.MaskType.Collider:
					case LightingCollider2D.MaskType.SpriteCustomPhysicsShape:
						LightingBufferCollider.Mask(buffer, colliderList[id], layer, z);
		
					break;
				}
			}

			GL.Color(Color.white);
			
			// Tilemap Shape Mask
			#if UNITY_2018_1_OR_NEWER
				for(int id = 0; id < tilemapList.Count; id++) {
					if ((int)tilemapList[id].lightingMaskLayer != layerID) {
						continue;
					}
					LightingBufferSTE.MaskShape(buffer, tilemapList[id], z);
					LightingBufferTilemapRectangle.MaskShape(buffer, tilemapList[id], z);
					LightingBufferTilemapIsometric.MaskShape(buffer, tilemapList[id], z);
				}
			#endif

			GL.End();

			// Collider Sprite Mask
			if (colliderList.Count > 0) {
				for(int id = 0; id < colliderList.Count; id++) {
					if ((int)colliderList[id].lightingMaskLayer != layerID) {
						continue;
					}
					material = materialWhite;

					if (colliderList[id].maskMode == LightingMaskMode.Invisible) {
						material = materialBlack;
					} else if (maskEffect && colliderList[id].transform.position.y < source2D.transform.position.y) {
						material = materialBlack;
					}

					switch(colliderList[id].shape.maskType) {
						case LightingCollider2D.MaskType.Mesh:
							LightingBufferMesh.WithoutAtlas.Mask(buffer, colliderList[id], material, layer, z);
						break;

						case LightingCollider2D.MaskType.SkinnedMesh:
							LightingBufferSkinnedMesh.WithoutAtlas.Mask(buffer, colliderList[id], material, layer, z);
						break;

						case LightingCollider2D.MaskType.Sprite:
							LightingBufferSprite.WithoutAtlas.Mask(buffer, colliderList[id], material, z);
						break;
					}
				}
			}
			
			// Tilemap Sprite Mask
			#if UNITY_2018_1_OR_NEWER
				for(int id = 0; id < tilemapList.Count; id++) {
					if ((int)tilemapList[id].lightingMaskLayer != layerID) {
						continue;
					}
					LightingBufferTilemapRectangle.WithoutAtlas.MaskSprite(buffer, tilemapList[id], materialWhite, materialBlack, z);
					LightingBufferTilemapIsometric.WithoutAtlas.MaskSprite(buffer, tilemapList[id], materialWhite, z);
					LightingBufferSTE.WithoutAtlas.MaskSprite(buffer, tilemapList[id], materialWhite, z);
				}
			#endif
		}
	}   
}