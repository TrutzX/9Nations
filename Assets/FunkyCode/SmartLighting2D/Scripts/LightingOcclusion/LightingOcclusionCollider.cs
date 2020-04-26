using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingOcclusionCollider {
    const float uv0 = 0;
    const float uv1 = 1;

	static LightingManager2D manager;

    static DoublePair2D pair = new DoublePair2D (null, null, null);
	static Vector2D pairA = Vector2D.Zero();
	static Vector2D pairC = Vector2D.Zero();
	static Vector2D vecA = Vector2D.Zero();
	static Vector2D vecC = Vector2D.Zero();

	static Vector2D vA = Vector2D.Zero(), vB = Vector2D.Zero(), vC = Vector2D.Zero();
	static Vector2D pA = Vector2D.Zero(), pB = Vector2D.Zero();

	static List<LightingCollider2D> colliderList = LightingCollider2D.GetList();
		

    public static void Draw(Vector2D offset, float z) {
		manager = LightingManager2D.Get();

		colliderList = LightingCollider2D.GetList();

		Strict(offset, z);
		Smooth(offset, z);
	}

    static void Strict(Vector2D offset, float z) {
		bool draw = false;

        LightingOcclussionShape shape;
        List<Pair2D> iterate1;
        List<Pair2D> iterate2;
        Vector2D first = null;
		Pair2D pA;
        Pair2D pB;
        bool isEdgeCollider;

		foreach (LightingCollider2D id in colliderList) {
			if (id.ambientOcclusion == false || id.smoothOcclusionEdges == true) {
				continue;
			}

			if (draw == false) {
				draw = true;
				manager.materials.GetOcclusionBlur().SetPass (0);
				GL.Begin (GL.QUADS);
				GL.Color(Color.white);
			}

            shape = id.GetOcclusionShape();

            isEdgeCollider = id.shape.IsEdgeCollider();
			
            for(int x = 0; x < shape.pairs1.Count; x++) {
                iterate1 = shape.pairs1[x];
                iterate2 = shape.pairs2[x];
		
				first = null;

				int i = 0;
				for(int y = 0; y < iterate1.Count; y++) {
					pA = iterate1[y];
					
					if (isEdgeCollider && first == null) {
						first = pA.A;
						continue;
					}

					if (i >= iterate2.Count) {
						continue;
					}

					pB = iterate2[i];

					GL.TexCoord3 (uv0, uv0, 0);
					GL.Vertex3((float)(pA.A.x + offset.x), (float)(pA.A.y + offset.y), z);

					GL.TexCoord3 (uv1, uv0, 0);
					GL.Vertex3((float)(pA.B.x + offset.x), (float)(pA.B.y + offset.y), z);

					GL.TexCoord3 (uv1, uv1, 0);
					GL.Vertex3((float)(pB.B.x + offset.x), (float)(pB.B.y + offset.y), z);

					GL.TexCoord3 (uv0, uv1, 0);
					GL.Vertex3((float)(pB.A.x + offset.x), (float)(pB.A.y + offset.y), z);
	
					i ++;
				}
			}
		}

		if (draw == true) {
			GL.End ();
		}
	}

	static void Smooth(Vector2D offset, float z) {
		bool draw = false;
        double angleA, angleB, angleC;

        List<DoublePair2D> iterate3;
        LightingOcclussionShape shape;
		DoublePair2D p;
		
		foreach (LightingCollider2D id in colliderList) {
			if (id.ambientOcclusion == false || id.smoothOcclusionEdges == false) {
				continue;
			}

			if (draw == false) {
				draw = true;
				manager.materials.GetOcclusionEdge().SetPass (0);
				GL.Begin (GL.TRIANGLES);
			}

            shape = id.GetOcclusionShape();
			
            for(int x = 0; x < shape.pairs3.Count; x++) {
                iterate3 = shape.pairs3[x];

				for(int y = 0; y < iterate3.Count; y++) {
					p = iterate3[y];
                
					vA.x = p.A.x + offset.x;
					vA.y = p.A.y + offset.y;

					vB.x = p.B.x + offset.x;
					vB.y = p.B.y + offset.y;

					vC.x = p.B.x + offset.x;
					vC.y = p.B.y + offset.y;

					pA.x = p.A.x + offset.x;
					pA.y = p.A.y + offset.y;

					pB.x = p.B.x + offset.x;
					pB.y = p.B.y + offset.y;

					angleA = Vector2D.Atan2 (p.A, p.B) - Mathf.PI / 2;
					angleB = Vector2D.Atan2 (p.A, p.B) - Mathf.PI / 2;
					angleC = Vector2D.Atan2 (p.B, p.C) - Mathf.PI / 2;

					vA.Push (angleA, id.occlusionSize);
					vB.Push (angleB, id.occlusionSize);
					vC.Push (angleC, id.occlusionSize);

					GL.TexCoord3 (uv0, uv0, 0);
					GL.Vertex3((float)pB.x, (float)pB.y, z);
					GL.TexCoord3 (0.5f - uv0, uv0, 0);
					GL.Vertex3((float)pA.x, (float)pA.y, z);
					GL.TexCoord3 (0.5f - uv0, uv1, 0);
					GL.Vertex3((float)vA.x, (float)vA.y, z);

					GL.TexCoord3 (uv0, uv1, 0);
					GL.Vertex3((float)vA.x, (float)vA.y, z);
					GL.TexCoord3 (0.5f - uv0, uv1, 0);
					GL.Vertex3((float)vB.x, (float)vB.y, z);
					GL.TexCoord3 (0.5f - uv0, uv0, 0);
					GL.Vertex3((float)pB.x, (float)pB.y, z);
		
					GL.TexCoord3 (uv1, uv0, 0);
					GL.Vertex3((float)vB.x, (float)vB.y, z);
					GL.TexCoord3 (0.5f - uv0, uv0, 0);
					GL.Vertex3((float)pB.x, (float)pB.y, z);
					GL.TexCoord3 (0.5f - uv0, uv1, 0);
					GL.Vertex3((float)vC.x, (float)vC.y, z);
				}
			}
		}

		if (draw == true) {
			GL.End ();
		}
	}

    static public Polygon2D PreparePolygon(Polygon2D polygon, float size) {
		Polygon2D newPolygon = new Polygon2D();

		foreach (Vector2D pB in polygon.pointsList) {
			int indexB = polygon.pointsList.IndexOf (pB);

			int indexA = (indexB - 1);
			if (indexA < 0) {
				indexA += polygon.pointsList.Count;
			}

			int indexC = (indexB + 1);
			if (indexC >= polygon.pointsList.Count) {
				indexC -= polygon.pointsList.Count;
			}

			pair.A = polygon.pointsList[indexA];
			pair.B = pB;
			pair.C = polygon.pointsList[indexC];

			float rotA = (float)Vector2D.Atan2(pair.B, pair.A);
			float rotC = (float)Vector2D.Atan2(pair.B, pair.C);

			pairA.x = pair.A.x;
			pairA.y = pair.A.y;
			pairA.Push(rotA - Mathf.PI / 2, -size);

			pairC.x = pair.C.x;
			pairC.y = pair.C.y;
			pairC.Push(rotC + Mathf.PI / 2, -size);
			
			vecA.x = pair.B.x;
			vecA.y = pair.B.y;
			vecA.Push(rotA - Mathf.PI / 2, -size);
			vecA.Push(rotA, 110f);

			vecC.x = pair.B.x;
			vecC.y = pair.B.y;
			vecC.Push(rotC + Mathf.PI / 2, -size);
			vecC.Push(rotC, 110f);

			Vector2D result = Math2D.GetPointLineIntersectLine(new Pair2D(pairA, vecA), new Pair2D(pairC, vecC));

			if (result != null) {
				newPolygon.AddPoint(result);
			}
		}

		return(newPolygon);
	}   
}
