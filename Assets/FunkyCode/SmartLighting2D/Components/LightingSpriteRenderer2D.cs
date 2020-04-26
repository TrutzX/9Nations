using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LightingSpriteRenderer2D : MonoBehaviour {
	public enum Type {Particle, WhiteMask, BlackMask};
	public enum SpriteMode {Custom, SpriteRenderer};
	public Type type = Type.Particle;
	public SpriteMode spriteMode = SpriteMode.Custom;
	public Sprite sprite = null;

    public Color color = new Color(0.5f, 0.5f, 0.5f, 1f);

	[Range(0, 1)]
	public float alpha = 1f;
    public bool flipX = false;
    public bool flipY = false;

	public Vector2 offsetPosition = new Vector2(0, 0);
	public Vector2 offsetScale = new Vector2(1, 1);
	public float offsetRotation = 0;

	[Range(1, 10)]
	public int blurSize = 1;

	[Range(1, 10)]
	public int blurIterations = 1;

	public bool applyBlur = false;

	public bool applyAdditive = false;

	public int sortingOrder;
	public string sortingLayer;
	
	public bool applyTransformRotation = true;

	public VirtualSpriteRenderer spriteRenderer = new VirtualSpriteRenderer();

	public static List<LightingSpriteRenderer2D> list = new List<LightingSpriteRenderer2D>();

	SpriteRenderer spriteRendererComponent;

	public bool InCamera(Camera camera) {
		float cameraSize = camera.orthographicSize * 1.25f;

		float verticalSize = cameraSize;
        float horizontalSize = cameraSize * ((float)Screen.width / Screen.height);

		return(Vector2.Distance(transform.position, camera.transform.position) < Mathf.Sqrt((verticalSize) * (horizontalSize)) + GetSize() * 2 );
	}

	public Sprite GetSprite() {
		if (GetSpriteOrigin() == null) {
			return(null);
		}

		if (applyBlur) {
			Sprite blurredSprite = BlurManager.RequestSprite(GetSpriteOrigin(), blurSize, blurIterations);
			if (blurredSprite == null) {
				return(GetSpriteOrigin());
			} else {
				return(blurredSprite);
			}
		} else {
			return(GetSpriteOrigin());
		}
	}

	public Sprite GetSpriteOrigin() {
		if (spriteMode == SpriteMode.Custom) {
			return(sprite);
		} else {
			if (GetSpriteRenderer() == null) {
				return(null);
			}
			sprite = spriteRendererComponent.sprite;

			return(sprite);
		}
	}

	public SpriteRenderer GetSpriteRenderer() {
		if (spriteRendererComponent == null) {
			spriteRendererComponent = GetComponent<SpriteRenderer>();
		}
		return(spriteRendererComponent);
	}

	public void OnEnable() {
		list.Add(this);

		color.a = 1f;
	}

	public void OnDisable() {
		list.Remove(this);
	}

	public void Update() {
		spriteRenderer.flipX = flipX;
		spriteRenderer.flipY = flipY;

		spriteRenderer.sprite = GetSprite();
		spriteRenderer.color = color;

		if (applyAdditive) {
			DrawMesh();
		}
	}

	public void DrawMesh() {
		if (applyAdditive) {
			LightingMeshRenderer lightingMesh = MeshRendererManager.Pull(this);

			if (lightingMesh != null) {
				lightingMesh.UpdateAsLightSprite(this);
			}
		}
	}

	float GetSize() {
		Vector2 size = offsetScale;

		Sprite sprite = GetSprite();

		float spriteSheetUV_X = (float)(sprite.texture.width) / sprite.rect.width;
		float spriteSheetUV_Y = (float)(sprite.texture.height) / sprite.rect.height;

		Rect rect = sprite.rect;
		Vector2 scale = new Vector2(spriteSheetUV_X * rect.width / sprite.pixelsPerUnit, spriteSheetUV_Y * rect.height / sprite.pixelsPerUnit);

		scale.x = (float)sprite.texture.width / sprite.rect.width;
		scale.y = (float)sprite.texture.height / sprite.rect.height;

		size.x /= scale.x;
		size.y /= scale.y;

		size.x *= (float)sprite.texture.width / (sprite.pixelsPerUnit * 2);
		size.y *= (float)sprite.texture.height / (sprite.pixelsPerUnit * 2);
		
		Vector2 scale2 = transform.lossyScale;
	
		scale2.x *= size.x;
		scale2.y *= size.y;
		return(Mathf.Sqrt(scale2.x * scale2.x + scale2.y * scale2.y));
	}

    static public List<LightingSpriteRenderer2D> GetList() {
		return(list);
	}

}
