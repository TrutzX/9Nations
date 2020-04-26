using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
	using UnityEditor;
#endif

[System.Serializable]
public class CameraSettings {
	public enum CameraType {MainCameraTag, Custom};
	public CameraType cameraType = CameraType.MainCameraTag;
	public Camera customCamera = null;

	public LightingMainBuffer2D mainBuffer;
}

[ExecuteInEditMode] // Only 1 Lighting Manager Allowed
public class LightingManager2D : MonoBehaviour {

	private static LightingManager2D instance;

	public CameraSettings[] cameraSettings = new CameraSettings[1];

	public bool debug = false;
	public bool disableEngine = false;

	public LightingManager2DMaterials materials = new LightingManager2DMaterials();

	public const int lightingLayer = 8;
	public const bool culling = true;

	public int version = 0;

	public static bool initialized = false; // Sets Lighting Settings for Lighting2D at the start of the scene

	public Camera GetCamera(int id) {
		CameraSettings cameraSetting = cameraSettings[id];

		if (cameraSetting == null) {
			return(null);
		}

		switch(cameraSetting.cameraType) {
			case CameraSettings.CameraType.MainCameraTag:
				return(Camera.main);

			case CameraSettings.CameraType.Custom:
				return(cameraSetting.customCamera);
		}

		return(null);
	}
	
	static public LightingManager2D Get() {
		if (instance != null) {
			return(instance);
		}

		foreach(LightingManager2D manager in Object.FindObjectsOfType(typeof(LightingManager2D))) {
			instance = manager;
			return(instance);
		}

		// Create New Light Manager
		GameObject gameObject = new GameObject();
		gameObject.name = "Lighting Manager 2D";

		instance = gameObject.AddComponent<LightingManager2D>();
		instance.Initialize();

		return(instance);
	}

	public void Initialize () {
		instance = this;

		transform.position = Vector3.zero;

		version = Lighting2D.VERSION;

		cameraSettings = new CameraSettings[1];
		cameraSettings[0] = new CameraSettings();
	}

	void DrawAdditiveLights() {
		foreach (LightingSource2D id in LightingSource2D.GetList()) {
			if (id.buffer == null) {
				continue;
			}

			if (id.isActiveAndEnabled == false) {
				continue;
			}

			if (id.buffer.bufferCamera == null) {
				continue;
			}

			if (id.InAnyCamera() == false) {
				continue;
			}

			if (id.additive == false) {
				continue;
			}

			LightingMeshRenderer lightingMesh = MeshRendererManager.Pull(id);
			
			if (lightingMesh != null) {
				lightingMesh.UpdateAsLightSource(id);
			}				
		}
	}

	public void Awake() {
		if (LightingManager2D.initialized == false) {
			Lighting2DSettingsProfile profile = Lighting2D.GetProfile();
			Lighting2D.UpdateByProfile(profile);
			LightingManager2D.initialized = true;
		}

		SpriteAtlasManager.Initialize();

		materials.Initialize(Lighting2D.commonSettings.hdr);

		SpriteAtlasManager.Update();
		
		if (instance != null && instance != this) {
			instance = this;
			Debug.LogWarning("Smart Lighting2D: Lighting Manager duplicate was found, old instance destroyed.", gameObject);

			foreach(LightingManager2D manager in Object.FindObjectsOfType(typeof(LightingManager2D))) {
				if (manager != instance) {
					Destroy(manager.gameObject);
				}
			}
		}
	}

	void Update() {
		if (materials.hdr != Lighting2D.commonSettings.hdr) {
			materials.Initialize(Lighting2D.commonSettings.hdr);

			foreach(LightingMainBuffer2D buffer in new List<LightingMainBuffer2D>(LightingMainBuffer2D.list)) {
				DestroyImmediate(buffer.gameObject);
			}

			foreach(LightingBuffer2D buffer in new List<LightingBuffer2D>(LightingBuffer2D.list)) {
				DestroyImmediate(buffer.gameObject);
			}
		}

		for(int i = 0; i < cameraSettings.Length; i++) {
			LightingMainBuffer2D.Get(GetCamera(i));
		}

		if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.L)) {
			debug = !debug;
		}

		SpriteAtlasManager.Update();

		foreach(LightingMainBuffer2D buffer in LightingMainBuffer2D.list) {
			if (disableEngine) {
				buffer.enabled = false;
				buffer.bufferCamera.enabled = false;	
				return;
			}

			if (buffer != null) {
				if (Lighting2D.commonSettings.darknessBuffer) {
					buffer.enabled = true;
					buffer.bufferCamera.enabled = true;
				} else {
					buffer.enabled = false;
					buffer.bufferCamera.enabled = false;
				}
			}
		}
		
		if (Lighting2D.commonSettings.drawAdditiveLights) {
			DrawAdditiveLights();
		}

		//if (Lighting2D.renderingMode == Lighting2D.RenderingMode.OnRender) {
		//	Render_OnRenderMode();
		//}
	}

	// Post-Rendering Mode Drawing	
	public void OnRenderObject() {
		if (disableEngine) {
			return;
		}
		
		for(int id = 0; id < cameraSettings.Length; id++) {
			Camera camera = GetCamera(id);

			if (Camera.current != camera) {
				continue;
			}

			if (Lighting2D.renderingMode == Lighting2D.RenderingMode.OnPostRender) {
				Lighting2DRender.PostRender(camera);
			}
		}
	}

	static public float GetSunDirection() {
		return(Lighting2D.dayLightingSettings.sunDirection * Mathf.Deg2Rad);
	}

	void OnGUI() {
		if (debug) {
			LightingDebug.OnGUI();
		}
	}
}

public class LightingRender {
	public static Mesh preRenderMesh = null;

	public static Mesh GetMesh() {
		if (preRenderMesh == null) {
			Polygon2D tilePoly = Polygon2D.CreateRect(new Vector2(1f, 1f));
			Mesh staticTileMesh = tilePoly.CreateMesh(new Vector2(2f, 2f), Vector2.zero);
			preRenderMesh = staticTileMesh;
		}
		return(preRenderMesh);
	}

	static public int GetTextureSize(LightingSourceTextureSize textureSize) {
		switch(textureSize) {
			case LightingSourceTextureSize.px2048:
				return(2048);

			case LightingSourceTextureSize.px1024:
				return(1024);

			case LightingSourceTextureSize.px512:
				return(512);

			case LightingSourceTextureSize.px256:
				return(256);
			
			default:
				return(128);
		}
	}
}

