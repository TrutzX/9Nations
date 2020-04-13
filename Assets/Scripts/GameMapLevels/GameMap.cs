using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game;
using IniParser.Model;
using Libraries;
using Libraries.Maps;
using Libraries.Terrains;
using Maps;
using Maps.GameMaps;
using Maps.TileMaps;
using Tools;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace GameMapLevels
{
    public class GameMap : MonoBehaviour
    {
        public GameMapLevel prototypeGameMapLevel;
        public TileBase[] prototypeSelected;
        public TileBase prototypeFog;
        public TileBase prototypeBorder;
        public GameMapView view;
        public GameMapTools tools;
        
        public List<GameMapLevel> levels;

        public GameMap()
        {
            levels = new List<GameMapLevel>();
            view = new GameMapView(this);
            tools = new GameMapTools(this);
        }
        
        public GameMapLevel this[int id] => levels[id];
        
        public IEnumerator CreateMap()
        {
            //can load?
            if (GameMgmt.Get().data.map.id == null)
            {
                //TODO show error
                yield return GameMgmt.Get().load.ShowSubMessage($"Map is missing for creation. Please restart 9 Nations");
                yield break;
            }
            //load Map
            yield return GameMgmt.Get().load.ShowSubMessage($"Create Map data");

            //read data
            DataMap map = LSys.tem.maps[GameMgmt.Get().data.map.id];
            GameMapData gmap = GameMgmt.Get().data.map;
            IniData data = map.Config();
            
            //set size
            gmap.width = map.width;
            gmap.height = map.height;
            
            //add level
            foreach (SectionData section in data.Sections)
            {
                GameMapDataLevel n = new GameMapDataLevel();
                n.Init(gmap.levels.Count,section.SectionName);
                gmap.levels.Add(n);
                yield return GameMgmt.Get().load.ShowSubMessage($"Reading {n.name} map data");

                if (data[section.SectionName].ContainsKey("generate"))
                {
                    n.generate = data[section.SectionName]["generate"];
                    continue;
                }
                
                if (data[section.SectionName].ContainsKey("standard"))
                {
                    gmap.standard = gmap.levels.Count()-1;
                }
                
                //add layer
                int m = Int32.Parse(data[section.SectionName]["layer"]);
                for (int i = 0; i < m; i++) 
                {
                    yield return GameMgmt.Get().load.ShowSubMessage($"Reading {n.name} map data {i}/{m}");
                    n.AddLayer(map.Layer(data[section.SectionName]["format"],i));
                }
                
            }
            
            //check generate
            foreach (GameMapDataLevel gmdl in gmap.levels)
            {
                yield return GameMgmt.Get().load.ShowSubMessage($"Finishing layer {gmdl.name}");
                gmdl.FinishBuild();
            }
        
            Debug.Log($"Load Map {map.name} ({gmap.levels.Count}:{gmap.width}/{gmap.height})");
            
            yield return CreateLayers();
        }

        public DataTerrain Terrain(NVector pos)
        {
            return levels[pos.level].dataLevel.Terrain(pos.x, pos.y);
        }
        
        public IEnumerator LoadMap()
        {
            yield return GameMgmt.Get().load.ShowSubMessage($"Loading Map data");
            //load Map
            yield return CreateLayers();
        }

        private IEnumerator CreateLayers()
        {
            GameMapData gmap = GameMgmt.Get().data.map;
            
            //load layer
            levels = new List<GameMapLevel>();
            for (int i = 0; i < gmap.levels.Count; i++)
            {
                GameMapLevel g = CreateNewLevel(gmap.levels[i].name, i);
                yield return g.AddLayers();
            }
            view.Init();
        }

        private GameMapLevel CreateNewLevel(string name, int level)
        {
            GameMapLevel gml = Instantiate<GameMapLevel>(prototypeGameMapLevel, transform);
            gml.Init(level);
            gml.gameObject.name = name;
            levels.Add(gml);
            return gml;
        }

        public MapPathFinding PathFinding(int level)
        {
            return levels[level].PathFinding();
        }

        public void NextRound()
        {
            levels.ForEach(l => l.NextRound());
        }
    }
}
