using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] 
public class BufferShadowRenderer : MonoBehaviour {
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;

    public Mesh mesh;	
    public int triangleCount = 0;
    public List<Vector3> verticesList;
    public List<Vector2> uvList;
    public List<int> trianglesList;

    public void Update() {
        if (Lighting2D.renderingPipeline == Lighting2D.RenderingPipeline.Scriptable) {
            meshRenderer.enabled = true;
        } else {
            meshRenderer.enabled = false;
        }
    }

    public void Awake() {
        mesh = new Mesh();
        verticesList = new List<Vector3>();
        uvList = new List<Vector2>();
        trianglesList = new List<int>();
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

    public void Export() {
        if (verticesList.Count > 0) {
            mesh.vertices = verticesList.ToArray();
            mesh.uv = uvList.ToArray();
            mesh.triangles = trianglesList.ToArray();
        } else {
            mesh.Clear();
            triangleCount = 0;
        }
        
        MeshFilter filter = GetMeshFilter();
        MeshRenderer renderer = GetMeshRenderer();

        renderer.sharedMaterial = LightingManager2D.Get().materials.GetAtlasMaterial();
        filter.sharedMesh = mesh;

        verticesList.Clear();
        uvList.Clear();
        trianglesList.Clear();
        triangleCount = 0;
    }

}
