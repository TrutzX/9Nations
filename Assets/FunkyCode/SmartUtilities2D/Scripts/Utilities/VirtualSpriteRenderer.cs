using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualSpriteRenderer {
	public Sprite sprite;

	public Color color = Color.white;
	public Material material;

	public bool flipX = false;
	public bool flipY = false;

	public VirtualSpriteRenderer(SpriteRenderer spriteRenderer) {
		Set(spriteRenderer);
	}

	public void Set(SpriteRenderer spriteRenderer) {
		sprite = spriteRenderer.sprite;

		flipX = spriteRenderer.flipX;
		flipY = spriteRenderer.flipY;

		material = spriteRenderer.sharedMaterial;

		color = spriteRenderer.color;
	}

	public VirtualSpriteRenderer() {}
}


		

