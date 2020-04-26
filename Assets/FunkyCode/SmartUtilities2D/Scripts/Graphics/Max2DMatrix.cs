using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Max2DMatrix {
	const float pi = Mathf.PI;
	const float pi2 = pi / 2;
	const float uv0 = 1f / 32;
	const float uv1 = 1f - uv0;

	public static float c_x = 0;
	public static float c_y = 0;

	public static void DrawTriangle(float x0, float y0, float x1, float y1, float x2, float y2, Vector2D offset, float z = 0f) {
		GL.Vertex3(x0 + (float)offset.x, y0 + (float)offset.y, z);
		GL.Vertex3(x1 + (float)offset.x, y1 + (float)offset.y, z);
		GL.Vertex3(x2 + (float)offset.x, y2 + (float)offset.y, z);
	}
	 
	public static void DrawTriangle(Vector2D vA, Vector2D vB, Vector2D vC, Vector2D offset, float z = 0f) {
		GL.TexCoord3(c_x, c_y, 0);
		GL.Vertex3((float)vA.x + (float)offset.x, (float)vA.y + (float)offset.y, z);
		GL.TexCoord3(c_x, c_y, 0);
		GL.Vertex3((float)vB.x + (float)offset.x, (float)vB.y + (float)offset.y, z);
		GL.TexCoord3(c_x, c_y, 0);
		GL.Vertex3((float)vC.x + (float)offset.x, (float)vC.y + (float)offset.y, z);
	}

	public static void DrawTriangle(Vector2 vA, Vector2 vB, Vector2 vC, Vector2 offset, float z = 0f) {
		GL.TexCoord3(c_x, c_y, 0);
		GL.Vertex3(vA.x * (float) offset.x, vA.y * (float) offset.y, z);
		GL.TexCoord3(c_x, c_y, 0);
		GL.Vertex3(vB.x * (float) offset.x, vB.y * (float) offset.y, z);
		GL.TexCoord3(c_x, c_y, 0);
		GL.Vertex3(vC.x * (float) offset.x, vC.y * (float) offset.y, z);
	}
	
	public static void DrawTriangle(Vector2 vA, Vector2 vB, Vector2 vC, Vector2 offset, float z, Vector2D scale) {
		GL.TexCoord3(c_x, c_y, 0);
		GL.Vertex3(vA.x * (float)scale.x + offset.x, vA.y * (float)scale.y + offset.y, z);
		GL.TexCoord3(c_x, c_y, 0);
		GL.Vertex3(vB.x * (float)scale.x + offset.x, vB.y * (float)scale.y + offset.y, z);
		GL.TexCoord3(c_x, c_y, 0);
		GL.Vertex3(vC.x * (float)scale.x + offset.x, vC.y * (float)scale.y + offset.y, z);
	}

	public static void DrawQuad(Vector2 vA, Vector2 vB, Vector2 vC, Vector2 vD, Vector2D offset, float z, Vector2D scale) {
		GL.Vertex3(vA.x * (float)scale.x + (float)offset.x, vA.y * (float)(scale.y) + (float)offset.y, z);
		GL.Vertex3(vB.x * (float)scale.x + (float)offset.x, vB.y * (float)(scale.y) + (float)offset.y, z);
		GL.Vertex3(vC.x * (float)scale.x + (float)offset.x, vC.y * (float)(scale.y) + (float)offset.y, z);
		GL.Vertex3(vD.x * (float)scale.x + (float)offset.x, vD.y * (float)(scale.y) + (float)offset.y, z);
	}

	public static void DrawQuad(Vector2D vA, Vector2D vB, Vector2D vC, Vector2D vD, float z) {
		GL.Vertex3((float)vA.x, (float)vA.y, z);
		GL.Vertex3((float)vB.x, (float)vB.y, z);
		GL.Vertex3((float)vC.x, (float)vC.y, z);
		GL.Vertex3((float)vD.x, (float)vD.y, z);
	}
}
