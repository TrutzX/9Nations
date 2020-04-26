using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[ExecuteInEditMode]
public class LightingBuffer2D : MonoBehaviour {
	public RenderTexture renderTexture;
	public int textureSize = 0;

	public LightingSource2D lightSource;
	public Camera bufferCamera;

	public List<PartiallyBatchedCollider> partiallyBatchedList_Collider = new List<PartiallyBatchedCollider>();
	public List<PartiallyBatchedTilemap> partiallyBatchedList_Tilemap = new List<PartiallyBatchedTilemap>();

	public bool free = true;

	public static List<LightingBuffer2D> list = new List<LightingBuffer2D>();

	public BufferShadowRenderer shadowRenderer = null;
	public BufferLightTextureRenderer lightTextureRenderer = null;
	
	public bool LWRP_disable = false;

	public BufferLightTextureRenderer GetLightTextureRenderer() {
		if (lightTextureRenderer == null) {
			BufferLightTextureRenderer lightTexture;
			foreach(Transform t in transform) {
				lightTexture = t.GetComponent<BufferLightTextureRenderer>();
				if (lightTexture != null) {
					lightTextureRenderer = lightTexture;
					return(lightTextureRenderer);
				}
			}

			GameObject newG = new GameObject("Light Texture");
			newG.transform.parent = transform;
			newG.transform.localPosition = Vector3.zero;
			newG.transform.localScale = Vector3.one;

			lightTexture = newG.AddComponent<BufferLightTextureRenderer>();

			lightTextureRenderer = lightTexture;
		}
		return(lightTextureRenderer);
	}

	public BufferShadowRenderer GetShadowRenderer() {
		if (shadowRenderer == null) {
			BufferShadowRenderer sr;
			foreach(Transform t in transform) {
				sr = t.GetComponent<BufferShadowRenderer>();
				if (sr != null) {
					shadowRenderer = sr;
					return(shadowRenderer);
				}
			}

			GameObject newG = new GameObject("Shadow Renderer");
			newG.transform.parent = transform;
			newG.transform.localPosition = Vector3.zero;
			newG.transform.localScale = Vector3.one;

			sr = newG.AddComponent<BufferShadowRenderer>();

			shadowRenderer = sr;
		}
		return(shadowRenderer);
	}

	public void OnEnable() {
		list.Add(this);
	}

	public void OnDisable() {
		list.Remove(this);
	}

	static public List<LightingBuffer2D> GetList() {
		return(list);
	}

	static public int GetCount() {
		return(list.Count);
	}

	public void Initiate (int textureSize) {
		SetUpRenderTexture (textureSize);
		SetUpCamera ();
	}

	void SetUpRenderTexture(int _textureSize) {
		textureSize = _textureSize;

		LightingDebug.NewRenderTextures ++;

		RenderTextureFormat format = RenderTextureFormat.DefaultHDR;

		if (Lighting2D.commonSettings.hdr == false) {
			format = Lighting2D.lightingSourceSettings.textureFormat;
		}
		
		renderTexture = new RenderTexture(textureSize, textureSize, 0, format);

		name = "Buffer " + GetCount() + " (size: " + textureSize + ")";

		if (Lighting2D.commonSettings.hdr) {
			name = "HDR " + name;
		}
	}

	void SetUpCamera() {
		bufferCamera = gameObject.AddComponent<Camera>();
		bufferCamera.clearFlags = CameraClearFlags.Color;
		bufferCamera.backgroundColor = Color.white;
		bufferCamera.cameraType = CameraType.Game;
		bufferCamera.orthographic = true;
		bufferCamera.targetTexture = renderTexture;
		bufferCamera.farClipPlane = 0.5f;
		bufferCamera.nearClipPlane = 0f;

		if (Lighting2D.commonSettings.hdr) {
			bufferCamera.allowHDR = true;
		} else {
			bufferCamera.allowHDR = false;
		}

		bufferCamera.allowMSAA = false;
		bufferCamera.enabled = false;
	}

	// Does not work
	void Update() {
		if (Lighting2D.commonSettings.hdr != bufferCamera.allowHDR) {
			bufferCamera.allowHDR = Lighting2D.commonSettings.hdr;
		}

		if (LWRP_disable) {
			LWRP_disable = false;

			//bufferCamera.enabled = false;
		}
	}

	void LateUpdate() {
		float cameraZ = -1000f;

		Camera camera = LightingManager2D.Get().GetCamera(0); ////////////////////// 0?

		if (camera != null) {
			cameraZ = camera.transform.position.z - 10 - GetCount();
		}

		bufferCamera.transform.position = new Vector3(0, 0, cameraZ);

		transform.rotation = Quaternion.Euler(0, 0, 0);

		if (Lighting2D.renderingPipeline == Lighting2D.RenderingPipeline.Scriptable) {
			LWRP_OnPostRender();
		}
	}

	public void LWRP_OnPostRender() {
		shadowRenderer = GetShadowRenderer();
		lightTextureRenderer = GetLightTextureRenderer();

		if (shadowRenderer == null) {
			return;
		}

		if (lightTextureRenderer == null) {
			return;
		}

		LightingBufferShadow.FillBlack.Calculate();
		LightingBufferShadow.Penumbra.Calculate();

		List<LightingCollider2D> colliderList = LightingCollider2D.GetList();
		float z = transform.position.z;

		for(int id = 0; id < colliderList.Count; id++) {

			LightingBufferCollider.LWRP.Shadow(this, colliderList[id], z, shadowRenderer);
			

		}
		shadowRenderer.Export();
		
		lightTextureRenderer.GetMeshRenderer();
		lightTextureRenderer.GetMeshFilter();

		lightTextureRenderer.meshRenderer.sharedMaterial = LightingManager2D.Get().materials.GetMultiply();

		Sprite lightSprite = lightSource.GetSprite();

		if (lightSprite.texture != null) {
			lightTextureRenderer.meshRenderer.sharedMaterial.mainTexture = lightSprite.texture;
		}

		float size = bufferCamera.orthographicSize;

		lightTextureRenderer.transform.localScale = new Vector3(size, size, 1);
		
		lightTextureRenderer.meshFilter.sharedMesh = lightTextureRenderer.GetMesh();

		LWRP_disable = true;
	}
	
	public void OnPostRender() {
		if (Lighting2D.renderingPipeline != Lighting2D.RenderingPipeline.Standard) {
			return;
		}

		LateUpdate ();

		LightingBufferShadow.FillWhite.Calculate();
		LightingBufferShadow.Penumbra.Calculate();
		LightingBufferShadow.Setup.Calculate(this);

		GL.PushMatrix();

		for (int layerID = 0; layerID < lightSource.layerSetting.Length; layerID++) {
			if (lightSource.layerSetting == null || lightSource.layerSetting.Length <= layerID) {
				continue;
			}

			LayerSetting layerSetting = lightSource.layerSetting[layerID];

			if (layerSetting == null) {
				continue;
			}

			if (lightSource.enableCollisions) {	
				if (layerSetting.renderingOrder == LightingLayerOrder.Default) {
					Default.Draw(this, layerSetting);
				} else {
					Sorted.Draw(this, layerSetting);
				}
			}
		}
	
		LightingSourceTexture.Draw(this);

		GL.PopMatrix();

		LightingDebug.LightBufferUpdates ++;
		LightingDebug.totalLightUpdates ++;

		bufferCamera.enabled = false;
	}

	class Default {
		static public void Draw(LightingBuffer2D buffer, LayerSetting layer) {
			LightingBufferDefault.Draw(buffer, layer);
		}
	}

	class Sorted {
		static public void Draw(LightingBuffer2D buffer, LayerSetting layer) {
			LightingBufferSorted.Draw(buffer, layer);
		}
	}	
}