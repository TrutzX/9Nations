using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColliderLineRenderer2D : MonoBehaviour {
	public bool customColor = false;
	public Color color = Color.white;
	public float lineWidth = 1;

	private bool edgeCollider = false; // For Edge Collider

	private Polygon2D polygon = null;
	private Mesh mesh = null;
	private float lineWidthSet = 1;

	private SmartMaterial material = null;
	private static SmartMaterial staticMaterial = null;

	const float lineOffset = -0.01f;

	public SmartMaterial GetMaterial() {
		if (material == null || material.material == null) {
			material = Utilities.MaterialManager.GetVertexLitCopy();
		}

		return(material);
	}

	public SmartMaterial GetStaticMaterial() {
		if (staticMaterial == null || staticMaterial.material == null)   {
			staticMaterial = Utilities.MaterialManager.GetVertexLitCopy();
			staticMaterial.SetColor(Color.black);
			//staticMaterial.color =  ("_Emission", Color.black);
		}
		
		return(staticMaterial);
	}

	public void Initialize() {
		polygon = null;
		mesh = null;

		if (GetComponent<EdgeCollider2D>() != null) {
			edgeCollider = true;
		} else {
			edgeCollider = false;
		}

		GenerateMesh();
		Draw();
	}

	void Start () {
		Initialize();
	}

	void OnDestroy() {
		if (mesh != null) {
			DestroyImmediate(mesh);
		}
	}
	
	public void LateUpdate() {
		if (lineWidth != lineWidthSet) {
			if (lineWidth < 0.01f) {
				lineWidth = 0.01f;
			}
			GenerateMesh();
		}

		Draw();
	}

	public Polygon2D GetPolygon() {
		if (polygon == null) {
			polygon = Polygon2DList.CreateFromGameObject (gameObject)[0];
		}
		return(polygon);
	}

	public void GenerateMesh() {
		lineWidthSet = lineWidth;

		if (mesh != null) {
			DestroyImmediate(mesh);
		}

		mesh = Max2DMesh.CreatePolygon(transform, GetPolygon(), lineOffset, lineWidth, edgeCollider == false);
	}

	public void Draw() {
		SmartMaterial mat;
		
		if (customColor) {
			mat = GetMaterial();
			if (mat != null) {
				mat.SetColor(color);
				Max2DMesh.Draw(mesh, transform, mat.material);
			}
		} else {
			mat = GetStaticMaterial();
			if (mat != null) {
				Max2DMesh.Draw(mesh, transform, mat.material);
			}
		}
	}
}