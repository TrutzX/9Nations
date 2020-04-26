using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DayLightingShadowHeight {
	public int height = 0;

	public DayLightingShadowHeight (int setHeight) {
		height = setHeight;
	}

	private CustomPhysicsShape customPhysicsShape = null;
	float sunDirection = 0;

	private Sprite originalSprite;

	public List<Polygon2D> polygons = new List<Polygon2D>();
	public List<List<DoublePair2D>> polygonsPairs = new List<List<DoublePair2D>>();
	public List<Mesh> meshes = new List<Mesh>();
	public List<Vector2D> meshVertices = new List<Vector2D>();
	public List<int> meshTriangles = new List<int>();

	public void SetOriginalSprite(Sprite sprite) {
		originalSprite = sprite;
	}

	public CustomPhysicsShape GetPhysicsShape() {
		if (customPhysicsShape == null) {
			customPhysicsShape = CustomPhysicsShapeManager.RequesCustomShape(originalSprite);
		}
		return(customPhysicsShape);
	}

	public void Update(float direction) {
		if (direction == sunDirection) {
			return;
		}
		
		float dir = direction * Mathf.Deg2Rad;

		sunDirection = direction;

		polygons.Clear();
		meshes.Clear();
		polygonsPairs.Clear();
		meshVertices.Clear();
		meshTriangles.Clear();

		Polygon2D poly;
		Mesh mesh;

		List<Polygon2D> polys = GetPhysicsShape().Get();

		foreach(Polygon2D polygon in polys) {
			if (polygon.pointsList.Count < 2) {
				continue;
			}
			poly = Polygon2D.GenerateShadow(polygon, dir, height);

			polygons.Add(poly.Copy());
			polygonsPairs.Add(DoublePair2D.GetList(poly.pointsList));

			mesh = poly.CreateMesh(Vector2.zero, Vector2.zero);

			meshes.Add(mesh);

			LightingDebug.ConvexHullGenerations ++;
		}

		foreach(Mesh m in meshes) {						
			for (int i = 0; i < m.vertices.GetLength (0); i ++) {
				meshVertices.Add(new Vector2D(m.vertices [i]));
			}
			for (int i = 0; i < m.triangles.GetLength (0); i ++) {
				meshTriangles.Add(m.triangles [i]);
			}
		}
	}
}

[System.Serializable]
public class DayLightingShadowShape {
	public Sprite sprite;
	
	public Dictionary<int, DayLightingShadowHeight> dictionary_heights = new Dictionary<int, DayLightingShadowHeight>();


	static public Dictionary<Sprite, DayLightingShadowShape> dictionary_shape = new Dictionary<Sprite, DayLightingShadowShape>();
	








	public DayLightingShadowHeight RequestHeight(int height) {
		DayLightingShadowHeight shapeHeight = null;

		bool exist = dictionary_heights.TryGetValue(height, out shapeHeight);

		if (exist) {
			//if (shapeHeights == null || shapeHeights.sprite.texture == null) {
				//dictionary_heights.Remove(sprite);

				//shapeHeights = AddShape(sprite);

				//..dictionary_heights.Add(sprite, shapeHeights);
			//} 
			shapeHeight.SetOriginalSprite(sprite);
		
			return(shapeHeight);
		} else {		
			shapeHeight = new DayLightingShadowHeight(height);

			dictionary_heights.Add(height, shapeHeight);

			shapeHeight.SetOriginalSprite(sprite);

			return(shapeHeight);
		}
	}










	static public DayLightingShadowShape RequestShape(Sprite sprite) {
		DayLightingShadowShape shape = null;

		if (sprite == null) {
			Debug.Log("Sprite is Null");
			return(null);
		}

		

		bool exist = dictionary_shape.TryGetValue(sprite, out shape);

		if (exist) {
			if (shape == null || shape.sprite.texture == null) {
				dictionary_shape.Remove(sprite);

				shape = AddShape(sprite);

				dictionary_shape.Add(sprite, shape);
			} 
			return(shape);
		} else {		
			shape = AddShape(sprite);

			dictionary_shape.Add(sprite, shape);

			return(shape);
		}
	}

	static private DayLightingShadowShape AddShape(Sprite sprite) {
        if (sprite == null || sprite.texture == null) {
            return(null);
        }

        DayLightingShadowShape shape = new DayLightingShadowShape();
        shape.sprite = sprite;

        return(shape);
    }
}

/*
[System.Serializable]
public class DayLightingShadowCollider {
	public float sunDirection;



	public void Update(float direction, float height, LightingCollider2D id) {
		
	}
} */