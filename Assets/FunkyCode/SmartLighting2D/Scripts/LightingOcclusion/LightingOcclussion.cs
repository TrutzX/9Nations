using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingOcclussion 
{
 
	/* 
	static void Draw_Tilemap(Vector2D offset, float z) {
		
		GL.PushMatrix ();
		LightingManager2D.Get().occlusionEdgeMaterial.SetPass (0);
		GL.Begin (GL.TRIANGLES);

		foreach (LightingTilemapCollider2D id in LightingTilemapCollider2D.GetList()) {
			if (id.map == null) {
				continue;
			}
			if (id.ambientOcclusion == false) {
				continue;
			}
			for(int x = 0; x < id.area.size.x; x++) {
				for(int y = 0; y < id.area.size.y; y++) {
					if (id.map[x, y] == null) {
						continue;
					}

					Vector2D offs = offset.Copy();
					offs += new Vector2D(0.5f, 0.5f);
					offs += new Vector2D(id.area.position.x, id.area.position.y);
					offs += new Vector2D(-id.area.size.x / 2, -id.area.size.y / 2);

					DrawTileOcclussion(offs + new Vector2D(x, y), z, id);
				}
			}
		}
		GL.End ();
		GL.PopMatrix ();
		
	}
	
	#if UNITY_2018_1_OR_NEWER
	static void DrawTileOcclussion(Vector2D offset, float z, LightingTilemapCollider2D id) {
		Polygon2D poly = Polygon2DList.CreateFromRect(new Vector2(0.5f, 0.5f));

		foreach (DoublePair2D p in DoublePair2D.GetList(poly.pointsList)) {
			Vector2D vA = p.A + offset;
			Vector2D vB = p.B + offset;
			Vector2D vC = p.B + offset;

			Vector2D pA = p.A + offset;
			Vector2D pB = p.B + offset;

			vA.Push (Vector2D.Atan2 (p.A, p.B) - Mathf.PI / 2, -1);
			vB.Push (Vector2D.Atan2 (p.A, p.B) - Mathf.PI / 2, -1);
			vC.Push (Vector2D.Atan2 (p.B, p.C) - Mathf.PI / 2, -1);

			GL.TexCoord2 (uv0, uv0);
			Max2D.Vertex3 (pB, z);
			GL.TexCoord2 (0.5f - uv0, uv0);
			Max2D.Vertex3 (pA, z);
			GL.TexCoord2 (0.5f - uv0, uv1);
			Max2D.Vertex3 (vA, z);

			GL.TexCoord2 (uv0, uv1);
			Max2D.Vertex3 (vA, z);
			GL.TexCoord2 (0.5f - uv0, uv1);
			Max2D.Vertex3 (vB, z);
			GL.TexCoord2 (0.5f - uv0, uv0);
			Max2D.Vertex3 (pB, z);

			GL.TexCoord2 (uv1, uv0);
			Max2D.Vertex3 (vB, z);
			GL.TexCoord2 (0.5f - uv0, uv0);
			Max2D.Vertex3 (pB, z);
			GL.TexCoord2 (0.5f - uv0, uv1);
			Max2D.Vertex3 (vC, z);
		}
	}
	#endif*/

	// Remove or Change!!!
    // Polygon Occlusion
	static public Polygon2D PreparePolygon(Polygon2D polygon, float size) {
		Polygon2D newPolygon = new Polygon2D();

		DoublePair2D pair = new DoublePair2D (null, null, null);;
		Vector2D pairA = Vector2D.Zero();
		Vector2D pairC = Vector2D.Zero();
		Vector2D vecA = Vector2D.Zero();
		Vector2D vecC = Vector2D.Zero();

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
