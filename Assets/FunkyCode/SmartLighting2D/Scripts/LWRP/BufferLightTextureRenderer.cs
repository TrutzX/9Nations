using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BufferLightTextureRenderer : MonoBehaviour {
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;

    public void Update() {
        if (Lighting2D.renderingPipeline == Lighting2D.RenderingPipeline.Scriptable) {
            meshRenderer.enabled = true;
        } else {
            meshRenderer.enabled = false;
        }
    }

    public MeshRenderer GetMeshRenderer() {
        if (meshRenderer == null) {
            meshRenderer = GetComponent<MeshRenderer>();
            if (meshRenderer == null) {
                meshRenderer = gameObject.AddComponent<MeshRenderer>();
            }
        }

        return(meshRenderer);
    }

    public MeshFilter GetMeshFilter() {
        if (meshFilter == null) {
            meshFilter = GetComponent<MeshFilter>();
            if (meshFilter == null) {
                meshFilter = gameObject.AddComponent<MeshFilter>();
            }
        }

        return(meshFilter);
    }

    // Lighting Sprite Renderer
	public Mesh mesh = null;
	public Mesh GetMesh() {
		if (mesh == null) {
			mesh = new Mesh();

			mesh.vertices = new Vector3[]{new Vector3(-1, -1), new Vector3(1, -1), new Vector3(1, 1), new Vector3(-1, 1)};
			mesh.triangles = new int[]{0, 1, 2, 2, 3, 0};
			mesh.uv = new Vector2[]{new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1)};
		}
		return(mesh);
	}
}
