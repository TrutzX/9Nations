using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Light Source
public enum LightingSourceTextureSize {px2048, px1024, px512, px256, px128};
public enum LightingEventState {OnCollision, OnCollisionEnter, OnCollisionExit, None}

[ExecuteInEditMode]
public class LightingSource2D : MonoBehaviour {
	public enum LightSprite {Default, Custom};
	public enum WhenInsideCollider {DrawAbove, DrawInside}; // Draw Bellow / Do Not Draw

	// Settings
	public Color lightColor = new Color(.5f,.5f, .5f, 1);
	public float lightAlpha = 1f;
	public float lightSize = 5f;
	public float lightCoreSize = 0.5f;
	public LightingSourceTextureSize textureSize = LightingSourceTextureSize.px1024;
	public bool enableCollisions = true;
	public bool rotationEnabled = false;
	public bool eventHandling = false;

	public bool additive = false;
	public float additive_alpha = 0.25f;

	public WhenInsideCollider whenInsideCollider = WhenInsideCollider.DrawAbove;

	public bool disableWhenInvisible = false;

	public LightSprite lightSprite = LightSprite.Default;
	public Sprite sprite;
	public bool spriteFlipX = false;
	public bool spriteFlipY = false;

	public LayerSetting[] layerSetting = new LayerSetting[1];

	private bool inScreen = false;

	public LightingBuffer2D buffer = null;

	public int sortingOrder;
	public string sortingLayer;

	//public LightingEventState lightingEventState = LightingEventState.None;
	public List<LightingCollider2D> lightignEventCache = new List<LightingCollider2D>();

	///// Movemnt ////
	public LightingSource2DMovement movement = new LightingSource2DMovement();

	//public bool staticUpdated = false; // Not Necessary

	public float occlusionSize = 15;

	public static Sprite defaultSprite = null;

	public static List<LightingSource2D> list = new List<LightingSource2D>();

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

	static public Sprite GetDefaultSprite() {
		if (defaultSprite == null || defaultSprite.texture == null) {
			defaultSprite = Resources.Load <Sprite> ("Sprites/gfx_light");
		}
		return(defaultSprite);
	}

	public void OnEnable() {
		list.Add(this);
	}

	public void OnDisable() {
		list.Remove(this);

		///// Free Buffer!
		FBOManager.FreeBuffer(buffer);

		buffer = null;
		inScreen = false;
	}

	static public List<LightingSource2D> GetList() {
		return(list);
	}

	public bool InCamera(Camera camera) {
		if (camera == null) {
			return(false);
		} else {
			float cameraSize = camera.orthographicSize;
			return(Vector2.Distance(transform.position, camera.transform.position) < Mathf.Sqrt((cameraSize * 2f) * (cameraSize * 2f)) + lightSize );
		}	
	}

	public bool InAnyCamera() {
		CameraSettings[] cameraSettings = LightingManager2D.Get().cameraSettings;
		for(int i = 0; i < cameraSettings.Length; i++) {
			Camera camera = LightingManager2D.Get().GetCamera(i);
			if (InCamera(camera)) {
				return(true);
			}
		}

		return(false);
	}

	void Start () {
		movement.ForceUpdate();

		for(int i = 0; i < layerSetting.Length; i++) {
			if (layerSetting[i] == null) {
				layerSetting[i] = new LayerSetting();
				layerSetting[i].layerID = LightingLayer.Layer1;
				layerSetting[i].type = LightingLayerType.Default;
				layerSetting[i].renderingOrder = LightingLayerOrder.Default;
			}
		}
	}

	public Sprite GetSprite() {
		if (sprite == null || sprite.texture == null) {
			sprite = GetDefaultSprite();
		}
		return(sprite);
	}
		
	public LightingBuffer2D GetBuffer() {
		buffer = FBOManager.PullBuffer (LightingRender.GetTextureSize(textureSize), this);
		return(buffer);
	}

	void Update() {
		movement.Update(this);

		LightingManager2D manager = LightingManager2D.Get();
		bool disabled = manager.disableEngine;

		if (InAnyCamera()) {
			if (movement.update == true) {
				if (inScreen == false) {
					GetBuffer();

					movement.update = false;
					if (buffer != null) {
						if (disabled == false) {
							buffer.bufferCamera.enabled = true; // //UpdateLightBuffer(True)
							buffer.bufferCamera.orthographicSize = lightSize;
						}
					}

					inScreen = true;
				} else {
					movement.update = false;
					if (buffer != null) {
						if (disabled == false) {
							buffer.bufferCamera.enabled = true; // //UpdateLightBuffer(True)
							buffer.bufferCamera.orthographicSize = lightSize;
						}
					}
				}
			} else {
				if (buffer == null) {
					GetBuffer();

					movement.update = false;
					if (buffer != null) {
						if (disabled == false) {
							buffer.bufferCamera.enabled = true; // //UpdateLightBuffer(True)
							buffer.bufferCamera.orthographicSize = lightSize;
						}
					}
				
					inScreen = true;
				}
			}
		} else {
			///// Free Buffer!
			if (buffer != null) {
				FBOManager.FreeBuffer(buffer);
				buffer = null;
			}
			inScreen = false;
		}
		
		if (eventHandling) {
			UpdateEventHandling();
			UpdateTilemapEventHandling();
		}
	}

	public void UpdateTilemapEventHandling() {
		/*
		List<LightTilemapCollision2D> collisions = EventHandling_GetCollisions();
		EventHandling_RemoveHiddenCollisions(collisions);

		if (collisions.Count < 1) {
			for(int i = 0; i < lightignEventCache.Count; i++) {
				LightingCollider2D collider = lightignEventCache[i];
				
				LightCollision2D collision = new LightCollision2D();
				collision.lightSource = this;
				collision.collider = collider;
				collision.pointsColliding = null;
				collision.lightingEventState = LightingEventState.OnCollisionExit;

				collider.CollisionEvent(collision);
			}

			lightignEventCache.Clear();

			return;
		}
			
		List<LightingCollider2D> eventColliders = new List<LightingCollider2D>();

		foreach(LightCollision2D collision in collisions) {
			eventColliders.Add(collision.collider);
		}

		for(int i = 0; i < lightignEventCache.Count; i++) {
			LightingCollider2D collider = lightignEventCache[i];
			if (eventColliders.Contains(collider) == false) {
				
				LightCollision2D collision = new LightCollision2D();
				collision.lightSource = this;
				collision.collider = collider;
				collision.pointsColliding = null;
				collision.lightingEventState = LightingEventState.OnCollisionExit;

				collider.CollisionEvent(collision);
				
				lightignEventCache.Remove(collider);
			}
		}
		
		foreach(LightCollision2D collision in collisions) {
			if (lightignEventCache.Contains(collision.collider)) {
				collision.lightingEventState = LightingEventState.OnCollision;
			} else {
				collision.lightingEventState = LightingEventState.OnCollisionEnter;
				lightignEventCache.Add(collision.collider);
			}
		
			collision.collider.CollisionEvent(collision);
		}*/
	}


	public void UpdateEventHandling() {
		List<LightCollision2D> collisions = EventHandling_GetCollisions();
		EventHandling_RemoveHiddenCollisions(collisions);

		if (collisions.Count < 1) {
			for(int i = 0; i < lightignEventCache.Count; i++) {
				LightingCollider2D collider = lightignEventCache[i];
				
				LightCollision2D collision = new LightCollision2D();
				collision.lightSource = this;
				collision.collider = collider;
				collision.pointsColliding = null;
				collision.lightingEventState = LightingEventState.OnCollisionExit;

				collider.CollisionEvent(collision);
			}

			lightignEventCache.Clear();

			return;
		}
			
		List<LightingCollider2D> eventColliders = new List<LightingCollider2D>();

		foreach(LightCollision2D collision in collisions) {
			eventColliders.Add(collision.collider);
		}

		for(int i = 0; i < lightignEventCache.Count; i++) {
			LightingCollider2D collider = lightignEventCache[i];
			if (eventColliders.Contains(collider) == false) {
				
				LightCollision2D collision = new LightCollision2D();
				collision.lightSource = this;
				collision.collider = collider;
				collision.pointsColliding = null;
				collision.lightingEventState = LightingEventState.OnCollisionExit;

				collider.CollisionEvent(collision);
				
				lightignEventCache.Remove(collider);
			}
		}
		
		foreach(LightCollision2D collision in collisions) {
			if (lightignEventCache.Contains(collision.collider)) {
				collision.lightingEventState = LightingEventState.OnCollision;
			} else {
				collision.lightingEventState = LightingEventState.OnCollisionEnter;
				lightignEventCache.Add(collision.collider);
			}
		
			collision.collider.CollisionEvent(collision);
		}









	}

	public List<LightCollision2D> EventHandling_GetCollisions() {
		List<LightCollision2D> collisions = new List<LightCollision2D>();
		List<LightingCollider2D> colliderList = LightingCollider2D.GetList();

		List<Vector2D> removePointsColliding = new List<Vector2D>();

		foreach (LightingCollider2D id in colliderList) {
			if (id.shape.colliderType == LightingCollider2D.ColliderType.None) {
				continue;
			}

			if (LightingManager2D.culling && Vector2.Distance(id.transform.position, transform.position) > id.shape.GetFrustumDistance(id.transform) + lightSize) {
				continue;
			}

			Polygon2D poly = id.shape.GetPolygons_Local_ColliderType(id.transform)[0].ToWorldSpace(id.transform);
			poly.ToOffsetItself(new Vector2D (-transform.position));

			LightCollision2D collision = new LightCollision2D();
			collision.lightSource = this;
			collision.collider = id;
			collision.pointsColliding = poly.pointsList;
			
			foreach(Vector2D point in collision.pointsColliding) {
				if (point.ToVector2().magnitude > lightSize) {
					removePointsColliding.Add(point);
				}
			}

			foreach(Vector2D point in removePointsColliding) {
				collision.pointsColliding.Remove(point);
			}
			removePointsColliding.Clear();

			collisions.Add(collision);
		}

		return(collisions);
	}

	public List<LightCollision2D> EventHandling_RemoveHiddenCollisions(List<LightCollision2D> collisions ) {
		List<LightingCollider2D> colliderList = LightingCollider2D.GetList();
		Vector2D zero = Vector2D.Zero();	
		float lightSizeSquared = Mathf.Sqrt(lightSize * lightSize + lightSize * lightSize);
		double rot;	
		
		Polygon2D triPoly = EventHandling_GetPolygon();

		Vector2D scale = new Vector2D(transform.lossyScale.x, transform.localScale.y);

		Pair2D p;
		Vector2D offset = Vector2D.Zero();
		VirtualSpriteRenderer virtualSpriteRenderer = new VirtualSpriteRenderer();

		List<Vector2D> removePointsColliding = new List<Vector2D>();
		List<LightCollision2D> removeCollisions = new List<LightCollision2D>();

		List<List<Pair2D>> pairs;

		foreach (LightingCollider2D id in colliderList) {
			if (LightingManager2D.culling && Vector2.Distance(id.transform.position, transform.position) > id.shape.GetFrustumDistance(id.transform) + lightSize) {
				continue;
			}

			if (id.shape.colliderType == LightingCollider2D.ColliderType.None) {
				continue;
			}

			pairs = id.shape.GetPolygons_Pair_World_ColliderType(id.transform, virtualSpriteRenderer);

			if (pairs.Count < 1) {
				continue;
			}

			offset.x = - transform.position.x;
			offset.y = - transform.position.y;

			removePointsColliding.Clear();
			removeCollisions.Clear();

			for(int i = 0; i < pairs.Count; i++) {
				for(int x = 0; x < pairs[i].Count; x++) {
					p = pairs[i][x];

					vA.x = p.A.x * scale.x + offset.x;
					vA.y = p.A.y * scale.y + offset.y;

					vB.x = p.B.x * scale.x + offset.x;
					vB.y = p.B.y * scale.y + offset.y;

					vC.x = p.A.x * scale.x + offset.x;
					vC.y = p.A.y * scale.y + offset.y;

					vD.x = p.B.x * scale.x + offset.x;
					vD.y = p.B.y * scale.y + offset.y;
					
					rot = System.Math.Atan2 (vA.y, vA.x);
					vA.x += System.Math.Cos(rot) * lightSizeSquared;
					vA.y += System.Math.Sin(rot) * lightSizeSquared;

					rot = System.Math.Atan2 (vB.y, vB.x);
					vB.x += System.Math.Cos(rot) * lightSizeSquared;
					vB.y += System.Math.Sin(rot) * lightSizeSquared;

					triPoly.pointsList[0].x = vA.x;
					triPoly.pointsList[0].y = vA.y;

					triPoly.pointsList[1].x = vB.x;
					triPoly.pointsList[1].y = vB.y;

					triPoly.pointsList[2].x = vD.x;
					triPoly.pointsList[2].y = vD.y;

					triPoly.pointsList[3].x = vC.x;
					triPoly.pointsList[3].y = vC.y;

					foreach(LightCollision2D col in collisions) {
						if (col.collider == id) {
							continue;
						}

						foreach(Vector2D point in col.pointsColliding) {
							if (triPoly.PointInPoly(point)) {
								removePointsColliding.Add(point);
							}
						}

						foreach(Vector2D point in removePointsColliding) {
							col.pointsColliding.Remove(point);
						}

						removePointsColliding.Clear();
						
						if (col.pointsColliding.Count < 1) {
							removeCollisions.Add(col);
						}
					}

					foreach(LightCollision2D col in removeCollisions) {
						collisions.Remove(col);
					}

					removeCollisions.Clear();
				}
			}
		}
		return(collisions);
	}

	static Vector2D vA = Vector2D.Zero(), vB = Vector2D.Zero();
	static Vector2D vC = Vector2D.Zero(), vD = Vector2D.Zero();
	static Polygon2D eventPoly = null;

	static public Polygon2D EventHandling_GetPolygon() {
		if (eventPoly == null) {
			eventPoly = new Polygon2D();
			eventPoly.AddPoint(Vector2D.Zero());
			eventPoly.AddPoint(Vector2D.Zero());
			eventPoly.AddPoint(Vector2D.Zero());
			eventPoly.AddPoint(Vector2D.Zero());
		}
		return(eventPoly);
	}
}
