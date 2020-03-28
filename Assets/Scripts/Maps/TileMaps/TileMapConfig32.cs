using Game;
using UI;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Maps
{
    public class TileMapConfig32 : MonoBehaviour
    {
        public void SetTile(int x, int y, Sprite sprite)
        {
            //set it
            Tilemap tileMap = GetComponent<Tilemap>();
            
            //top left
            Tile t = ScriptableObject.CreateInstance<Tile>();
            t.sprite = sprite;
            tileMap.SetTile(new Vector3Int(x-1, y-1, 0), t);
        }
    }
}