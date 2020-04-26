using System.Collections.Generic;
using UnityEngine;

public class Max2D {
	
	#if UNITY_2018_3_OR_NEWER 
		public const string shaderPath = "Legacy Shaders/";
	#else
		public const string shaderPath = "";
	#endif
	
	static public void Vertex3(Vector2D p, float z) {
        GL.Vertex3((float)p.x, (float)p.y, z);
	}

}







