using UI;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Help
{
    public class TilemapHelper
    {
        public static void SetTile(Tilemap tileMap, int x, int y, TileBase tile = null)
        {
            //set it
            tileMap.SetTile(new Vector3Int(x * 2, y * 2, 0), tile);
            tileMap.SetTile(new Vector3Int(x * 2 + 1, y * 2, 0), tile);
            tileMap.SetTile(new Vector3Int(x * 2, y * 2 + 1, 0), tile);
            tileMap.SetTile(new Vector3Int(x * 2 + 1, y * 2 + 1, 0), tile);
        }
    }
}