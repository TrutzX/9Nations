using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Game;
using Libraries;
using Libraries.Terrains;
using Maps;
using Maps.GameMaps;
using Maps.TileMaps;
using NesScripts.Controls.PathFind;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GameMapLevels
{
    public class GameMapLevel : MonoBehaviour
    {
        public Grid map;
        public TileMapConfig16 prototypeMapLayer16;
        public TileMapConfig32 prototypeMapLayer32;
        public GameObject buildings;
        public GameObject units;
        public GameObject effects;
        
        public int level;
        public List<TileMapConfig16> layers;
        public TileMapConfig16 border;
        public TileMapConfig16 overlay;
        public List<TileMapConfig16> layersWinter;
        public GameMapDataLevel dataLevel;
        private GameMapData mapData;
        public TileMapConfig32 improvement;
        
        private MapPathFinding _pathFinding, _winterPathFinding;

        public GameMapLevel()
        {
            layers = new List<TileMapConfig16>();
            layersWinter = new List<TileMapConfig16>();
        }

        public void Init(int level)
        {
            this.level = level;
            dataLevel = GameMgmt.Get().data.map.levels[level];
            mapData = GameMgmt.Get().data.map;
            
            _pathFinding = new MapPathFinding(level);
            _winterPathFinding = new MapPathFinding(level);
        }
        
        public TileMapConfig16 CreateNewLayer(string name, int order = 0)
        {
            TileMapConfig16 gml = Instantiate<TileMapConfig16>(prototypeMapLayer16, map.transform);
            gml.gameObject.name = name;
            gml.GetComponent<TilemapRenderer>().sortingOrder = order;
            //gml.level = levels.Count;
            return gml;
        }
        
        private TileMapConfig32 CreateNewLayer32(string name, int order = 0)
        {
            TileMapConfig32 gml = Instantiate<TileMapConfig32>(prototypeMapLayer32, map.transform);
            gml.gameObject.name = name;
            gml.GetComponent<TilemapRenderer>().sortingOrder = order;
            //gml.level = levels.Count;
            return gml;
        }
        
        public IEnumerator AddLayers()
        {
            for (int i = 0; i < dataLevel.LayerCount();i++)
            {
                yield return AddLayer(i);
            }
            improvement = CreateNewLayer32("Improvement", dataLevel.LayerCount());
            overlay = CreateNewLayer("overlay", dataLevel.LayerCount()+1);
            overlay.GetComponent<TilemapRenderer>().sortingLayerName = "Overlay";
            yield return AddBorder();
        }

        public IEnumerator AddLayer(int layer)
        {
            TileMapConfig16 t = CreateNewLayer($"{layer} {layers.Count}", layer);
            TileMapConfig16 w = CreateNewLayer($"W{layer} {layers.Count}", layer).GetComponent<TileMapConfig16>();
            string max = "/" + mapData.width + "/" + layers.Count;
            
            for (int x = 0; x < mapData.width; x++)
            {
                for (int y = 0; y < mapData.height; y++)
                {
                    Vector3Int v = new Vector3Int(x, y, layer);
                    int d = dataLevel.At(v);
                    //skip?
                    if (d == -1)
                    {
                        continue;
                    }
                    
                    try
                    {
                        t.SetTile(dataLevel, v,L.b.terrains[d], false, Color.white);
                        int q = dataLevel.At(v, true);
                        w.SetTile(dataLevel, v,L.b.terrains[q], true, Color.white);
                    }
                    catch (Exception e)
                    {
                        Debug.Log(d+" "+L.b.terrains.Values().Count(terr => terr.defaultTile == d));
                        Debug.Log($"{x}/{dataLevel.Width()}/{mapData.width},{y}/{dataLevel.Height()}/{mapData.height}");
                        Debug.LogException(new InvalidDataException($"Field type {d} does not exist", e));
                        break;
                    }
                }
                yield return GameMgmt.Get().load.ShowSubMessage($"Loading {layer} {x}{max}");
            }
            
            layers.Add(t);
            layersWinter.Add(w);
            w.gameObject.SetActive(false);
        }

        public void SetTile(Vector3Int pos, DataTerrain terrain)
        {
            //set or remove?
            if (terrain == null)
            {
                dataLevel.Set(pos, -1, false);
                dataLevel.Set(pos, -1, true);
            }
            else
            {
                dataLevel.Set(pos, terrain.defaultTile, false);

                DataTerrain winter = string.IsNullOrEmpty(terrain.winter) ? terrain : L.b.terrains[terrain.winter];
                dataLevel.Set(pos, winter.defaultTile, true);
            }

            //inform tiles
            UpdateTile(new Vector3Int(pos.x-1, pos.y-1, pos.z));
            UpdateTile(new Vector3Int(pos.x-1, pos.y, pos.z));
            UpdateTile(new Vector3Int(pos.x-1, pos.y+1, pos.z));
            UpdateTile(new Vector3Int(pos.x, pos.y-1, pos.z));
            UpdateTile(pos);
            UpdateTile(new Vector3Int(pos.x, pos.y+1, pos.z));
            UpdateTile(new Vector3Int(pos.x+1, pos.y-1, pos.z));
            UpdateTile(new Vector3Int(pos.x+1, pos.y, pos.z));
            UpdateTile(new Vector3Int(pos.x+1, pos.y+1, pos.z));

            //reset pathfinding
            ResetPathFinding();
        }

        private void UpdateTile(Vector3Int pos)
        {
            if (!S.Valid(pos.x, pos.y)) return;
            
            int tId = dataLevel.At(pos, false);
            layers[pos.z].SetTile(dataLevel, pos, tId==-1?null:L.b.terrains[tId], false, Color.white);
            
            tId = dataLevel.At(pos, true);
            layersWinter[pos.z].SetTile(dataLevel, pos, tId==-1?null:L.b.terrains[tId], true, Color.white);
        }
        
        private IEnumerator AddBorder()
        {
            yield return GameMgmt.Get().load.ShowSubMessage($"Add border");

            TileBase bTile = GameMgmt.Get().newMap.prototypeBorder;

            border = CreateNewLayer("Border", dataLevel.LayerCount()+5);
            border.transform.position = new Vector3(0,0,-1);

            for (int x = -1; x <= GameMgmt.Get().data.map.width; x++)
            {
                border.SetTile(x,-1,bTile);
                border.SetTile(x,GameMgmt.Get().data.map.height,bTile);
            }
            
            yield return GameMgmt.Get().load.ShowSubMessage($"Add vertical border");

            for (int y = 0; y < GameMgmt.Get().data.map.height; y++)
            {
                border.SetTile(-1,y,bTile);
                border.SetTile(GameMgmt.Get().data.map.width,y,bTile);
            }
        }

        public void ResetPathFinding()
        {
            _pathFinding.ResetAll();
            _winterPathFinding.ResetAll();
        }

        public MapPathFinding PathFinding()
        {
            return dataLevel.isWinter ? _winterPathFinding : _pathFinding;
        }

        public void NextRound()
        {
            //todo change dyn
            bool winter = GameMgmt.Get().gameRound.IsSeason("winter");
            //is no change
            if (dataLevel.isWinter == winter)
            {
                return;
            }
            
            //change to summer?
            if (dataLevel.isWinter && !winter)
            {
                layersWinter.ForEach(w => w.gameObject.SetActive(false));
                layers.ForEach(w => w.gameObject.SetActive(true));
                dataLevel.isWinter = false;
                return;
            }
                
            //change to winter?
            if (!dataLevel.isWinter && winter)
            {
                layersWinter.ForEach(w => w.gameObject.SetActive(true));
                layers.ForEach(w => w.gameObject.SetActive(false));
                dataLevel.isWinter = true;
            }
        }
    }
}
