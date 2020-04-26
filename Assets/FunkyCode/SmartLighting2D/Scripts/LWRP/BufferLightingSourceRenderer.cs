using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] 
public class BufferLightingSourceRenderer : MonoBehaviour {
    public static List<BufferLightingSourceRenderer> list = new List<BufferLightingSourceRenderer>();

    public bool free = true;

    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;

	public void OnEnable() {
		list.Add(this);
	}

	public void OnDisable() {
		list.Remove(this);
	}

    public void Update() {
        if (Lighting2D.renderingPipeline == Lighting2D.RenderingPipeline.Scriptable) {
            meshRenderer.enabled = true;
        } else {
            meshRenderer.enabled = false;
        }
    }

	static public List<BufferLightingSourceRenderer> GetList() {
		return(list);
	}

    static public void FreeAll() {
        foreach(BufferLightingSourceRenderer source in list) {
             source.free = true;
        }
    }

    static public BufferLightingSourceRenderer GetFree() {
        foreach(BufferLightingSourceRenderer source in list) {
            if (source.free) {
                return(source);
            }
        }

        return(null);
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
