using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingBufferBase {
    public static Vector2 p = Vector2.zero;
  	public static Vector2D polyOffset = Vector2D.Zero();
	public static Vector2D offset = Vector2D.Zero();
	public static Vector2 polyOffset2 = Vector2.zero;
	public static Vector2 scale = new Vector2(1, 1);
	public static Vector2D scale2D = new Vector2D(1, 1);
	public static List<Polygon2D> polygons;
	public static List<List<Pair2D>> polygonPairs;
	public static Polygon2D polygon = null;
	public static List<Pair2D> pairList;
	public static VirtualSpriteRenderer spriteRenderer = new VirtualSpriteRenderer();
	public static MeshObject tileMesh;
	public static Vector2D tilemapOffset = Vector2D.Zero();
	public static LightingTile tile;
	public static Vector2Int newPositionInt = new Vector2Int();
	public static Vector2 posScale = Vector2.zero;
    public static int sizeInt;
	public static Sprite reqSprite;
	public static PartiallyBatchedTilemap batched;
    public static Color color = Color.white;
    public static Vector2D zero = Vector2D.Zero();
   	public static Vector2D vA = Vector2D.Zero(), pA = Vector2D.Zero(), vB = Vector2D.Zero(), pB = Vector2D.Zero();
	public static Rect uvRect = new Rect();
	public static Pair2D pair = Pair2D.Zero();
	public const float uv0 = 0f;
	public const float uv1 = 1f;
}
