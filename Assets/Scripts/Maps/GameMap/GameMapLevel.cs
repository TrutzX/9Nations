using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Game;
using Libraries;
using Map;
using Maps.TileMaps;
using NesScripts.Controls.PathFind;
using Terrains;
using UnityEngine;

namespace Maps.GameMap
{
    public class GameMapLevel
    {
        
        private MapPathFinding _pathFinding, _winterPathFinding;
        public GameMapDataLevel DataLevel;
        public TileMapConfig32 Improvement;
        public GameMapData map;

        public int Id;
        
        public List<TileMapConfig16> Layers;
        public List<TileMapConfig16> WinterLayers;

        public GameMapLevel(int id)
        {
            Id = id;
            DataLevel = GameMgmt.Get().data.map.levels[id];
            map = GameMgmt.Get().data.map;
            DataLevel.Improvement = new string[map.width,map.height];
            
            //load layers
            Layers = new List<TileMapConfig16>();
            WinterLayers = new List<TileMapConfig16>();
            
            _pathFinding = new MapPathFinding(id);
            _winterPathFinding = new MapPathFinding(id);
        }

        public IEnumerator AddLayers()
        {
            for (int i = 0; i < DataLevel.LayerCount();i++)
            {
                yield return AddLayer(i);
            }
            Improvement = GameMgmt.Get().map.CreateLayer32("Improvement", GameMapMgmt.SortImpro);
        }

        public IEnumerator AddLayer(int layer)
        {
            TileMapConfig16 t = GameMgmt.Get().map.CreateLayer16($"{Id} {Layers.Count}", layer).GetComponent<TileMapConfig16>();
            TileMapConfig16 w = GameMgmt.Get().map.CreateLayer16($"W{Id} {Layers.Count}", layer).GetComponent<TileMapConfig16>();
            string max = "/" + map.width + "/" + Layers.Count;
            
            for (int x = 0; x < map.width; x++)
            {
                for (int y = 0; y < map.height; y++)
                {
                    int d = DataLevel.At(new Vector3Int(x, y, layer));
                    //skip?
                    if (d == -1)
                    {
                        continue;
                    }
                    
                    try
                    {
                        t.SetTile(DataLevel, layer, x,y,L.b.terrain.Terrain(d), false);
                        int q = DataLevel.At(new Vector3Int(x, y, layer), true);
                        w.SetTile(DataLevel, layer, x,y,L.b.terrain.Terrain(q), true);
                    }
                    catch (IndexOutOfRangeException i)
                    {
                        Debug.Log(d+" "+L.b.terrain.Values().Count(terr => terr.DefaultTile == d));
                        Debug.LogException(new InvalidDataException($"Field type {d} does not exist",i));
                        break;
                    }
                }
                yield return GameMgmt.Get().load.ShowSubMessage($"Loading {Id} {x}{max}");
            }
            
            Layers.Add(t);
            WinterLayers.Add(w);
            w.gameObject.SetActive(false);
        }

        public BTerrain Terrain(int x, int y)
        {
            return DataLevel.Terrain(x, y, map.IsWinter);
        }

        public void NextRound()
        {
            bool winter = GameMgmt.Get().round.IsSeason(GameMgmt.Get().round.season[3]);
            //is no change
            if (map.IsWinter == winter)
            {
                return;
            }
            
            //change to summer?
            if (map.IsWinter && !winter)
            {
                WinterLayers?.ForEach(w => w.gameObject.SetActive(false));
                Layers.ForEach(w => w.gameObject.SetActive(true));
                map.IsWinter = false;
                return;
            }
                
            //change to winter?
            if (!map.IsWinter && winter)
            {
                WinterLayers?.ForEach(w => w.gameObject.SetActive(true));
                Layers.ForEach(w => w.gameObject.SetActive(false));
                map.IsWinter = true;
            }
        }

        public void ResetPathFinding()
        {
            _pathFinding.ResetAll();
            _winterPathFinding.ResetAll();
        }

        public MapPathFinding PathFinding()
        {
            return map.IsWinter ? _winterPathFinding : _pathFinding;
        }
    }
}