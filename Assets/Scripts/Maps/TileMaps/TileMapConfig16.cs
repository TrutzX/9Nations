using Game;
using Libraries.Terrains;
using Maps.GameMaps;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Maps.TileMaps
{
    public class TileMapConfig16 : MonoBehaviour
    {
        private bool _winter;
        private IDataLevel _data;
        private Tilemap _tileMap;
        private DataTerrain _terrain;
        private Color _color;
        
        public void SetTile(IDataLevel data, Vector3Int pos, DataTerrain terrain, bool winter, Color color)
        {
            //set it
            _data = data;
            _winter = winter;
            _tileMap = GetComponent<Tilemap>();
            _terrain = terrain;
            _color = color;
            
            //remove it?
            if (terrain == null)
            {
                SetTile(pos.x, pos.y);
                return;
            }
            
            //top left
            _tileMap.SetTile(new Vector3Int(pos.x * 2, pos.y * 2 + 1, 0), _terrain.Tile(TopLeft(pos.z, pos.x, pos.y), _color));
            
            //top right
            _tileMap.SetTile(new Vector3Int(pos.x * 2 + 1, pos.y * 2 + 1, 0), _terrain.Tile(TopRight(pos.z, pos.x, pos.y), _color));
            
            //down left
            _tileMap.SetTile(new Vector3Int(pos.x * 2, pos.y * 2, 0), _terrain.Tile(DownLeft(pos.z, pos.x, pos.y), _color));
            
            //down right
            _tileMap.SetTile(new Vector3Int(pos.x * 2 + 1, pos.y * 2, 0), _terrain.Tile(DownRight(pos.z, pos.x, pos.y), _color));
        }
        
        private int TopLeft(int layer, int x, int y)
        {
            int self = _data.At(new Vector3Int(x,y,layer), _winter);
            bool north = Def(layer, self, x, y+1);
            bool west = Def(layer, self, x-1, y);
            bool northwest = Def(layer, self, x-1, y+1);
            
            if (north && west && northwest)
            {
                return 18;
            }
            
            if (north && west)
            {
                return 2;
            }
            
            if (north)
            {
                return 16;
            }
            
            if (west)
            {
                return 10;
            }

            return 8;
        }

        private int TopRight(int layer, int x, int y)
        {
            int self = _data.At(new Vector3Int(x,y,layer), _winter);
            bool north = Def(layer, self, x, y+1);
            bool east = Def(layer, self, x+1, y);
            bool northeast = Def(layer, self, x+1, y+1);
            
            if (north && east && northeast)
            {
                return 17;
            }
            
            if (north && east)
            {
                return 3;
            }
            
            if (north)
            {
                return 19;
            }
            
            if (east)
            {
                return 9;
            }
            return 11;
        }

        private int DownLeft(int layer, int x, int y)
        {
            int self = _data.At(new Vector3Int(x,y,layer), _winter);
            bool south = Def(layer, self, x, y-1);
            bool west = Def(layer, self, x-1, y);
            bool southwest = Def(layer, self, x-1, y-1);
            
            if (south && west && southwest)
            {
                return 14;
            }
            
            if (south && west)
            {
                return 6;
            }
            
            if (south)
            {
                return 12;
            }
            
            if (west)
            {
                return 22;
            }

            return 20;
        }

        private int DownRight(int layer, int x, int y)
        {
            int self = _data.At(new Vector3Int(x,y,layer), _winter);
            bool south = Def(layer, self, x, y-1);
            bool east = Def(layer, self, x+1, y);
            bool southeast = Def(layer, self, x+1, y-1);
            
            if (south && east && southeast)
            {
                return 13;
            }
            
            if (south && east)
            {
                return 7;
            }
            
            if (south)
            {
                return 15;
            }
            
            if (east)
            {
                return 21;
            }
            return 23;
        }

        private bool Def(int layer, int self, int x, int y)
        {
            if (!GameHelper.Valid(x, y))
            {
                return true;
            }

            return _data.At(new Vector3Int(x,y,layer), _winter)==self;
        }
        public void SetTile(int x, int y, TileBase t = null)
        {
            //set it
            Tilemap tileMap = GetComponent<Tilemap>();
            
            tileMap.SetTile(new Vector3Int(x * 2, y * 2, 0), t);
            tileMap.SetTile(new Vector3Int(x * 2 + 1, y * 2, 0), t);
            tileMap.SetTile(new Vector3Int(x * 2, y * 2 + 1, 0), t);
            tileMap.SetTile(new Vector3Int(x * 2 + 1, y * 2 + 1, 0), t);
        }
    }
}