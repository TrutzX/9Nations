using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CustomPhysicsShape {
    private List<Polygon2D> polygons = null; 
	private MeshObject shapeMeshObject = null;
    private Sprite sprite;

    public List<Polygon2D> Get() {
        if (polygons == null) {
            Generate();
        }
        return(polygons);
    }

    private void Generate() {
        polygons = new List<Polygon2D>();

		#if UNITY_2018_1_OR_NEWER

			int count = sprite.GetPhysicsShapeCount();

			List<Vector2> points;
			Polygon2D newPolygon;

			for(int i = 0; i < count; i++) {
				points = new List<Vector2>();
				sprite.GetPhysicsShape(i, points);
				
				newPolygon = new Polygon2D();

				for(int id = 0; id < points.Count; id++) {
					newPolygon.AddPoint(points[id]);
				}

				polygons.Add(newPolygon);
			}

			LightingDebug.totalCustomPhysicsShapes++;
		#endif
	}

	public void SetSprite(Sprite newSprite) {
		sprite = newSprite;
	}

	public Sprite GetSprite() {
		return(sprite);
	}

    public MeshObject GetMesh() {
		if (shapeMeshObject == null) {
			if (polygons.Count > 0) {
				shapeMeshObject = new MeshObject(polygons[0].CreateMesh(Vector2.zero, Vector2.zero));	
				LightingDebug.totalObjectMaskMeshGenerations ++;
			}
		}
		return(shapeMeshObject);
	}
}
