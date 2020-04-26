using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(Lighting2DSettings))]
public class LightingSettings2DEditor : Editor {
	override public void OnInspectorGUI() {
		Lighting2DSettings script = target as Lighting2DSettings;

		Lighting2DSettingsProfile profile = script.GetProfile();

		profile = (Lighting2DSettingsProfile)EditorGUILayout.ObjectField("Profile", profile, typeof(Lighting2DSettingsProfile), true);

		script.SetProfile(profile);

		script.initializeCopy = EditorGUILayout.Toggle("Initialize Copy", script.initializeCopy);

		if (GUI.changed && EditorApplication.isPlaying == false) {
			if (target != null) {
				EditorUtility.SetDirty(target);
			}
			EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
		}
	}
}
