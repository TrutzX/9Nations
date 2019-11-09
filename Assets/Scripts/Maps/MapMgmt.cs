using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using DataTypes;
using Endless;
using Game;
using Help;
using Map;
using NesScripts.Controls.PathFind;
using Units;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Maps
{
    public class MapMgmt : MonoBehaviour
    {
        private List<Tilemap> layers;

        public GameObject mapLayerPrototyp;
        public TileBase[] tiles;
        public TileBase[] selected;

        public TileBase fog;
        public TileBase border;

        public Tilemap activeAction;

        public MapPathFinding PathFinding;
    
        /// <summary>
        /// Get it
        /// </summary>
        /// <returns></returns>
        [Obsolete("MapMgmt.Get() is deprecated, please use GameMgmt.Get().map instead.")]
        public static MapMgmt Get()
        {
            return GameMgmt.Get().map;
        }

        public static Vector2 GetMouseMapXY()
        {
            return Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        }

        public IEnumerator LoadMap()
        {
            //can load?
            if (GameMgmt.Get().data.mapfile == null)
            {
                //TODO show error
                yield break;
            }
        
            //load Map
            yield return StartCoroutine(LoadMap(MapHelper.GetAllMaps().Find(m => m.name==GameMgmt.Get().data.mapfile)));
        
        }
    
        // Update is called once per frame
        public IEnumerator LoadMap(Map map)
        {
            yield return GameMgmt.Get().ShowLoadingScreenSecond($"Loading Map data");
            //set size
            string[][] l = CSV.Read(map.Layer(0));
            GameMgmt.Get().data.mapwidth = l.Length;
            GameMgmt.Get().data.mapheight = l[0].Length;
        
            Debug.Log($"Load Map {map.name} ({l.Length}/{l[0].Length})");
        
            //load layer
            layers = new List<Tilemap>();
            yield return AddNewLayer(transform.childCount,CSV.Convert(l));
            yield return AddNewLayer(transform.childCount,CSV.Convert(CSV.Read(map.Layer(1))));
            //todo dynamic layers
            
            PathFinding = new MapPathFinding();
        }

        private IEnumerator AddNewLayer(int level, int[][] data)
        {
            Tilemap t = CreateLayer($"Layer {level}");
            layers.Insert(0,t);
        
            for (int x = 0; x < data.Length; x++)
            {
                for (int y = 0; y < data[0].Length; y++)
                {
                    int d = data[x][y];
                    //skip?
                    if (d == -1)
                    {
                        continue;
                    }

                    try
                    {
                        TilemapHelper.setTile(t, x, y, tiles[d]);

                        //add border?
                        if (x == 0)
                        {
                            TilemapHelper.setTile(t, x-1, y, tiles[d]);
                        }
                        if (x == GameMgmt.Get().data.mapwidth-1)
                        {
                            TilemapHelper.setTile(t, x+1, y, tiles[d]);
                        }
                        if (y == 0)
                        {
                            TilemapHelper.setTile(t, x, y-1, tiles[d]);
                        
                        }
                        if (y == GameMgmt.Get().data.mapheight-1)
                        {
                            TilemapHelper.setTile(t, x, y+1, tiles[d]);
                        }
                    }
                    catch (IndexOutOfRangeException i)
                    {
                        Debug.LogException(new InvalidDataException($"Field type {d} does not exist",i));
                        break;
                    }
                }
                yield return GameMgmt.Get().ShowLoadingScreenSecond($"Loading Map {level*data.Length+x}/{data.Length*(level+1)}");
            }
        }

        public void SetTile(int layer, int x, int y, string terrain)
        {
            TilemapHelper.setTile(layers[layer], x, y+1, tiles[Data.nTerrain[terrain].tileid]);
        }
        
        private void AddBorder()
        {
            Tilemap t = CreateLayer("Border");
            t.transform.position = new Vector3(0,0,-1);

            for (int x = -1; x <= GameMgmt.Get().data.mapwidth; x++)
            {
                TilemapHelper.setTile(t,x,-1,border);
                TilemapHelper.setTile(t,x,GameMgmt.Get().data.mapheight,border);
            }

            for (int y = 0; y < GameMgmt.Get().data.mapheight; y++)
            {
                TilemapHelper.setTile(t,-1,y,border);
                TilemapHelper.setTile(t,GameMgmt.Get().data.mapwidth,y,border);
            }
        }

        public Tilemap CreateLayer(string name)
        {
            GameObject g = Instantiate(mapLayerPrototyp, GetComponent<Transform>());
            g.name = name;
            return g.GetComponent<Tilemap>();
        }

        public NTerrain GetTerrain(int x, int y)
        {
            Vector3Int v = new Vector3Int(x * 2, y * 2, 0);
            foreach (Tilemap layer in layers)
            {
                if (layer.HasTile(v))
                {
                    return Data.nTerrain[layer.GetTile(v).name];
                }
            }

            return Data.nTerrain["unknown"];
        }

        /// <summary>
        /// Get a start position or an exception
        /// </summary>
        /// <param name="nation"></param>
        /// <returns></returns>
        /// <exception cref="MissingMemberException"></exception>
        public PPoint GetStartPos(string nation)
        {
            Nation n = Data.nation[nation];
            int i = 0;
            while (i < 1000)
            {
                int x = Random.Range(0, GameMgmt.Get().data.mapwidth);
                int y = Random.Range(0, GameMgmt.Get().data.mapheight);
                NTerrain t = GetTerrain(x, y);
            
                //right terrain?
                if (t.walk == 10 || t.name == n.hometerrain)
                {
                    //has a unit?
                    if (UnitMgmt.At(x,y) == null)
                        return new PPoint(x,y);
                }

                i++;
            }
        
            throw new MissingMemberException($"Can not find a start position for {nation}");
        }

        public static bool Valide(int x, int y)
        {
            return 0 <= x && x < GameMgmt.Get().data.mapwidth && 0 <= y && y < GameMgmt.Get().data.mapheight;
        }

        public void FinishStart()
        {
            AddBorder();
        }
    }
}
