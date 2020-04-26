using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LightingMainBuffer2D))]
public class Lighting2DMainBufferEditor : Editor {
	override public void OnInspectorGUI() {
		LightingMainBuffer2D script = target as LightingMainBuffer2D;
		EditorGUILayout.ObjectField(script.regularCamera, typeof(Camera), true);
	}
}