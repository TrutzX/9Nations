using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlurObject {
	public Sprite sprite;
	public int blurSize;
	public int blurIterations;

	public BlurObject(Sprite image, int size, int iterations) {
		sprite = image;
		blurSize = size;
		blurIterations = iterations;
	}
}