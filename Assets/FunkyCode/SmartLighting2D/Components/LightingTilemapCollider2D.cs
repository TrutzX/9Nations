using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using UnityEngine;

#if UNITY_2018_1_OR_NEWER
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
public class LightingTilemapCollider2D : MonoBehaviour {
	public enum MapType {UnityEngineTilemapRectangle, UnityEngineTilemapIsometric, UnityEngineTilemapHexagon, SuperTilemapEditor};
	
	public enum ColliderType {None, Tile, Collider, SpriteCustomPhysicsShape};
	public enum MaskType {None, Tile, Sprite, SpriteCustomPhysicsShape};

	public enum ColliderTypeSTE {None, Tile, Collider};
	public enum MaskTypeSTE {None, Tile, Sprite};

	public MapType mapType = MapType.UnityEngineTilemapRectangle;

	public LightingLayer lightingCollisionLayer = LightingLayer.Layer1;
	public LightingLayer lightingMaskLayer = LightingLayer.Layer1;

	public ColliderType colliderType = ColliderType.Tile;
	public MaskType maskType = MaskType.Sprite;
	public LightingMaskMode maskMode = LightingMaskMode.Visible;

	// Day Lighting
	public bool dayHeight = false;
	public float height = 1;

	public List<Polygon2D> edgeColliders = new List<Polygon2D>();
	public List<Polygon2D> polygonColliders = new List<Polygon2D>();

	//public bool ambientOcclusion = false;
	//public float occlusionSize = 1f;

	public Vector2 cellSize = new Vector2(1, 1);
	public Vector2 cellAnchor = new Vector2(0.5f, 0.5f);
	public Vector2 cellGap = new Vector2(1, 1);
	public Vector2 colliderOffset = new Vector2(0, 0);

	public BoundsInt area;
	public LightingTile[,] map;

	// public CreativeSpore.SuperTilemapEditor.STETilemap tilemapSTE;
	private Tilemap tilemap2D;

	public IsometricMap isometricMap;
	public RectangleMap rectangleMap;

	public static List<LightingTilemapCollider2D> list = new List<LightingTilemapCollider2D>();

	public void OnEnable() {
		list.Add(this);

		Initialize();

		foreach(LightingSource2D light in LightingSource2D.GetList()) {
			light.movement.ForceUpdate();
		}
	}

	public void OnDisable() {
		list.Remove(this);

		foreach(LightingSource2D light in LightingSource2D.GetList()) {
			light.movement.ForceUpdate();
		}
	}

	static public List<LightingTilemapCollider2D> GetList() {
		return(list);
	}

	public void Initialize() {
		switch(mapType) {
			case MapType.UnityEngineTilemapRectangle:
				InitializeUnityRectangle();

			break;

			case MapType.UnityEngineTilemapIsometric:
				InitializeUnityIsometric();

			break;

			case MapType.SuperTilemapEditor:
				InitializeSuperTilemapEditor();

            break;
		}
	}

	void InitializeUnityIsometric() {
		tilemap2D = GetComponent<Tilemap>();

		if (tilemap2D == null) {
			return;
		}

		Grid grid = tilemap2D.layoutGrid;

		if (grid == null) {
			Debug.LogError("Lighting 2D Error: Lighting Tilemap Collider is missing Grid");
		} else {
			cellSize = grid.cellSize;
			cellGap = grid.cellGap;
		}

		cellAnchor = tilemap2D.tileAnchor;

		isometricMap = new IsometricMap();

		ITilemap tilemap = (ITilemap) FormatterServices.GetUninitializedObject(typeof(ITilemap));
		typeof(ITilemap).GetField("m_Tilemap", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(tilemap, tilemap2D);

		foreach (Vector3Int position in tilemap2D.cellBounds.allPositionsWithin)
		{
			TileData tileData = new TileData();

			TileBase tilebase = tilemap2D.GetTile(position);

			if (tilebase != null) {
				tilebase.GetTileData(position, tilemap, ref tileData);

				IsometricMapTile tile = new IsometricMapTile();
				tile.position = position;
				
				LightingTile lightingTile = new LightingTile();
				lightingTile.SetOriginalSprite(tileData.sprite);
				lightingTile.GetShapePolygons();

				tile.tile = lightingTile;

				isometricMap.mapTiles.Add(tile);
			}
		}

		edgeColliders.Clear();
		polygonColliders.Clear();
	}

	void InitializeUnityRectangle() {
		tilemap2D = GetComponent<Tilemap>();

		if (tilemap2D == null) {
			return;
		}

		Grid grid = tilemap2D.layoutGrid;

		if (grid == null) {
			Debug.LogError("Lighting 2D Error: Lighting Tilemap Collider is missing Grid");
		} else {
			cellSize = grid.cellSize;
			cellGap = grid.cellGap;
		}

		cellAnchor = tilemap2D.tileAnchor;

		int minPos = Mathf.Min(tilemap2D.cellBounds.xMin, tilemap2D.cellBounds.yMin);
		int maxPos = Mathf.Max(tilemap2D.cellBounds.size.x, tilemap2D.cellBounds.size.y);

		area = new BoundsInt(minPos, minPos, 0, maxPos + Mathf.Abs(minPos), maxPos + Mathf.Abs(minPos), 1);

		TileBase[] tileArray = tilemap2D.GetTilesBlock(area);

		map = new LightingTile[area.size.x + 1, area.size.y + 1];

		for (int index = 0; index < tileArray.Length; index++) {
			int x = (index % area.size.x);
			int y = (index / area.size.x);
			map[x, y] = null;
		}

		TilemapCollider2D tilemapCollider = GetComponent<TilemapCollider2D>();
		if (tilemapCollider != null) {
			colliderOffset = tilemapCollider.offset;
		}

		cellAnchor += colliderOffset;

		for (int index = 0; index < tileArray.Length; index++) {
			TileBase tile = tileArray[index];
			if (tile == null) {
				continue;
			}

			TileData tileData = new TileData();

			ITilemap tilemap = (ITilemap) FormatterServices.GetUninitializedObject(typeof(ITilemap));
			typeof(ITilemap).GetField("m_Tilemap", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(tilemap, tilemap2D);
			tile.GetTileData(new Vector3Int(0, 0, 0), tilemap, ref tileData);

			LightingTile lightingTile = new LightingTile();
			lightingTile.SetOriginalSprite(tileData.sprite);
			lightingTile.GetShapePolygons();
	
			int x = (index % area.size.x);
			int y = (index / area.size.x);
			map[x, y] = lightingTile;
		}

		edgeColliders.Clear();
		polygonColliders.Clear();
	}

	void InitializeSuperTilemapEditor() {
		/*
		tilemapSTE = GetComponent<CreativeSpore.SuperTilemapEditor.STETilemap>();
	
		cellSize = tilemapSTE.CellSize;

		map = new LightingTile[tilemapSTE.GridWidth + 2, tilemapSTE.GridHeight + 2];

		area.position = new Vector3Int((int)tilemapSTE.MapBounds.center.x, (int)tilemapSTE.MapBounds.center.y, 0);

		area.size = new Vector3Int((int)(tilemapSTE.MapBounds.extents.x + 1) * 2, (int)(tilemapSTE.MapBounds.extents.y + 1) * 2, 0);

		for(int x = 0; x <= tilemapSTE.GridWidth; x++) {
			for(int y = 0; y <= tilemapSTE.GridHeight; y++) {
				map[x, y] = null;
			}
		}

		rectangleMap = new RectangleMap();
		rectangleMap.width = tilemapSTE.GridWidth;
		rectangleMap.height = tilemapSTE.GridHeight;
	
		for(int x = 0; x <= tilemapSTE.GridWidth; x++) {
			for(int y = 0; y <= tilemapSTE.GridHeight; y++) {
				int tileX = x + area.position.x - area.size.x / 2;
				int tileY = y + area.position.y - area.size.y / 2;

				CreativeSpore.SuperTilemapEditor.Tile tileSTE = tilemapSTE.GetTile(tileX, tileY);

				if (tileSTE == null) {
					continue;
				}

				LightingTile lightingTile = new LightingTile();
				map[x, y] = lightingTile;

				RectangleTile tile = new RectangleTile();
				tile.position = new Vector2Int(x, y);

				tile.tile = lightingTile;
				tile.uv = tileSTE.uv;

				rectangleMap.mapTiles.Add(tile);
			}
		}	

		edgeColliders.Clear();
		polygonColliders.Clear();

		if (colliderType == ColliderType.Collider) {
			foreach(Transform t in transform) {
				foreach(Component c in t.GetComponents<EdgeCollider2D>()) {
					Polygon2D poly = Polygon2D.CreateFromEdgeCollider(c as EdgeCollider2D);
					poly = poly.ToWorldSpace(t);
					edgeColliders.Add(poly);
				}
				foreach(Component c in t.GetComponents<PolygonCollider2D>()) {
					Polygon2D poly = Polygon2DList.CreateFromPolygonColliderToWorldSpace(c as PolygonCollider2D)[0];
					polygonColliders.Add(poly);
				}
			}			
		}	
		*/
	}

	public class IsometricMap {
		public List<IsometricMapTile> mapTiles = new List<IsometricMapTile>();
	}

	public class IsometricMapTile {
		public Vector3Int position;

		public LightingTile tile;
	}

	public class RectangleMap {
		public List<RectangleTile> mapTiles = new List<RectangleTile>();

		public int width;
		public int height;
	}

	public class RectangleTile {
		public Vector2Int position;

		public LightingTile tile;

		public Rect uv;
	}
	
}
#endif





















