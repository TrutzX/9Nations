using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] 
public class LightingMainBuffer2D : MonoBehaviour {
	public RenderTexture renderTexture;

	// should be static
	private LightingMaterial material = null; 
	public Camera bufferCamera;
	public Camera regularCamera;

	// Updated to match camera size
	public int screenWidth = 640;
	public int screenHeight = 480;

	// This should be hanged to setting
	const float cameraOffset = 40f;

	// Updated to match tracked camera size
	public float cameraSize = 5f;

	static Vector2D pos2D = Vector2D.Zero();
	static Vector2D size2D = Vector2D.Zero();
	static Vector2D offset = Vector2D.Zero();

	public static List<LightingMainBuffer2D> list = new List<LightingMainBuffer2D>();

	public void OnEnable() {
		list.Add(this);
	}

	public void OnDisable() {
		list.Remove(this);
	}

	static public LightingMainBuffer2D Get(Camera camera) {

		foreach(LightingMainBuffer2D mainBuffer in list) {
			if (mainBuffer.regularCamera == camera) {
				return(mainBuffer);
			}
		}

		GameObject setMainBuffer = new GameObject ();
		setMainBuffer.transform.parent = LightingManager2D.Get().transform;
		setMainBuffer.name = "Camera Buffer";
		setMainBuffer.layer = LightingManager2D.lightingLayer;

		LightingMainBuffer2D buffer = setMainBuffer.AddComponent<LightingMainBuffer2D> ();
		buffer.regularCamera = camera;
		buffer.Initialize();
		
		return(buffer);
	}

	 public static void ForceUpdate() {
		foreach(LightingMainBuffer2D buffer in new List<LightingMainBuffer2D>(list)) {
			buffer.gameObject.SetActive(false);
			buffer.gameObject.SetActive(true);
		}
	}

	public void ForceUpdateSelf() {
		gameObject.SetActive(false);
		gameObject.SetActive(true);
	}

	public void Initialize() {
		SetUpRenderTexture ();

		SetUpCamera ();
	}

	void SetUpRenderTexture() {
		LightingManager2D manager = LightingManager2D.Get();

		screenWidth = (int)(Screen.width * Lighting2D.commonSettings.lightingResolution);
		screenHeight = (int)(Screen.height * Lighting2D.commonSettings.lightingResolution);

		LightingDebug.NewRenderTextures ++;

		if (screenWidth > 0 && screenHeight > 0) {
			RenderTextureFormat format = RenderTextureFormat.Default;
			if (Lighting2D.commonSettings.hdr == false) {
				name = "Camera Buffer";
	
				format = Lighting2D.lightingSourceSettings.textureFormat;
			} else {
				name = "HDR Camera Buffer";
				format = RenderTextureFormat.DefaultHDR;
			}

			renderTexture = new RenderTexture (screenWidth, screenHeight, 0, format);
			renderTexture.Create ();
		}
	}

	public void ClearMaterial() {
		material = null;
	}

	public Material GetMaterial() {
		if (material == null || material.Get() == null) {
			if (Lighting2D.commonSettings.hdr) {
				material = LightingMaterial.Load("SmartLighting2D/Multiply HDR");
			} else {
				material = LightingMaterial.Load(Max2D.shaderPath + "Particles/Multiply");
			}
		}

		material.SetTexture(renderTexture);

		return(material.Get());
	}

	void SetUpCamera() {
		bufferCamera = gameObject.AddComponent<Camera> ();
		bufferCamera.clearFlags = CameraClearFlags.Color;
		bufferCamera.backgroundColor = Color.black;
		bufferCamera.cameraType = CameraType.Game;
		bufferCamera.orthographic = true;
		bufferCamera.targetTexture = renderTexture;
		bufferCamera.farClipPlane = 1f;
		bufferCamera.nearClipPlane = 0f;
		bufferCamera.allowMSAA = false;
		bufferCamera.allowHDR = Lighting2D.commonSettings.hdr;
		bufferCamera.enabled = false;
	}

	void Update() {
		if (Lighting2D.commonSettings.hdr != bufferCamera.allowHDR) {
			bufferCamera.allowHDR = Lighting2D.commonSettings.hdr;
		}

		// Workaround
		Camera camera = regularCamera;
		if (camera == null) {

			return;
		}
		bufferCamera.backgroundColor = Lighting2D.commonSettings.darknessColor;
	}

	void LateUpdate () {
		LightingManager2D manager = LightingManager2D.Get();

		int width = (int)(Screen.width * Lighting2D.commonSettings.lightingResolution);
		int height = (int)(Screen.height * Lighting2D.commonSettings.lightingResolution);

		if (width != screenWidth || height != screenHeight) {
			SetUpRenderTexture();

			bufferCamera.targetTexture = renderTexture;
		}

		Camera camera = regularCamera;

		if (camera == null) {
			Debug.LogWarning("Smart Lighting 2D: Camera is Missing");
			DestroyImmediate(this.gameObject);
			return;
		}
 
		bufferCamera.orthographicSize = camera.orthographicSize;

		cameraSize = bufferCamera.orthographicSize;

		transform.position = new Vector3(0, 0, camera.transform.position.z - cameraOffset);
		transform.rotation = camera.transform.rotation;

		ForceUpdateSelf();

		if (Lighting2D.renderingPipeline == Lighting2D.RenderingPipeline.Scriptable) {
			LWRP_OnPostRender();
		}
	}

	// Light Weight Pipeline
	public void LWRP_OnPostRender() {
		LightingManager2D manager = LightingManager2D.Get();
		Camera camera = regularCamera;

		if (camera == null) {
			return;
		}

		float z = transform.position.z;		

		// Day Lighting Not Supported

		// Rooms Not Supported

		// Lighting Sprite Renderers Not Supported

		LWRP_DrawLightingBuffers(z);

		// Occlusion Not Supportedd
	}

	// Lighting Buffers
	void LWRP_DrawLightingBuffers(float z) {
		BufferLightingSourceRenderer.FreeAll();

		LightingManager2D manager = LightingManager2D.Get();

		Camera camera = regularCamera;
		
		Material material = manager.materials.GetAdditive();

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

			//if (id.InCamera() == false) {
			//	continue;
			//}

			BufferLightingSourceRenderer sourceBuffer = BufferLightingSourceRenderer.GetFree();

			MeshRenderer meshRenderer = sourceBuffer.GetMeshRenderer();
			MeshFilter meshFilter = sourceBuffer.GetMeshFilter();

			meshFilter.mesh = sourceBuffer.GetMesh();

			material.mainTexture = id.buffer.renderTexture;

			Color lightColor = id.lightColor;
			lightColor.a = id.lightAlpha / 2;
			material.SetColor ("_TintColor", lightColor);
		
			meshRenderer.sharedMaterial = material;

			Vector3 pos = id.transform.position - camera.transform.position;
			float size = id.buffer.bufferCamera.orthographicSize;
	
			sourceBuffer.transform.localPosition = new Vector3(pos.x, pos.y, 0);
			sourceBuffer.transform.localScale = new Vector3(size, size, 1);
		}
	}
	
	// Universal Rendering Pipeline
	public void OnPostRender() {
		if (Lighting2D.renderingPipeline != Lighting2D.RenderingPipeline.Standard) {
			return;
		}

		Camera camera = regularCamera;
		if (camera == null) {
			return;
		}

		LightingDebug.LightMainBufferUpdates += 1;

		float z = transform.position.z;

		offset.x = -camera.transform.position.x;
		offset.y = -camera.transform.position.y;

		GL.PushMatrix();
		
		if (Lighting2D.dayLightingSettings.drawDayShadows) {
			DayLighting.Draw(camera, offset, z);
		}
	
		if (Lighting2D.commonSettings.drawRooms) {
			DrawRooms(camera, offset, z);
			
			#if UNITY_2018_1_OR_NEWER
				DrawTilemapRooms(camera, offset, z);
			#endif
		}

		LightingSpriteBuffer.Draw(camera, offset, z);

		LightingTextureBuffer.Draw(camera, offset, z);

		DrawLightingBuffers(regularCamera, z);

		if (Lighting2D.commonSettings.drawOcclusion) {
			LightingOcclusionCollider.Draw(offset, z);
		}

		GL.PopMatrix();
	}

	public void OnPreCull() {
		LightingManager2D manager = LightingManager2D.Get();

		if (manager.disableEngine) {
			return;
		}

		switch(Lighting2D.renderingMode) {
			case Lighting2D.RenderingMode.OnRender:
				Lighting2DRender.OnRender(regularCamera);
			break;

			case Lighting2D.RenderingMode.OnPreRender:
				Lighting2DRender.PreRender(regularCamera);
			break;
		}
	}
	
	// Room Mask
	void DrawRooms(Camera camera, Vector2D offset, float z) {
		LightingManager2D manager = LightingManager2D.Get();

		manager.materials.GetAtlasMaterial().SetPass(0);
		GL.Begin(GL.TRIANGLES);

		foreach (LightingRoom2D id in LightingRoom2D.GetList()) {
			//LightingRoomCollider.Mask(camera, id, offset, z);
		}

		GL.End();
	}

	#if UNITY_2018_1_OR_NEWER
		void DrawTilemapRooms(Camera camera, Vector2D offset, float z) {
			LightingManager2D manager = LightingManager2D.Get();

			foreach (LightingTilemapRoom2D id in LightingTilemapRoom2D.GetList()) {
			//	LightingRoomTilemap.MaskSpriteWithoutAtlas(camera, id, offset, z);
			}
		}
	#endif

	// Lighting Buffers
	void DrawLightingBuffers(Camera camera, float z) {
		LightingManager2D manager = LightingManager2D.Get();

		Material material = manager.materials.GetAdditive();

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

			if (id.InCamera(camera) == false) {
				continue;
			}

			Vector3 pos = id.transform.position - camera.transform.position;
			pos2D.x = pos.x;
			pos2D.y = pos.y;

			float size = id.buffer.bufferCamera.orthographicSize;
			size2D.x = size;
			size2D.y = size;

			Color lightColor = id.lightColor;
			lightColor.a = id.lightAlpha / 2;

			material.mainTexture = id.buffer.renderTexture;

			material.SetColor ("_TintColor", lightColor);
		
			Lighting2DUtility.Max2D.DrawImage (material, pos2D, size2D, z);
		}
	}

















	
	

}

public class Lighting2DRender {
	public static Vector3 GetSize(Camera camera) {
		float sizeY = camera.orthographicSize;

		Vector3 size = new Vector2(sizeY, sizeY);
		
		size.x *= ((float)Screen.width / (float)Screen.height);

		size.x *= (((float)Screen.width + 1f) / Screen.width);

		// Necessary?
		size.y *= (((float)Screen.height + 1f) / Screen.height);
		
		size.z = 1;

		return(size);
	}

	public static Vector3 GetPosition(Camera camera) {
		Vector3 pos = camera.transform.position;
		pos.z += camera.nearClipPlane + 0.1f;
		return(pos);
	}	

	public static bool IsReady(Camera camera) {
		if (Lighting2D.commonSettings.darknessBuffer == false) {
			return(false);
		}

		LightingMainBuffer2D mainBuffer = LightingMainBuffer2D.Get(camera);

		if (mainBuffer == null) {
			mainBuffer = LightingMainBuffer2D.Get(camera);
			return(false);
		}	

		if (mainBuffer.bufferCamera == null) {
			return(false);
		}

		return(true);
	}

	// Post-Render Mode Drawing
	public static void PostRender(Camera camera) {
		if (IsReady(camera) == false) {
			return;
		}

		if (Lighting2D.renderingMode != Lighting2D.RenderingMode.OnPostRender) {
			return;
		}

		if (Camera.current != camera) {
			return;
		}

		LightingDebug.LightMainCameraUpdates +=1;

		LightingMainBuffer2D mainBuffer = LightingMainBuffer2D.Get(camera);

		Lighting2DUtility.Max2D.DrawImage(mainBuffer.GetMaterial(), GetPosition(camera), GetSize(camera), camera.transform.eulerAngles.z, GetPosition(camera).z);
	}

	// Mesh-Render Mode Drawing
	static public void OnRender(Camera camera) {
		if (IsReady(camera) == false) {
			return;
		}

		LightingMainBuffer2D mainBuffer = LightingMainBuffer2D.Get(camera);

		OnRenderMode onRenderMode = OnRenderMode.Get(camera, mainBuffer);
		if (onRenderMode == null) {
			return;
		}

		onRenderMode.UpdatePosition();

		if (onRenderMode.meshRenderer != null) {	
			onRenderMode.meshRenderer.sortingLayerID = Lighting2D.sortingLayer.ID;
			onRenderMode.meshRenderer.sortingLayerName = Lighting2D.sortingLayer.Name;
			onRenderMode.meshRenderer.sortingOrder = Lighting2D.sortingLayer.Order;

			onRenderMode.meshRenderer.enabled = true;
			if (onRenderMode.meshRenderer.sharedMaterial != mainBuffer.GetMaterial()) {
				onRenderMode.meshRenderer.sharedMaterial = mainBuffer.GetMaterial();
			}
			
			if (onRenderMode.meshRenderer.sharedMaterial == null) {
				Debug.Log(mainBuffer.GetMaterial());
				onRenderMode.meshRenderer.sharedMaterial = mainBuffer.GetMaterial();
			}
		}
	}

	static public void PreRender(Camera camera) {
		if (IsReady(camera) == false) {
			return;
		}

		LightingDebug.LightMainCameraUpdates +=1;

		Quaternion rotation = camera.transform.rotation;

		LightingMainBuffer2D mainBuffer = LightingMainBuffer2D.Get(camera);

		Graphics.DrawMesh(LightingRender.GetMesh(), Matrix4x4.TRS(GetPosition(camera), rotation, GetSize(camera)), mainBuffer.GetMaterial(), 0);
	}
}