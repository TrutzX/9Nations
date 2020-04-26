using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

#if UNITY_2018_1_OR_NEWER

[CustomEditor(typeof(LightingTilemapCollider2D))]
public class LightingTilemapCollider2DEditor : Editor {
	override public void OnInspectorGUI() {
		LightingTilemapCollider2D script = target as LightingTilemapCollider2D;

		script.mapType = (LightingTilemapCollider2D.MapType)EditorGUILayout.EnumPopup("Tilemap Type", script.mapType);

		if (script.mapType == LightingTilemapCollider2D.MapType.SuperTilemapEditor) {
			LightingTilemapCollider2D.ColliderTypeSTE steCollider = (LightingTilemapCollider2D.ColliderTypeSTE)(int)script.colliderType;
			if (((LightingTilemapCollider2D.ColliderTypeSTE)(int)script.colliderType).ToString().Length < 2) {
				steCollider = LightingTilemapCollider2D.ColliderTypeSTE.None;
			}

			script.colliderType = (LightingTilemapCollider2D.ColliderType)EditorGUILayout.EnumPopup("Collision Type", steCollider);
		} else {
			script.colliderType = (LightingTilemapCollider2D.ColliderType)EditorGUILayout.EnumPopup("Collision Type", script.colliderType);
		}
				
		
		script.lightingCollisionLayer = (LightingLayer)EditorGUILayout.EnumPopup("Collision Layer", script.lightingCollisionLayer);
		
		
		if (script.mapType == LightingTilemapCollider2D.MapType.SuperTilemapEditor) {
			LightingTilemapCollider2D.MaskTypeSTE steMask = (LightingTilemapCollider2D.MaskTypeSTE)(((int)script.maskType));
			if (((LightingTilemapCollider2D.MaskTypeSTE)(int)script.maskType).ToString().Length < 2) {
				steMask = LightingTilemapCollider2D.MaskTypeSTE.None;
			}
			script.maskType = (LightingTilemapCollider2D.MaskType)EditorGUILayout.EnumPopup("Mask Type", steMask);
	
		} else {
			script.maskType = (LightingTilemapCollider2D.MaskType)EditorGUILayout.EnumPopup("Mask Type", script.maskType);
		}
		
		


		if (script.maskType != LightingTilemapCollider2D.MaskType.None) {
			script.lightingMaskLayer = (LightingLayer)EditorGUILayout.EnumPopup("Mask Layer", script.lightingMaskLayer);
			script.maskMode = (LightingMaskMode)EditorGUILayout.EnumPopup("Mask Mode", script.maskMode);
		} else {
			EditorGUI.BeginDisabledGroup(true);
			EditorGUILayout.EnumPopup("Mask Layer", script.lightingMaskLayer);
			EditorGUILayout.EnumPopup("Mask Mode", script.maskMode);
			EditorGUI.EndDisabledGroup();
		}

		script.dayHeight = EditorGUILayout.Toggle("Day Height", script.dayHeight);
		if (script.dayHeight)  {
			script.height = EditorGUILayout.FloatField("Height", script.height);
		}
		
		//script.ambientOcclusion = EditorGUILayout.Toggle("Ambient Occlusion", script.ambientOcclusion);
		//if (script.ambientOcclusion)  {
		//	script.occlusionSize = EditorGUILayout.FloatField("Occlussion Size", script.occlusionSize);
		//}

		if (GUILayout.Button("Update Collisions")) {
			script.Initialize();

			foreach(LightingSource2D light in LightingSource2D.GetList()) {
				light.movement.ForceUpdate();
			}

			LightingMainBuffer2D.ForceUpdate();
		}
		
		if (GUI.changed && EditorApplication.isPlaying == false){
            EditorUtility.SetDirty(target);
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
		}
	}
}

#endif