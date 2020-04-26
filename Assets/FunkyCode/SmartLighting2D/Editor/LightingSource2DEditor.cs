using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Linq;

[CanEditMultipleObjects]
[CustomEditor(typeof(LightingSource2D))]
public class LightingSource2DEditor : Editor {
	static bool foldout = true;

	override public void OnInspectorGUI() {
		LightingSource2D script = target as LightingSource2D;

		int layerCount = script.layerSetting.Length;
		layerCount = EditorGUILayout.IntField("Layer Count", layerCount);

		if (layerCount != script.layerSetting.Length) {
			System.Array.Resize(ref script.layerSetting, layerCount );
		}

		foldout = EditorGUILayout.Foldout(foldout, "Layers" );
		if (foldout) {
			EditorGUI.indentLevel = EditorGUI.indentLevel + 1;
			for(int i = 0; i < script.layerSetting.Length; i++) {
				if (script.layerSetting[i] == null) {
					script.layerSetting[i] = new LayerSetting();
					if (i < 8) {
						script.layerSetting[i].layerID = (LightingLayer)i;
					}
				}
				script.layerSetting[i].layerID = (LightingLayer)EditorGUILayout.EnumPopup("ID", script.layerSetting[i].layerID);
				script.layerSetting[i].type = (LightingLayerType)EditorGUILayout.EnumPopup("Type", script.layerSetting[i].type);
				script.layerSetting[i].renderingOrder = (LightingLayerOrder)EditorGUILayout.EnumPopup("Sorting", script.layerSetting[i].renderingOrder);
				script.layerSetting[i].effect = (LightingLayerEffect)EditorGUILayout.EnumPopup("Effect", script.layerSetting[i].effect);

				if (script.layerSetting[i].effect == LightingLayerEffect.InvisibleBellow && Lighting2D.atlasSettings.lightingSpriteAtlas) {
					script.layerSetting[i].maskEffectDistance = EditorGUILayout.FloatField("Effect Distance", script.layerSetting[i].maskEffectDistance);
				} else {
					EditorGUI.BeginDisabledGroup(true);
					EditorGUILayout.FloatField("Effect Distance", 0);
					EditorGUI.EndDisabledGroup();
				}
			}
			EditorGUI.indentLevel = EditorGUI.indentLevel - 1;
		}

		Color newColor = Color.white;
		if (Lighting2D.commonSettings.hdr) {
			newColor = EditorGUILayout.ColorField(new GUIContent("Color"), script.lightColor, true, true, true);
		} else {
			newColor = EditorGUILayout.ColorField("Color", script.lightColor);
		}
		
		if (script.lightColor.Equals(newColor) == false) {
			newColor.a = 1f;
			script.lightColor = newColor;
			
			LightingMainBuffer2D.ForceUpdate();
		}

		float newAlpha = EditorGUILayout.Slider("Alpha", script.lightAlpha, 0, 1);
		if (script.lightAlpha != newAlpha) {
			script.lightAlpha = newAlpha;

			LightingMainBuffer2D.ForceUpdate();
		}

		float newLightSize = EditorGUILayout.FloatField("Size", script.lightSize);
		if (newLightSize != script.lightSize) {
			script.lightSize = newLightSize;

			LightingMainBuffer2D.ForceUpdate();
		}
		
		switch(Lighting2D.lightingSourceSettings.fixedLightBufferSize) {
			case true:
				EditorGUI.BeginDisabledGroup(true);
				EditorGUILayout.EnumPopup("Buffer Size", Lighting2D.lightingSourceSettings.fixedLightTextureSize);
				EditorGUI.EndDisabledGroup();

				break;

			case false:
				script.textureSize = (LightingSourceTextureSize)EditorGUILayout.EnumPopup("Buffer Size", script.textureSize);
				break;
		}

		EditorGUILayout.Foldout(true, "Light Sprite" );
		EditorGUI.indentLevel = EditorGUI.indentLevel + 1;
		script.lightSprite = (LightingSource2D.LightSprite)EditorGUILayout.EnumPopup("Light Sprite", script.lightSprite);

		if (script.lightSprite == LightingSource2D.LightSprite.Custom) {
			script.spriteFlipX = EditorGUILayout.Toggle("Flip X", script.spriteFlipX);
			script.spriteFlipY = EditorGUILayout.Toggle("Flip Y", script.spriteFlipY);
			Sprite newSprite = (Sprite)EditorGUILayout.ObjectField("Sprite", script.sprite, typeof(Sprite), true);
			if (newSprite != script.sprite) {
				script.sprite = newSprite;

				LightingMainBuffer2D.ForceUpdate();
			}
			
		} else {
			if (script.sprite != LightingSource2D.GetDefaultSprite()) {
				script.sprite = LightingSource2D.GetDefaultSprite();

				LightingMainBuffer2D.ForceUpdate();
			}
		}
		EditorGUI.indentLevel = EditorGUI.indentLevel - 1;

		EditorGUILayout.Foldout(foldout, "Additive Shader" );
		EditorGUI.indentLevel = EditorGUI.indentLevel + 1;

		script.additive = EditorGUILayout.Toggle("Enable", script.additive);
		script.additive_alpha = EditorGUILayout.Slider("Alpha", script.additive_alpha, 0, 1);

		EditorGUILayout.Foldout(true, "Sorting Layer" );
			//if (foldout_sortingLayer) {
			//	EditorGUILayout.Slider("Sorting Layer ID",0, 0, 31);
		script.sortingOrder = EditorGUILayout.IntField("Sorting Layer Order", script.sortingOrder);
		script.sortingLayer = EditorGUILayout.TextField("Sorting Layer Name", script.sortingLayer);

			//}
		EditorGUI.indentLevel = EditorGUI.indentLevel - 1;
		//if (script.additive) {
			
		//}
	
		script.enableCollisions = EditorGUILayout.Toggle("Apply Shadows & Masks", script.enableCollisions);

		script.rotationEnabled = EditorGUILayout.Toggle("Apply Rotation", script.rotationEnabled);
	
		script.eventHandling = EditorGUILayout.Toggle("Apply Event Handling" , script.eventHandling);

		script.whenInsideCollider= (LightingSource2D.WhenInsideCollider)EditorGUILayout.EnumPopup("Apply Light Inside Collider", script.whenInsideCollider);

		// script.disableWhenInvisible = EditorGUILayout.Toggle("Disable When Invisible", script.disableWhenInvisible);
		
		
		float newPenumbraSize = EditorGUILayout.Slider("Penumbra Size (Inverse)", script.occlusionSize, 1, 60);;
		
		if (newPenumbraSize != script.occlusionSize) {
			script.occlusionSize = newPenumbraSize;

			script.movement.ForceUpdate();
		}

		if (targets.Length > 1) {
			if (GUILayout.Button("Apply to All")) {
				foreach(Object obj in targets) {
					LightingSource2D copy = obj as LightingSource2D;
					if (copy == script) {
						continue;
					}

					copy.layerSetting[0].renderingOrder = script.layerSetting[0].renderingOrder;
					copy.layerSetting[1].renderingOrder = script.layerSetting[1].renderingOrder;
					
					copy.lightColor = script.lightColor;
					copy.lightAlpha = script.lightAlpha;
					copy.lightSize = script.lightSize;
					copy.textureSize = script.textureSize;

					copy.enableCollisions = script.enableCollisions;
					copy.rotationEnabled = script.rotationEnabled;
					copy.additive = script.additive;
					copy.additive_alpha = script.additive_alpha;
					

					copy.eventHandling = script.eventHandling;
					copy.whenInsideCollider = script.whenInsideCollider;

					copy.lightSprite = script.lightSprite;
					copy.sprite = script.sprite;
				}

				LightingMainBuffer2D.ForceUpdate();
			}
		}
		
		if (GUI.changed && EditorApplication.isPlaying == false){
            EditorUtility.SetDirty(target);
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
		}
	}
}
