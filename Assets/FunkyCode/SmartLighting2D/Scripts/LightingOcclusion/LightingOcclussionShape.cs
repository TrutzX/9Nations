using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingOcclussionShape {
    VirtualSpriteRenderer spriteRenderer = new VirtualSpriteRenderer();
    
    public List<List<Pair2D>> pairs1 = new List<List<Pair2D>>();
    public List<List<Pair2D>> pairs2 = new List<List<Pair2D>>();
    public List<List<DoublePair2D>> pairs3 = new List<List<DoublePair2D>>();
    

    public void Init(LightingCollider2D id) {
        spriteRenderer.sprite = id.shape.GetOriginalSprite();
        if (id.spriteRenderer != null) {
            spriteRenderer.flipX = id.spriteRenderer.flipX;
            spriteRenderer.flipY = id.spriteRenderer.flipY;
        } else {
            spriteRenderer.flipX = false;
            spriteRenderer.flipY = false;
        }

        pairs1.Clear();
        pairs2.Clear();
        pairs3.Clear();

        List<Polygon2D> polygons = id.shape.GetPolygons_World_ColliderType(id.transform, spriteRenderer);
        if (polygons == null || polygons.Count < 1) {
            return;
        }

        foreach(Polygon2D polygon in polygons) {
            polygon.Normalize();

            List<Pair2D> iterate1 = Pair2D.GetList(polygon.pointsList);
            List<Pair2D> iterate2 = Pair2D.GetList(PreparePolygon(polygon, id.occlusionSize).pointsList);
            List<DoublePair2D> iterate3 = DoublePair2D.GetList(polygon.pointsList);
            
            pairs1.Add(iterate1);
            pairs2.Add(iterate2);
            pairs3.Add(iterate3);
        }
    }

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
