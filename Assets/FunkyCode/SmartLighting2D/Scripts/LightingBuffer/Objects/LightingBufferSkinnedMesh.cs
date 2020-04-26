using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingBufferSkinnedMesh : LightingBufferBase {

	public static void Shadow(LightingBuffer2D buffer, LightingCollider2D id, float lightSizeSquared, float z) {
		if (id.isVisibleForLight(buffer) == false) {
			return;
		}

		polygons = id.shape.GetPolygons_World_ColliderType(id.transform);

		Polygon2D poly;
		
		float angleA, angleB;	

		uvRect = LightingBufferShadow.Penumbra.uvRect;

		offset.x = -buffer.lightSource.transform.position.x;
		offset.y = -buffer.lightSource.transform.position.y;
	
		if (polygons.Count < 1) {
			return;
		}

		foreach(Polygon2D polygon in polygons) {
			poly = polygon;

			//if (poly.PointInPoly (zero)) {
			//	continue;
			//}
			
			GL.Color(Color.white);

			for(int i = 0; i < poly.pointsList.Count; i++) {
				pair.A.x = poly.pointsList[i].x + offset.x;
				pair.A.y = poly.pointsList[i].y + offset.y;

				pair.B.x = poly.pointsList[(i + 1) % poly.pointsList.Count].x + offset.x;
				pair.B.y = poly.pointsList[(i + 1) % poly.pointsList.Count].y + offset.y;

				vA.x = pair.A.x;
				vA.y = pair.A.y;

				pA.x = pair.A.x;
				pA.y = pair.A.y;

				vB.x = pair.B.x;
				vB.y = pair.B.y;

				pB.x = pair.B.x;
				pB.y = pair.B.y;

				angleA = (float)Vector2D.Atan2 (vA, zero);
				angleB = (float)Vector2D.Atan2 (vB, zero);

				vA.Push (angleA, lightSizeSquared);
				pA.Push (angleA - Mathf.Deg2Rad * buffer.lightSource.occlusionSize, lightSizeSquared);

				vB.Push (angleB, lightSizeSquared);
				pB.Push (angleB + Mathf.Deg2Rad * buffer.lightSource.occlusionSize, lightSizeSquared);

				GL.TexCoord2(uvRect.x, uvRect.y);
				GL.Vertex3((float)pair.A.x,(float)pair.A.y, z);

				GL.TexCoord2(uvRect.width, uvRect.y);
				GL.Vertex3((float)vA.x, (float)vA.y, z);

				GL.TexCoord2((float)uvRect.x, uvRect.height);
				GL.Vertex3((float)pA.x,(float)pA.y, z);

				GL.TexCoord2(uvRect.x, uvRect.y);
				GL.Vertex3((float)pair.B.x,(float)pair.B.y, z);

				GL.TexCoord2(uvRect.width, uvRect.y);
				GL.Vertex3((float)vB.x, (float)vB.y, z);

				GL.TexCoord2(uvRect.x, uvRect.height);
				GL.Vertex3((float)pB.x, (float)pB.y, z);
			}

			GL.Color(Color.black);
			for(int i = 0; i < poly.pointsList.Count - 1; i++) {
				pair.A.x = poly.pointsList[i].x + offset.x;
				pair.A.y = poly.pointsList[i].y + offset.y;

				pair.B.x = poly.pointsList[i + 1].x + offset.x;
				pair.B.y = poly.pointsList[i + 1].y + offset.y;

				vA.x = pair.A.x;
				vA.y = pair.A.y;

				vB.x = pair.B.x;
				vB.y = pair.B.y;

				vA.Push (Vector2D.Atan2 (vA, zero), lightSizeSquared);
				vB.Push (Vector2D.Atan2 (vB, zero), lightSizeSquared);
				

				GL.TexCoord3(Max2DMatrix.c_x, Max2DMatrix.c_y, 0);
				GL.Vertex3((float)pair.A.x, (float)pair.A.y, z);

				GL.TexCoord3(Max2DMatrix.c_x, Max2DMatrix.c_y, 0);
				GL.Vertex3((float)pair.B.x, (float)pair.B.y, z);

				GL.TexCoord3(Max2DMatrix.c_x, Max2DMatrix.c_y, 0);
				GL.Vertex3((float)vA.x, (float)vA.y, z);


				GL.TexCoord3(Max2DMatrix.c_x, Max2DMatrix.c_y, 0);
				GL.Vertex3((float)vA.x, (float)vA.y, z);

				GL.TexCoord3(Max2DMatrix.c_x, Max2DMatrix.c_y, 0);
				GL.Vertex3((float)vB.x, (float)vB.y, z);

				GL.TexCoord3(Max2DMatrix.c_x, Max2DMatrix.c_y, 0);
				GL.Vertex3((float)pair.B.x, (float)pair.B.y, z);
			}
		}
	}

	public class WithoutAtlas {
		
		public static void Mask(LightingBuffer2D buffer, LightingCollider2D id, Material material, LayerSetting layerSetting, float z) {
			if (id.isVisibleForLight(buffer) == false) {
				return;
			}

			MeshVertices vertices = id.shape.GetMesh_Vertices_MaskType(id.transform);

			if (vertices == null || vertices.veclist == null || vertices.veclist.Count < 1) {
				return;
			}

			bool maskEffect = (layerSetting.effect == LightingLayerEffect.InvisibleBellow);

			LightingMaskMode maskMode = id.maskMode;
			MeshVertice vertice;
			MeshUV uv;

			if (id.skinnedMeshRenderer.sharedMaterial != null) {
				material.mainTexture = id.skinnedMeshRenderer.sharedMaterial.mainTexture;
			}

			offset.x = -buffer.lightSource.transform.position.x;
			offset.y = -buffer.lightSource.transform.position.y;

			material.SetPass (0); 
			GL.Begin (GL.TRIANGLES);

			for (int i = 0; i < vertices.veclist.Count; i ++) {
				vertice = vertices.veclist[i];
				uv = vertices.uvList[i];

				GL.TexCoord3(uv.a.x, uv.a.y, 0);
				GL.Vertex3((float)vertice.a.x + (float)offset.x, (float)vertice.a.y + (float)offset.y, z);
				GL.TexCoord3(uv.b.x, uv.b.y, 0);
				GL.Vertex3((float)vertice.b.x + (float)offset.x, (float)vertice.b.y + (float)offset.y, z);
				GL.TexCoord3(uv.c.x, uv.c.y, 0);
				GL.Vertex3((float)vertice.c.x + (float)offset.x, (float)vertice.c.y + (float)offset.y, z);
			}

			GL.End ();

			material.mainTexture = null;
		}
	}
}
