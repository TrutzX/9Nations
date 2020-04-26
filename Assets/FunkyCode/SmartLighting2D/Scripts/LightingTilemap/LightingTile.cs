using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_2018_1_OR_NEWER
[System.Serializable]
public class LightingTile {
	private Sprite originalSprite;
	private Sprite atlasSprite;

	public CustomPhysicsShape customPhysicsShape = null;

	private List<Polygon2D> shapePolygons = null;
	private MeshObject shapeMesh = null;

	public List<Polygon2D> world_polygon = null;
	public List<List<Pair2D>> world_polygonPairs = null;

	public static MeshObject staticTileMesh = null;


	public void SetOriginalSprite(Sprite sprite) {
		originalSprite = sprite;
	}

	public Sprite GetOriginalSprite() {
		return(originalSprite);
	}

	public Sprite GetAtlasSprite() {
		return(atlasSprite);
	}

	public void SetAtlasSprite(Sprite sprite) {
		atlasSprite = sprite;
	}

	public bool InRange(Vector2 pos, Vector2 sourcePos, float sourceSize) {
		return(Vector2.Distance(pos, sourcePos) > 2 + sourceSize);
	}

	public List<Polygon2D> GetPolygons(LightingTilemapCollider2D tilemap) {
		if (world_polygon == null) {
			if (tilemap.colliderType == LightingTilemapCollider2D.ColliderType.SpriteCustomPhysicsShape) {
				if (GetShapePolygons().Count < 1) {
					return(null);
				}
				
				world_polygon = GetShapePolygons(); //poly.ToScaleItself(defaultSize); // scale?
				
			} else {
				// Rectangle
				Vector2 size = tilemap.cellSize * 0.5f;

				world_polygon = new List<Polygon2D>();

				switch(tilemap.mapType) {
					case LightingTilemapCollider2D.MapType.UnityEngineTilemapRectangle:
					case LightingTilemapCollider2D.MapType.SuperTilemapEditor:
						world_polygon.Add(Polygon2D.CreateRect(size));
					break;

					case LightingTilemapCollider2D.MapType.UnityEngineTilemapIsometric:
						world_polygon.Add(Polygon2D.CreateIsometric(size));
					break;

				}
			}
		}
		return(world_polygon);
	}

	public List<List<Pair2D>> GetPairs(LightingTilemapCollider2D tilemap) {
		if (world_polygonPairs == null) {
			world_polygonPairs = new List<List<Pair2D>>();

			if (GetPolygons(tilemap) == null) {
				return(world_polygonPairs);
			}

			foreach (Polygon2D poly in GetPolygons(tilemap)) {
				world_polygonPairs.Add(Pair2D.GetList(poly.pointsList));
			}
		}
		return(world_polygonPairs);
	}

	public List<Polygon2D> GetShapePolygons() {
		if (shapePolygons == null) {
			shapePolygons = new List<Polygon2D>();

			if (originalSprite == null) {
				return(shapePolygons);
			}

			#if UNITY_2018_1_OR_NEWER
				if (customPhysicsShape == null) {
					customPhysicsShape = CustomPhysicsShapeManager.RequesCustomShape(originalSprite);
				}
				shapePolygons = customPhysicsShape.Get();
			#endif
		}
		return(shapePolygons);
	}

	public MeshObject GetTileDynamicMesh() {
		if (shapeMesh == null) {
			if (shapePolygons != null && shapePolygons.Count > 0) {
				shapeMesh = customPhysicsShape.GetMesh();
			}
		}
		return(shapeMesh);
	}

	public static MeshObject GetStaticTileMesh(LightingTilemapCollider2D tilemap) {
		if (staticTileMesh == null) {
			// Can be optimized?
			Mesh mesh = new Mesh();

			float x = 0.5f + 0.01f;
			float y = 0.5f + 0.01f;

			switch(tilemap.mapType) {
				case LightingTilemapCollider2D.MapType.UnityEngineTilemapRectangle:
				case LightingTilemapCollider2D.MapType.SuperTilemapEditor:
					mesh.vertices = new Vector3[]{new Vector2(-x, -y), new Vector2(x, -y), new Vector2(x, y), new Vector2(-x, y)};
				break;

				case LightingTilemapCollider2D.MapType.UnityEngineTilemapIsometric:
					mesh.vertices = new Vector3[]{new Vector2(0, y), new Vector2(x, y / 2), new Vector2(0, 0), new Vector2(-x, y / 2)};
				break;
				

			}

			mesh.triangles = new int[]{0, 1, 2, 2, 3, 0};
			mesh.uv = new Vector2[]{new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1)};
			
			staticTileMesh = new MeshObject(mesh);	
		}
		return(staticTileMesh);
	}
}
#endif