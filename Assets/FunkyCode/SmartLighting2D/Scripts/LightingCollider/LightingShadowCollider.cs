using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MeshObject {
	public Mesh mesh;
	public Vector3[] vertices;
	public Vector2[] uv;
	public int[] triangles;

	public MeshObject(Mesh meshOrigin) {
		vertices = meshOrigin.vertices;
		uv = meshOrigin.uv;
		triangles = meshOrigin.triangles;

		mesh = meshOrigin;
	}
 }

[System.Serializable]
public class MeshVertices {
	public List<MeshVertice> veclist = new List<MeshVertice>();
	public List<MeshUV> uvList = new List<MeshUV>();
}

[System.Serializable]
public class MeshVertice {
	public Vector2D a;
	public Vector2D b;
	public Vector2D c;
}

[System.Serializable]
public class MeshUV {
	public Vector2 a;
	public Vector2 b;
	public Vector2 c;
}

[System.Serializable]
public class LightingShape {
	public List<List<Pair2D>> polygons_world_pair = null;
	public List<List<Pair2D>> polygons_world_pair_cache = null;
	
	public List<Polygon2D> polygons_world = null;
	public List<Polygon2D> polygons_world_cache = null;

	public List<Polygon2D> polygons_local = null;

	public MeshVertices mesh_vertices = null;
	public MeshVertices mesh_vertices_cache = null;

	public float meshDistance = -1f;
	public MeshObject mesh = null;

	public bool edgeCollider2D = false;

	public void ResetLocal() {
		polygons_local = null;
		meshDistance = -1f;
		mesh = null;

		ResetWorld();
	}

	public void ResetWorld() {
		polygons_world_pair = null;
		polygons_world = null;
		mesh_vertices = null;
	}
}

[System.Serializable]
public class LightingColliderShape {
	public LightingCollider2D.ColliderType colliderType = LightingCollider2D.ColliderType.Collider;
	public LightingCollider2D.MaskType maskType = LightingCollider2D.MaskType.Sprite;

	private LightingShape colliderShape = new LightingShape();
	private LightingShape spriteShape = new LightingShape();
	private LightingShape meshShape = new LightingShape();
	private LightingShape skinnedMeshShape = new LightingShape();

	private CustomPhysicsShape customPhysicsShape = null;

	public LightingCollider2D lightingCollider2D;

	public void SetLightingCollider2D(LightingCollider2D id) {
		lightingCollider2D = id;
	}

	public CustomPhysicsShape GetPhysicsShape() {
		if (customPhysicsShape == null) {
			customPhysicsShape = CustomPhysicsShapeManager.RequesCustomShape(originalSprite);
		}
		return(customPhysicsShape);
	}

	private Sprite originalSprite;
	private Sprite atlasSprite;

	public Sprite GetOriginalSprite() {
		return(originalSprite);
	}

	public Sprite GetAtlasSprite() {
		return(atlasSprite);
	}

	public void SetAtlasSprite(Sprite sprite) {
		atlasSprite = sprite;
	}

	public void SetOriginalSprite(Sprite sprite) {
		originalSprite = sprite;
	}

	public void ResetLocal() {
		colliderShape.ResetLocal();

		spriteShape.ResetLocal();

		customPhysicsShape = null;

		ResetWorld();
	}

	public void ResetWorld() {
		colliderShape.ResetWorld();
		spriteShape.ResetWorld();
		meshShape.ResetWorld();
		skinnedMeshShape.ResetWorld();
	}

	public bool IsEdgeCollider() {
		switch(colliderType) {
			case LightingCollider2D.ColliderType.Collider:
				return(colliderShape.edgeCollider2D);
			case LightingCollider2D.ColliderType.SpriteCustomPhysicsShape:
				return(false);
		}
		return(false);
	}

	////////////////////////////////////////////////////////////////////////////// Frustum
	public float GetFrustumDistanceSet() {
		switch(colliderType) {
			case LightingCollider2D.ColliderType.Collider:
				return(colliderShape.meshDistance);
			case LightingCollider2D.ColliderType.SpriteCustomPhysicsShape:
				return(spriteShape.meshDistance);
		}
		return(1000f);
	}

	
	public float GetFrustumDistance(Transform transform) {
		float sx = Mathf.Abs(transform.localScale.x);
		float sy = Mathf.Abs(transform.localScale.y);

		float multiplier = Mathf.Max(sx, sy);

		switch(colliderType) {
			case LightingCollider2D.ColliderType.Collider:
				return(GetFrustumDistance_Collider(transform) * multiplier);
			case LightingCollider2D.ColliderType.SpriteCustomPhysicsShape:
				return(GetFrustumDistance_Shape() * multiplier);
		}
		return(1000f);
	}
	
	public float GetFrustumDistance_Collider(Transform transform) {
		if (colliderShape.meshDistance < 0) {
			colliderShape.meshDistance = 0;
			if (GetPolygons_Collider_Local(transform).Count > 0) {
				foreach (Vector2D id in GetPolygons_Collider_Local(transform)[0].pointsList) {
					colliderShape.meshDistance = Mathf.Max(colliderShape.meshDistance, Vector2.Distance(id.ToVector2(), Vector2.zero));
				}
			}
		}
		return(colliderShape.meshDistance);
	}

	public float GetFrustumDistance_Shape() {
		if (spriteShape.meshDistance < 0) {
			spriteShape.meshDistance = 0;
			if (GetPolygons_Shape_Local().Count > 0) {
				foreach (Vector2D id in GetPolygons_Shape_Local()[0].pointsList) {
					spriteShape.meshDistance = Mathf.Max(spriteShape.meshDistance, Vector2.Distance(id.ToVector2(), Vector2.zero));
				}
			}
		}
		return(spriteShape.meshDistance);
	}

	//////////////////////////////////////////////////////////////////////////////// Pair List 
	public List<List<Pair2D>> GetPolygons_Pair_World_ColliderType(Transform transform, VirtualSpriteRenderer spriteRenderer) {
		switch(colliderType) {
			case LightingCollider2D.ColliderType.Collider:
				return(GetPolygons_Pair_Collider_World(transform));

			case LightingCollider2D.ColliderType.SpriteCustomPhysicsShape:
				return(GetPolygons_Pair_Shape_World(transform, spriteRenderer));
		}
		return(null);
	}

	public List<List<Pair2D>> GetPolygons_Pair_Collider_World(Transform transform) {
		if (colliderShape.polygons_world_pair == null) {
			if (colliderShape.polygons_world_pair_cache != null) {
				colliderShape.polygons_world_pair = colliderShape.polygons_world_pair_cache;
				
				// Recalculate Cache !!!!!

			} else {
				LightingDebug.ShadowColliderTotalGenerationsWorld_collider_pair ++;

				colliderShape.polygons_world_pair = new List<List<Pair2D>>();
				foreach(Polygon2D poly in GetPolygons_Collider_World(transform)) {
					colliderShape.polygons_world_pair.Add(Pair2D.GetList(poly.pointsList, colliderShape.edgeCollider2D == false));
				}

				colliderShape.polygons_world_pair_cache = colliderShape.polygons_world_pair;
			}
		}

		return(colliderShape.polygons_world_pair);
	}

	public List<List<Pair2D>> GetPolygons_Pair_Shape_World(Transform transform, VirtualSpriteRenderer spriteRenderer) {
		if (spriteShape.polygons_world_pair == null) {
			
			if (spriteShape.polygons_world_pair_cache != null) {
				GetPolygons_Shape_World(transform, spriteRenderer);					
			}

			if (spriteShape.polygons_world_pair_cache != null) {
				spriteShape.polygons_world_pair = spriteShape.polygons_world_pair_cache;
		
				
			} else {
				spriteShape.polygons_world_pair = new List<List<Pair2D>>();

				foreach(Polygon2D poly in GetPolygons_Shape_World(transform, spriteRenderer)) {
					spriteShape.polygons_world_pair.Add(Pair2D.GetList(poly.pointsList));
				}

				spriteShape.polygons_world_pair_cache = spriteShape.polygons_world_pair;
			}
		}

		return(spriteShape.polygons_world_pair);
	}

	/////////////////////////////////////////// Mesh Object
	public MeshObject GetMesh_Collider(Transform transform) {
       if (colliderShape.mesh == null) {
		   List<Polygon2D> polygons = GetPolygons_Collider_Local(transform);
		   
			if (polygons.Count > 0) {
				if (polygons[0].pointsList.Count > 2) {
					// Triangulate Polygon List?
					Mesh mesh = PolygonTriangulator2D.Triangulate (polygons[0], Vector2.zero, Vector2.zero, PolygonTriangulator2D.Triangulation.Advanced);
					colliderShape.mesh = new MeshObject(mesh);
				}
			}
		}
		return(colliderShape.mesh);
    }

	public MeshObject GetMesh_Shape() {
        if (spriteShape.mesh == null) {
            if (GetPolygons_Shape_Local().Count > 0) {
                if (GetPolygons_Shape_Local()[0].pointsList.Count > 2) {
                    // Triangulate Polygon List?
                    spriteShape.mesh = GetPhysicsShape().GetMesh();
                }
            }
           LightingDebug.ConvexHullGenerations ++;
        }
        return(spriteShape.mesh);
    }

	public MeshObject GetMesh_Mesh() {
       if (colliderShape.mesh == null) {
		   if (lightingCollider2D.meshFilter != null) {
				Mesh mesh = lightingCollider2D.meshFilter.sharedMesh;
				colliderShape.mesh = new MeshObject(mesh);
		   }
		}
		return(colliderShape.mesh);
    }

	public MeshObject GetMesh_SkinnedMesh() {
       if (skinnedMeshShape.mesh == null) {
		   if (lightingCollider2D.skinnedMeshRenderer != null) {
				Mesh mesh = lightingCollider2D.skinnedMeshRenderer.sharedMesh;
				skinnedMeshShape.mesh = new MeshObject(mesh);
		   }
		}
		return(skinnedMeshShape.mesh);
    }

	//////////////////////////////////////////// Mesh Vertices
	public MeshVertices GetMesh_Vertices_MaskType(Transform transform) {
		switch(maskType) {
			case LightingCollider2D.MaskType.Collider:
				return(GetMesh_Vertices_Collider(transform));

			case LightingCollider2D.MaskType.SpriteCustomPhysicsShape:
				return(GetMesh_Vertices_Shape(transform));
	
			case LightingCollider2D.MaskType.Mesh:
				return(GetMesh_Vertices_Mesh(transform));

			case LightingCollider2D.MaskType.SkinnedMesh:
				return(GetMesh_Vertices_SkinnedMesh(transform));
		}
		return(null);
	}

	public MeshVertices GetMesh_Vertices_SkinnedMesh(Transform transform) {
		if (skinnedMeshShape.mesh_vertices != null) {
			return(skinnedMeshShape.mesh_vertices);
		}

		MeshObject meshObject = GetMesh_SkinnedMesh();

		if (meshObject == null) {
			skinnedMeshShape.mesh_vertices = new MeshVertices();
			return(skinnedMeshShape.mesh_vertices);
		}

		MeshUV uv;

		if (skinnedMeshShape.mesh_vertices_cache == null) {
			skinnedMeshShape.mesh_vertices = new MeshVertices();

			MeshVertice vertice;

			for (int i = 0; i < meshObject.triangles.GetLength (0); i = i + 3) {
				vertice = new MeshVertice();
				vertice.a = new Vector2D(transform.TransformPoint(meshObject.vertices [meshObject.triangles [i]]));
				vertice.b = new Vector2D(transform.TransformPoint(meshObject.vertices [meshObject.triangles [i + 1]]));
				vertice.c = new Vector2D(transform.TransformPoint(meshObject.vertices [meshObject.triangles [i + 2]]));

				skinnedMeshShape.mesh_vertices.veclist.Add(vertice);

				uv = new MeshUV();
				uv.a = meshObject.uv [meshObject.triangles [i]];
				uv.b = meshObject.uv [meshObject.triangles [i + 1]];
				uv.c = meshObject.uv [meshObject.triangles [i + 2]];

				skinnedMeshShape.mesh_vertices.uvList.Add(uv);
			}

			skinnedMeshShape.mesh_vertices_cache = skinnedMeshShape.mesh_vertices;
		} else {
			skinnedMeshShape.mesh_vertices = skinnedMeshShape.mesh_vertices_cache;
			
			int count = 0;
			Vector2 v;

			for (int i = 0; i < meshObject.triangles.GetLength (0); i = i + 3) {
				v =  transform.TransformPoint(meshObject.vertices [meshObject.triangles [i]]); 
				skinnedMeshShape.mesh_vertices.veclist[count].a.x = v.x;
				skinnedMeshShape.mesh_vertices.veclist[count].a.y = v.y;

				v =  transform.TransformPoint(meshObject.vertices [meshObject.triangles [i + 1]]);
				skinnedMeshShape.mesh_vertices.veclist[count].b.x = v.x;
				skinnedMeshShape.mesh_vertices.veclist[count].b.y = v.y;

				v =  transform.TransformPoint(meshObject.vertices [meshObject.triangles [i + 2]]);
				skinnedMeshShape.mesh_vertices.veclist[count].c.x = v.x;
				skinnedMeshShape.mesh_vertices.veclist[count].c.y = v.y;

				uv = skinnedMeshShape.mesh_vertices.uvList[count];
				uv.a = meshObject.uv [meshObject.triangles [i + 0]];
				uv.b = meshObject.uv [meshObject.triangles [i + 1]];
				uv.c = meshObject.uv [meshObject.triangles [i + 2]];
				
				count += 1;
			}
		}
		return(skinnedMeshShape.mesh_vertices);
    }

	public MeshVertices GetMesh_Vertices_Mesh(Transform transform) {
		if (meshShape.mesh_vertices != null) {
			return(meshShape.mesh_vertices);
		}

		MeshObject meshObject = GetMesh_Mesh();

		if (meshObject == null) {
			meshShape.mesh_vertices = new MeshVertices();
			return(meshShape.mesh_vertices);
		}

		MeshUV uv;

		if (meshShape.mesh_vertices_cache == null) {
			meshShape.mesh_vertices = new MeshVertices();

			MeshVertice vertice;

			for (int i = 0; i < meshObject.triangles.GetLength (0); i = i + 3) {
				vertice = new MeshVertice();
				vertice.a = new Vector2D(transform.TransformPoint(meshObject.vertices [meshObject.triangles [i]]));
				vertice.b = new Vector2D(transform.TransformPoint(meshObject.vertices [meshObject.triangles [i + 1]]));
				vertice.c = new Vector2D(transform.TransformPoint(meshObject.vertices [meshObject.triangles [i + 2]]));

				meshShape.mesh_vertices.veclist.Add(vertice);

				uv = new MeshUV();
				uv.a = meshObject.uv [meshObject.triangles [i]];
				uv.b = meshObject.uv [meshObject.triangles [i + 1]];
				uv.c = meshObject.uv [meshObject.triangles [i + 2]];

				meshShape.mesh_vertices.uvList.Add(uv);
			}

			meshShape.mesh_vertices_cache = meshShape.mesh_vertices;
		} else {
			meshShape.mesh_vertices = meshShape.mesh_vertices_cache;
			
			int count = 0;
			Vector2 v;

			for (int i = 0; i < meshObject.triangles.GetLength (0); i = i + 3) {
				v =  transform.TransformPoint(meshObject.vertices [meshObject.triangles [i]]); 
				meshShape.mesh_vertices.veclist[count].a.x = v.x;
				meshShape.mesh_vertices.veclist[count].a.y = v.y;

				v =  transform.TransformPoint(meshObject.vertices [meshObject.triangles [i + 1]]);
				meshShape.mesh_vertices.veclist[count].b.x = v.x;
				meshShape.mesh_vertices.veclist[count].b.y = v.y;

				v =  transform.TransformPoint(meshObject.vertices [meshObject.triangles [i + 2]]);
				meshShape.mesh_vertices.veclist[count].c.x = v.x;
				meshShape.mesh_vertices.veclist[count].c.y = v.y;

				uv = meshShape.mesh_vertices.uvList[count];
				uv.a = meshObject.uv [meshObject.triangles [i + 0]];
				uv.b = meshObject.uv [meshObject.triangles [i + 1]];
				uv.c = meshObject.uv [meshObject.triangles [i + 2]];
				
				count += 1;
			}
		}
		return(meshShape.mesh_vertices);
    }

	public MeshVertices GetMesh_Vertices_Collider(Transform transform) {
		if (colliderShape.mesh_vertices != null) {
			return(colliderShape.mesh_vertices);
		}

		MeshObject meshObject = GetMesh_Collider(transform);

		if (meshObject == null) {
			colliderShape.mesh_vertices = new MeshVertices();
			return(colliderShape.mesh_vertices);
		}

		if (colliderShape.mesh_vertices_cache == null) {
			colliderShape.mesh_vertices = new MeshVertices();

			Helper.MeshToVertices(colliderShape.mesh_vertices, meshObject, transform);

			colliderShape.mesh_vertices_cache = colliderShape.mesh_vertices;
		} else {
			colliderShape.mesh_vertices = colliderShape.mesh_vertices_cache;

			Helper.MeshToVerticesCached(colliderShape.mesh_vertices, meshObject, transform);
		}
		return(colliderShape.mesh_vertices);
    }

	public MeshVertices GetMesh_Vertices_Shape(Transform transform) {
        if (spriteShape.mesh_vertices != null) {
			 return(spriteShape.mesh_vertices);
		}
		
		MeshObject meshObject = GetMesh_Shape();

		if (meshObject == null) {
			colliderShape.mesh_vertices = new MeshVertices();
			return(colliderShape.mesh_vertices);
		}

		if (spriteShape.mesh_vertices_cache == null) {
			spriteShape.mesh_vertices = new MeshVertices();

			Helper.MeshToVertices(spriteShape.mesh_vertices, meshObject, transform);

			spriteShape.mesh_vertices_cache = spriteShape.mesh_vertices;
		} else {
			spriteShape.mesh_vertices = spriteShape.mesh_vertices_cache;

			Helper.MeshToVerticesCached(spriteShape.mesh_vertices, meshObject, transform);
		}
        return(spriteShape.mesh_vertices);
    }

	//////////////////////////////////////////// Polygon Objects
	public List<Polygon2D> GetPolygons_World_ColliderType(Transform transform, VirtualSpriteRenderer virtualSpriteRenderer = null) {
		switch(colliderType) {
			case LightingCollider2D.ColliderType.Collider:
				return(GetPolygons_Collider_World(transform));

			case LightingCollider2D.ColliderType.SpriteCustomPhysicsShape:
				return(GetPolygons_Shape_World(transform, virtualSpriteRenderer));

			case LightingCollider2D.ColliderType.Mesh:
				return(GetPolygons_Mesh_World(transform));

			case LightingCollider2D.ColliderType.SkinnedMesh:
				return(GetPolygons_SkinnedMesh_World(transform));
		}
		return(null);
	}

	public List<Polygon2D> GetPolygons_Local_ColliderType(Transform transform) {
		switch(colliderType) {
			case LightingCollider2D.ColliderType.Collider:
				return(GetPolygons_Collider_Local(transform));

			case LightingCollider2D.ColliderType.SpriteCustomPhysicsShape:
				return(GetPolygons_Shape_Local());
		}
		return(null);
	}

	public List<Polygon2D> GetPolygons_Collider_World(Transform transform) {
		if (colliderShape.polygons_world != null) {
			return(colliderShape.polygons_world);
		}

		if (colliderShape.polygons_world_cache != null) {
			LightingDebug.ShadowColliderTotalGenerationsWorld_collider_re ++;
			
			colliderShape.polygons_world = colliderShape.polygons_world_cache;

			Polygon2D poly;
			Vector2D point;
			List<Polygon2D> list = GetPolygons_Collider_Local(transform);
			for(int i = 0; i < list.Count; i++) {
				poly = list[i];
				for(int p = 0; p < poly.pointsList.Count; p++) {
					point = poly.pointsList[p];
					
					colliderShape.polygons_world[i].pointsList[p].x = point.x;
					colliderShape.polygons_world[i].pointsList[p].y = point.y;
				}
				colliderShape.polygons_world[i].ToWorldSpaceItself(transform);
			}

		} else {
			LightingDebug.ShadowColliderTotalGenerationsWorld_collider ++;

			colliderShape.polygons_world = new List<Polygon2D>();
			
			foreach(Polygon2D poly in GetPolygons_Collider_Local(transform)) {
				colliderShape.polygons_world.Add(poly.ToWorldSpace(transform));
			}

			colliderShape.polygons_world_cache = colliderShape.polygons_world;
		}
	
		return(colliderShape.polygons_world);
	}

	public List<Polygon2D> GetPolygons_SkinnedMesh_World(Transform transform) {
		if (skinnedMeshShape.polygons_world != null) {
			return(skinnedMeshShape.polygons_world); 
		}

		LightingDebug.ShadowColliderTotalGenerationsWorld_collider ++;

		MeshObject meshObject = GetMesh_SkinnedMesh();

		if (meshObject == null) {
			skinnedMeshShape.polygons_world = new List<Polygon2D>();
			return(skinnedMeshShape.polygons_world);
		}

		Vector3 vecA, vecB, vecC;
		Polygon2D poly;

		if (skinnedMeshShape.polygons_world_cache == null) {
			skinnedMeshShape.polygons_world = new List<Polygon2D>();

			for (int i = 0; i < meshObject.triangles.GetLength (0); i = i + 3) {
				vecA = lightingCollider2D.transform.TransformPoint(meshObject.vertices [meshObject.triangles [i]]);
				vecB = lightingCollider2D.transform.TransformPoint(meshObject.vertices [meshObject.triangles [i + 1]]);
				vecC = lightingCollider2D.transform.TransformPoint(meshObject.vertices [meshObject.triangles [i + 2]]);

				poly = new Polygon2D();
				poly.AddPoint(vecA.x, vecA.y);
				poly.AddPoint(vecB.x, vecB.y);
				poly.AddPoint(vecC.x, vecC.y);

				skinnedMeshShape.polygons_world.Add(poly);
			}	

			skinnedMeshShape.polygons_world_cache = skinnedMeshShape.polygons_world;

		} else {
			int count = 0;

			skinnedMeshShape.polygons_world = skinnedMeshShape.polygons_world_cache;

			for (int i = 0; i < meshObject.triangles.GetLength (0); i = i + 3) {
				vecA = lightingCollider2D.transform.TransformPoint(meshObject.vertices [meshObject.triangles [i]]);
				vecB = lightingCollider2D.transform.TransformPoint(meshObject.vertices [meshObject.triangles [i + 1]]);
				vecC = lightingCollider2D.transform.TransformPoint(meshObject.vertices [meshObject.triangles [i + 2]]);

				poly = skinnedMeshShape.polygons_world[count];
				poly.pointsList[0].x = vecA.x;
				poly.pointsList[0].y = vecA.y;
				poly.pointsList[1].x = vecB.x;
				poly.pointsList[1].y = vecB.y;
				poly.pointsList[2].x = vecC.x;
				poly.pointsList[2].y = vecC.y;

				count += 1;
			}
		}

		return(skinnedMeshShape.polygons_world);
	}

	public List<Polygon2D> GetPolygons_Mesh_World(Transform transform) {
		if (meshShape.polygons_world != null) {
			return(meshShape.polygons_world);
		}

		LightingDebug.ShadowColliderTotalGenerationsWorld_collider ++;

		MeshObject meshObject = GetMesh_Mesh();

		if (meshObject == null) {
			meshShape.polygons_world = new List<Polygon2D>();
			return(meshShape.polygons_world);
		}

		Vector3 vecA, vecB, vecC;
		Polygon2D poly;

		if (meshShape.polygons_world_cache == null) {
			meshShape.polygons_world = new List<Polygon2D>();

			for (int i = 0; i < meshObject.triangles.GetLength (0); i = i + 3) {
				vecA = lightingCollider2D.transform.TransformPoint(meshObject.vertices [meshObject.triangles [i]]);
				vecB = lightingCollider2D.transform.TransformPoint(meshObject.vertices [meshObject.triangles [i + 1]]);
				vecC = lightingCollider2D.transform.TransformPoint(meshObject.vertices [meshObject.triangles [i + 2]]);

				poly = new Polygon2D();
				poly.AddPoint(vecA.x, vecA.y);
				poly.AddPoint(vecB.x, vecB.y);
				poly.AddPoint(vecC.x, vecC.y);

				meshShape.polygons_world.Add(poly);
			}	
			meshShape.polygons_world_cache = meshShape.polygons_world;

		} else {
			int count = 0;

			meshShape.polygons_world = meshShape.polygons_world_cache;

			for (int i = 0; i < meshObject.triangles.GetLength (0); i = i + 3) {
				vecA = lightingCollider2D.transform.TransformPoint(meshObject.vertices [meshObject.triangles [i]]);
				vecB = lightingCollider2D.transform.TransformPoint(meshObject.vertices [meshObject.triangles [i + 1]]);
				vecC = lightingCollider2D.transform.TransformPoint(meshObject.vertices [meshObject.triangles [i + 2]]);

				poly = meshShape.polygons_world[count];
				poly.pointsList[0].x = vecA.x;
				poly.pointsList[0].y = vecA.y;
				poly.pointsList[1].x = vecB.x;
				poly.pointsList[1].y = vecB.y;
				poly.pointsList[2].x = vecC.x;
				poly.pointsList[2].y = vecC.y;

				count += 1;
			}
		}
	
		return(meshShape.polygons_world);
	}

	public List<Polygon2D> GetPolygons_Shape_World(Transform transform, VirtualSpriteRenderer virtualSpriteRenderer) {
		if (spriteShape.polygons_world != null) {
			return(spriteShape.polygons_world);
		}

		Vector2 scale = new Vector2();
		List<Polygon2D> list = GetPolygons_Shape_Local();

		if (spriteShape.polygons_world_cache != null) {				
			if (list.Count != spriteShape.polygons_world_cache.Count) {
				spriteShape.polygons_world_cache = null;
				spriteShape.polygons_world_pair_cache = null;

			} else {
				for(int i = 0; i < list.Count; i++) {
					if (list[i].pointsList.Count != spriteShape.polygons_world_cache[i].pointsList.Count) {
						spriteShape.polygons_world_cache = null;
						spriteShape.polygons_world_pair_cache = null;

						break;
					}
				}
			}
		}
	
		if (spriteShape.polygons_world_cache != null) {
			LightingDebug.ShadowColliderTotalGenerationsWorld_shape_re ++;

			spriteShape.polygons_world = spriteShape.polygons_world_cache;

			Polygon2D poly;
			Vector2D point;

			for(int i = 0; i < list.Count; i++) {
				poly = list[i];
				for(int p = 0; p < poly.pointsList.Count; p++) {
					point = poly.pointsList[p];
					
					spriteShape.polygons_world[i].pointsList[p].x = point.x;
					spriteShape.polygons_world[i].pointsList[p].y = point.y;
				}

				if (virtualSpriteRenderer != null) {
					scale.x = 1;
					scale.y = 1;

					if (virtualSpriteRenderer.flipX == true) {
						scale.x = -1;
					}

					if (virtualSpriteRenderer.flipY == true) {
						scale.y = -1;
					}
					
					if (virtualSpriteRenderer.flipX != false || virtualSpriteRenderer.flipY != false) {
						spriteShape.polygons_world[i].ToScaleItself(scale);
					}
				}

				spriteShape.polygons_world[i].ToWorldSpaceItself(transform);
			}
		} else {
			LightingDebug.ShadowColliderTotalGenerationsWorld_shape ++;

			Polygon2D polygon;

			spriteShape.polygons_world = new List<Polygon2D>();

			foreach(Polygon2D poly in list) {
				polygon = poly.Copy();

				if (virtualSpriteRenderer != null) {
					scale.x = 1;
					scale.y = 1;

					if (virtualSpriteRenderer.flipX == true) {
						scale.x = -1;
					}

					if (virtualSpriteRenderer.flipY == true) {
						scale.y = -1;
					}
					
					if (virtualSpriteRenderer.flipX != false || virtualSpriteRenderer.flipY != false) {
						polygon.ToScaleItself(scale);
					}
				}
				
				polygon.ToWorldSpaceItself(transform);

				spriteShape.polygons_world.Add(polygon);

				spriteShape.polygons_world_cache = spriteShape.polygons_world;
			}
		}

		return(spriteShape.polygons_world);
	}

	public List<Polygon2D> GetPolygons_Collider_Local(Transform transform) {
		if (colliderShape.polygons_local != null) {
			return(colliderShape.polygons_local);
		}
		
		LightingDebug.ShadowColliderTotalGenerationsLocal_collider ++;

		colliderShape.polygons_local = Polygon2DList.CreateFromGameObject (transform.gameObject);

		if (colliderShape.polygons_local.Count > 0) {

			colliderShape.edgeCollider2D = (transform.GetComponent<EdgeCollider2D>() != null);

		} else {
			Debug.LogWarning("SmartLighting2D: LightingCollider2D object is missing Collider2D Component", transform.gameObject);
		}
	
		return(colliderShape.polygons_local);
	}

    public List<Polygon2D> GetPolygons_Shape_Local() {
		if (spriteShape.polygons_local != null) {
			return(spriteShape.polygons_local);
		}

		LightingDebug.ShadowColliderTotalGenerationsLocal_shape ++;

		spriteShape.polygons_local = new List<Polygon2D>();

		#if UNITY_2018_1_OR_NEWER

			if (customPhysicsShape == null) {
				if (originalSprite == null) {
					return(spriteShape.polygons_local);
				}

				customPhysicsShape = GetPhysicsShape();
			}

			spriteShape.polygons_local = customPhysicsShape.Get();
			spriteShape.edgeCollider2D = false;

		#endif

		return(spriteShape.polygons_local);
	}

	public class Helper {
		static public void MeshToVertices(MeshVertices meshVertices, MeshObject meshObject, Transform transform) {
			int triangleCount = meshObject.triangles.GetLength (0);
			MeshVertice vertice;

			for (int i = 0; i < triangleCount; i = i + 3) {
				vertice = new MeshVertice();
				vertice.a = new Vector2D(transform.TransformPoint(meshObject.vertices [meshObject.triangles [i]]));
				vertice.b = new Vector2D(transform.TransformPoint(meshObject.vertices [meshObject.triangles [i + 1]]));
				vertice.c = new Vector2D(transform.TransformPoint(meshObject.vertices [meshObject.triangles [i + 2]]));

				meshVertices.veclist.Add(vertice);
			}
		}

		static public void MeshToVerticesCached(MeshVertices meshVertices, MeshObject meshObject, Transform transform) {
			if (meshVertices == null) {
				return;
			}

			int triangleCount = meshObject.triangles.GetLength (0);
			int count = 0;
			Vector2 v;

			for (int i = 0; i < triangleCount; i = i + 3) {
				v =  transform.TransformPoint(meshObject.vertices [meshObject.triangles [i]]); 
				meshVertices.veclist[count].a.x = v.x;
				meshVertices.veclist[count].a.y = v.y;

				v =  transform.TransformPoint(meshObject.vertices [meshObject.triangles [i + 1]]);
				meshVertices.veclist[count].b.x = v.x;
				meshVertices.veclist[count].b.y = v.y;

				v =  transform.TransformPoint(meshObject.vertices [meshObject.triangles [i + 2]]);
				meshVertices.veclist[count].c.x = v.x;
				meshVertices.veclist[count].c.y = v.y;

				count += 1;
			}
		}
	}
}