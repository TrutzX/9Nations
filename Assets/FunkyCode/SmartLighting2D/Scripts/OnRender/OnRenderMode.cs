using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class OnRenderMode : MonoBehaviour {
    public Camera regularCamera;
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;

    public static List<OnRenderMode> list = new List<OnRenderMode>();

	public void OnEnable() {
		list.Add(this);
	}

	public void OnDisable() {
		list.Remove(this);
	}

    public static OnRenderMode Get(Camera camera, LightingMainBuffer2D mainBuffer) {
		foreach(OnRenderMode meshModeObject in list) {
			if (meshModeObject.regularCamera == camera) {
                return(meshModeObject);
            }
		}

        GameObject meshRendererMode = new GameObject("On Render");
        OnRenderMode onRenderMode = meshRendererMode.AddComponent<OnRenderMode>();

        onRenderMode.regularCamera = camera;
        onRenderMode.Initialize(camera, mainBuffer);

        return(onRenderMode);
    }

    public void Initialize(Camera camera, LightingMainBuffer2D mainBuffer) {         
        transform.parent = mainBuffer.transform;
        
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = LightingMainBuffer2D.Get(camera).GetMaterial();
           
        meshRenderer.sortingLayerName = Lighting2D.sortingLayer.Name;
        meshRenderer.sortingLayerID = Lighting2D.sortingLayer.ID;
        meshRenderer.sortingOrder = Lighting2D.sortingLayer.Order;

        // Disable Mesh Renderer Settings
        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        meshRenderer.receiveShadows = false;
        meshRenderer.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
        meshRenderer.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
        meshRenderer.allowOcclusionWhenDynamic = false;

        UpdatePosition();

        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = LightingRender.GetMesh();
    }

    public void Update() {
        if (regularCamera == null) {
            DestroyImmediate(gameObject);
        }

         if (LightingManager2D.Get().disableEngine) {
            if (meshRenderer != null) {
				meshRenderer.enabled = false;
			}
        }

        if (Lighting2D.commonSettings.darknessBuffer == false) {
			meshRenderer.enabled = false;
		}
			
		if (Lighting2D.renderingMode != Lighting2D.RenderingMode.OnRender) {
			meshRenderer.enabled = false;
		}
    }

    void LateUpdate() {
		if (Lighting2D.renderingMode == Lighting2D.RenderingMode.OnRender) {
            UpdatePosition();
        }
    }

    public void UpdatePosition() {
        Camera camera = regularCamera;
        if (camera == null) {
            return;
        }
        
        Vector3 position = camera.transform.position;
        position.z += camera.nearClipPlane + 0.1f;

        transform.position = position;
        transform.rotation = camera.transform.rotation;
        transform.localScale = Lighting2DRender.GetSize(regularCamera);
    }
}