using System;
using System.Collections.Generic;
using Game;
using Libraries;
using Libraries.Nations;
using Libraries.Terrains;
using Maps;
using Tools;
using UI;
using Units;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace GameMapLevels
{
    public class GameMapTools
    {
        private readonly GameMap _map;
        private readonly Dictionary<string, Tile> _tileCache;

        public GameMapTools(GameMap map)
        {
            _map = map;
            _tileCache = new Dictionary<string, Tile>();
        }

        public Tile GetTile(string path, string bid=null)
        {
            string id = bid == null ? path : path + bid;
            if (_tileCache.ContainsKey(id))
            {
                return _tileCache[id];
            }
            
            Tile t = ScriptableObject.CreateInstance<Tile>();
            t.sprite = SpriteHelper.Load(path);
            _tileCache[id] = t;
            return t;
        }

        /// <summary>
        /// Get a start position or an exception
        /// </summary>
        /// <param name="nation"></param>
        /// <returns></returns>
        /// <exception cref="System.MissingMemberException"></exception>
        public NVector GetStartPos(string nation)
        {
            GameMapData gmap = GameMgmt.Get().data.map;
            
            Nation n = L.b.nations[nation];
            int i = 0;
            while (i < 1000)
            {
                int x = Random.Range(0, gmap.width);
                int y = Random.Range(0, gmap.height);
                DataTerrain t = _map.Terrain(new NVector(x, y, gmap.standard));
            
                //right terrain?
                //TODO  && t.visible > 0
                if (t.id == n.Terrain || (t.MoveCost("walk") > 0 && t.MoveCost("walk") <= 10))
                {
                    //has a unit?
                    if (S.Unit().Free(new NVector(x,y,gmap.standard)))
                        //TODO find right spot with level support
                        return new NVector(x,y,gmap.standard);
                }

                i++;
            }
            
            Debug.Log($"{gmap.standard}/{gmap.levels.Count}");

            NVector pos = new NVector(Random.Range(0, gmap.width), Random.Range(0, gmap.height), gmap.standard);
            Debug.LogError($"Can not find a start position for {nation} using {pos}");
            return pos;
        }
    }
}