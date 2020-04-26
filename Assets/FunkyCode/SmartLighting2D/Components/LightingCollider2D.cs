using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum LightingMaskMode {Visible, Invisible};



[ExecuteInEditMode]
public class LightingCollider2D : MonoBehaviour {
	public enum MaskType {None, Sprite, Collider, SpriteCustomPhysicsShape, Mesh, SkinnedMesh};
	public enum ColliderType {None, Collider, SpriteCustomPhysicsShape, Mesh, SkinnedMesh};
	
	public delegate void LightCollision2DEvent(LightCollision2D collision);

	public LightingLayer lightingMaskLayer = LightingLayer.Layer1;
	public LightingLayer lightingCollisionLayer = LightingLayer.Layer1;

	public LightingMaskMode maskMode = LightingMaskMode.Visible;

	public event LightCollision2DEvent collisionEvents;

	public LightingColliderShape shape = new LightingColliderShape();

	DayLightingShadowHeight dayLightingShadow = null;
	LightingOcclussionShape occlusionShape = null;

	private LightingColliderMovement movement = new LightingColliderMovement();

	//public class DayLightingMode {
		public bool generateDayMask = false;
		
		public bool dayHeight = false;
		public float height = 1f;
	//}


	///// Day Lighting /////


	public bool ambientOcclusion = false;
	public bool smoothOcclusionEdges = false;
	public float occlusionSize = 1f;
	/////////////////////////

	public bool disableWhenInvisible = false;

	public static List<LightingCollider2D> list = new List<LightingCollider2D>();

	public SpriteRenderer spriteRenderer;
	public MeshFilter meshFilter;
	public MeshRenderer meshRenderer;
	public SkinnedMeshRenderer skinnedMeshRenderer;

	public void Awake() {
		shape.SetLightingCollider2D(this);
	}
	
	public void AddEvent(LightCollision2DEvent collisionEvent) {
		collisionEvents += collisionEvent;
	}

	public void CollisionEvent(LightCollision2D collision) {
		if (collisionEvents != null) {
			collisionEvents (collision);
		}
	}

	// 1.5f??
	public bool isVisibleForLight(LightingBuffer2D buffer) {
		if (LightingManager2D.culling && Vector2.Distance(transform.position, buffer.lightSource.transform.position) > shape.GetFrustumDistance(transform) + buffer.lightSource.lightSize * 1.5f) {
			LightingDebug.culled ++;
			return(false);
		}

		return(true);
	}

	public bool InCamera(Camera camera) {
		float cameraSize = camera.orthographicSize;
		
		float distance = Vector2.Distance(transform.position, camera.transform.position);
		float size = Mathf.Sqrt((cameraSize * 2f) * (cameraSize * 2f)) + shape.GetFrustumDistance(transform);
		
        return (distance < size);
    }

	public void OnEnable() {
		list.Add(this);

		Initialize();
	}

	public void OnDisable() {
		list.Remove(this);

		float distance = shape.GetFrustumDistanceSet();
		foreach (LightingSource2D id in LightingSource2D.GetList()) {
			if (Vector2.Distance (id.transform.position, transform.position) < distance + id.lightSize) {
				id.movement.ForceUpdate();
			}
		}
	}

	public void OnBecameVisible() {
		if (disableWhenInvisible) {
			if (this.enabled == false) {
				this.enabled = true;
			}
		}	
	}

	public void OnBecameInvisible() {
		if (disableWhenInvisible) {
			if (this.enabled == true) {
				this.enabled = false;
			}
		}
	}

	static public List<LightingCollider2D> GetList() {
		return(list);
	}

	public void Initialize() {
		movement.Reset();
		movement.moved = true;

		occlusionShape = null;

		shape.ResetLocal();

		spriteRenderer = GetComponent<SpriteRenderer>();
		
		if (spriteRenderer != null) {
			shape.SetOriginalSprite(spriteRenderer.sprite);
		}

		meshRenderer = GetComponent<MeshRenderer>();
		meshFilter = GetComponent<MeshFilter>();

		skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
	}

	public bool DrawOrNot(LightingSource2D id) {
		if (id.layerSetting == null) {
			return(false);
		}

		for(int i = 0; i < id.layerSetting.Length; i++) {
			if (id.layerSetting[i] == null) {
				continue;
			}
			switch(id.layerSetting[i].type) {
				case LightingLayerType.Default:
					if (i == (int)lightingCollisionLayer || i == (int)lightingMaskLayer) {
						return(true);
					}
				break;

				case LightingLayerType.MaskOnly:
					if (i == (int)lightingMaskLayer) {
						return(true);
					}
				break;

				case LightingLayerType.ShadowOnly:
					if (i == (int)lightingCollisionLayer) {
						return(true);
					}
				break;
			}
		}
		return(false);
	}

	public void Update() {
		movement.Update(this);

		if (movement.moved) {
			shape.ResetWorld();
			occlusionShape = null;
			
			float distance = shape.GetFrustumDistance(transform);
		
			foreach (LightingSource2D id in LightingSource2D.GetList()) {
				bool draw = DrawOrNot(id);

				if (draw == false) {
					continue;
				}
				
				if (Vector2.Distance (id.transform.position, transform.position) < distance + id.lightSize) {
					id.movement.ForceUpdate();
				}
			}

			dayLightingShadow = null;
		}
	}

	public void LateUpdate() {
		Update();
	}	
	
	public DayLightingShadowHeight GetDayLightingShadow(float sunDirection) {
		DayLightingShadowShape dayLightingShadowShape = DayLightingShadowShape.RequestShape(shape.GetOriginalSprite());

		if (dayLightingShadowShape == null) {
			return(null);
		}

		dayLightingShadow = dayLightingShadowShape.RequestHeight((int)height);
		dayLightingShadow.Update(sunDirection);
		//dayLightingShadow.Update(sunDirection, height, this);

		return(dayLightingShadow);
	}

	public LightingOcclussionShape GetOcclusionShape() {
		if (occlusionShape == null) {
			occlusionShape = new LightingOcclussionShape();
			occlusionShape.Init(this);
		}
		return(occlusionShape);
	}
}