using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_2018_1_OR_NEWER
using UnityEngine.Tilemaps;
#endif

using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(LightingManager2D))]
public class LightingManager2DEditor : Editor {

	static public Camera GetCamera() {
		Camera camera = null;

		if (SceneView.lastActiveSceneView != null && SceneView.lastActiveSceneView.camera != null) {
			camera = SceneView.lastActiveSceneView.camera;
		} else if (Camera.main != null) {
			camera = Camera.main;
		}
		return(camera);
	}

	static public Vector3 GetCameraPoint() {
		Vector3 pos = Vector3.zero;

		Camera camera = GetCamera();
		if (camera != null) {
			Ray worldRay = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 1.0f));
			pos = worldRay.origin;
			pos.z = 0;
		} else {
			Debug.LogError("Scene Camera Not Found");
		}

		return(pos);
	}

	[MenuItem("GameObject/2D Light/Light Source", false, 4)]
    static void CreateLightSource() {	
		GameObject newGameObject = new GameObject("2D Light Source");

		newGameObject.AddComponent<LightingSource2D>();

		newGameObject.transform.position = GetCameraPoint();
	}

	[MenuItem("GameObject/2D Light/Light Collider", false, 4)]
    static void CreateLightCollider() {
		GameObject newGameObject = new GameObject("2D Light Collider");

		newGameObject.AddComponent<PolygonCollider2D>();
		newGameObject.AddComponent<LightingCollider2D>();

		newGameObject.transform.position = GetCameraPoint();
    }

	[MenuItem("GameObject/2D Light/Light Sprite Renderer", false, 4)]
    static void CreateLightSpriteRenderer() {
		GameObject newGameObject = new GameObject("2D Light Sprite Renderer");
		
		newGameObject.AddComponent<LightingSpriteRenderer2D>();

		newGameObject.transform.position = GetCameraPoint();
    }

	#if UNITY_2018_1_OR_NEWER

	[MenuItem("GameObject/2D Light/Light Tilemap Collider", false, 4)]
    static void CreateLightTilemapCollider() {
		GameObject newGrid = new GameObject("2D Light Grid");
		newGrid.AddComponent<Grid>();

		GameObject newGameObject = new GameObject("2D Light Tilemap");
		newGameObject.transform.parent = newGrid.transform;

		newGameObject.AddComponent<Tilemap>();
		newGameObject.AddComponent<LightingTilemapCollider2D>();
    }

	#endif

	[MenuItem("GameObject/2D Light/Light Manager", false, 4)]
    static void CreateLightManager(){
		LightingManager2D.Get();
    }

	override public void OnInspectorGUI() {
		LightingManager2D script = target as LightingManager2D;

		int count = script.cameraSettings.Length;
		count = EditorGUILayout.IntField("Camer Count", count);
		if (count != script.cameraSettings.Length) {
			System.Array.Resize(ref script.cameraSettings, count);
		}

		for(int i = 0; i < script.cameraSettings.Length; i++) {
			CameraSettings cameraSetting = script.cameraSettings[i];
			if (cameraSetting == null) {
				cameraSetting = new CameraSettings();
				script.cameraSettings[i] = cameraSetting;
			}
			cameraSetting.cameraType = (CameraSettings.CameraType)EditorGUILayout.EnumPopup("Camera Type", cameraSetting.cameraType);

			if (cameraSetting.cameraType == CameraSettings.CameraType.Custom) {
				cameraSetting.customCamera = (Camera)EditorGUILayout.ObjectField(cameraSetting.customCamera, typeof(Camera), true);
			}
		}

		//script.debug = EditorGUILayout.Toggle("Debug", script.debug);
		script.disableEngine = EditorGUILayout.Toggle("Disable Engine", script.disableEngine);

		string buttonName = "Re-Initialize";
		if (script.version < Lighting2D.VERSION) {
			buttonName += " (Outdated)";
			GUI.backgroundColor = Color.red;
		}
		
		if (GUILayout.Button(buttonName)) {
			foreach(Transform transform in script.transform) {
				DestroyImmediate(transform.gameObject);
			}
			
			script.Initialize();
		
			LightingManager2D.Get();
			
			LightingMainBuffer2D.ForceUpdate();
		}

		if (GUI.changed && EditorApplication.isPlaying == false) {
			if (target != null) {
				EditorUtility.SetDirty(target);
			}
			EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
		}
	}
}