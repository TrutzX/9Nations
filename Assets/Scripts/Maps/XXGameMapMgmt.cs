using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using DataTypes;
using Endless;
using Game;
using Help;
using IniParser.Model;
using Libraries;
using Libraries.Maps;
using Libraries.Terrains;
using Maps.GameMaps;
using Maps.TileMaps;
using NesScripts.Controls.PathFind;
using Tools;
using UI;
using Units;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Maps
{
    public class XXGameMapMgmt : MonoBehaviour
    {
        public const int SortFog = 13, SortImpro = 12, SortBorder = 15, SortActiveAction=14;
        
        public GameObject mapLayerPrototyp16;
        public GameObject mapLayerPrototyp32;
        public TileBase[] selected;

        public TileBase fog;
        public TileBase border;

        public Tilemap activeAction;

        //public List<OldGameMapLevel> Levels;
        
        //public OldGameMapLevel this[int id] => Levels[id];

        public IEnumerator CreateMap()
        {
            //can load?
            if (GameMgmt.Get().data.map.id == null)
            {
                //TODO show error
                yield break;
            }
            //load Map
            //yield return LoadMap(L.b.maps[GameMgmt.Get().data.map.id]);
            yield return CreateLayers();
        }

        public IEnumerator LoadMap()
        {
            yield return GameMgmt.Get().load.ShowSubMessage($"Loading Map data");
            //load Map
            yield return CreateLayers();
        }

        [Obsolete("Is deprecated, please use [z].Terrain(x,y) instead.")]
        public DataTerrain GetTerrain(int x, int y)
        {
            return null;//Levels[0].Terrain(x, y);
        }
    
        // Update is called once per frame
        public IEnumerator LoadMap(DataMap map)
        {
            yield return GameMgmt.Get().load.ShowSubMessage($"Loading Map data");
            
            //read data
            GameMapData gmap = GameMgmt.Get().data.map;
            IniData data = map.Config();
            //add level
            foreach (SectionData section in data.Sections)
            {
                GameMapDataLevel n = new GameMapDataLevel();
                n.name = section.SectionName;
                yield return GameMgmt.Get().load.ShowSubMessage($"Reading {n.name} map data");

                if (data[section.SectionName].ContainsKey("standard"))
                {
                    gmap.standard = gmap.levels.Count();
                }
                
                //add layer
                int m = Int32.Parse(data[section.SectionName]["layer"]);
                for (int i = 0; i < m; i++)
                {
                    n.AddLayer(map.Layer(data[section.SectionName]["format"],i));
                    yield return GameMgmt.Get().load.ShowSubMessage($"Reading {n.name} map data {i}/{m}");
                }
                gmap.levels.Add(n);
            }
            
            //set size
            gmap.width = gmap.levels.First().Width();
            gmap.height = gmap.levels.First().Height();
        
            Debug.Log($"Load Map {map.name} ({gmap.width}/{gmap.height})");
            
            yield return CreateLayers();
        }

        private IEnumerator CreateLayers()
        {
            GameMapData gmap = GameMgmt.Get().data.map;
            
            //load layer
            //Levels = new List<OldGameMapLevel>();
            for (int i = 0; i < gmap.levels.Count; i++)
            {
                //OldGameMapLevel g = new OldGameMapLevel(i);
                //Levels.Add(g);
                //yield return g.AddLayers();
            }

            yield return AddBorder();
        }
        
        public void SetTile(int layer, int x, int y, string terrain)
        {
            throw new MissingComponentException();
            //TilemapHelper.SetTile(layers[layer], x, y+1, tiles[Data.nTerrain[terrain].tileid]);
        }

        public TileMapConfig16 CreateLayer16(string name, int order)
        {
            GameObject g = Instantiate(mapLayerPrototyp16, GetComponent<Transform>());
            g.name = name;
            g.GetComponent<TilemapRenderer>().sortingOrder = order;
            return g.GetComponent<TileMapConfig16>();
        }

        public TileMapConfig32 CreateLayer32(string name, int order)
        {
            GameObject g = Instantiate(mapLayerPrototyp32, GetComponent<Transform>());
            g.name = name;
            g.GetComponent<TilemapRenderer>().sortingOrder = order;
            return g.GetComponent<TileMapConfig32>();
        }

        private IEnumerator AddBorder()
        {
            yield return GameMgmt.Get().load.ShowSubMessage($"Add border");

            TileMapConfig16 t = CreateLayer16("Border", SortBorder).GetComponent<TileMapConfig16>();
            t.transform.position = new Vector3(0,0,-1);

            for (int x = -1; x <= GameMgmt.Get().data.map.width; x++)
            {
                t.SetTile(x,-1,border);
                t.SetTile(x,GameMgmt.Get().data.map.height,border);
            }
            
            yield return GameMgmt.Get().load.ShowSubMessage($"Add vertical border");

            for (int y = 0; y < GameMgmt.Get().data.map.height; y++)
            {
                t.SetTile(-1,y,border);
                t.SetTile(GameMgmt.Get().data.map.width,y,border);
            }
            
        }

        public static bool Valid(NVector pos)
        {
            return 0 <= pos.x && pos.x < GameMgmt.Get().data.map.width && 0 <= pos.y && pos.y < GameMgmt.Get().data.map.height;
        }
    }
}
