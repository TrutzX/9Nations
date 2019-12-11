using System;
using System.Collections.Generic;
using System.Linq;
using Libraries;
using Terrains;
using UnityEngine;
using UnityEngine.Serialization;

namespace Maps
{
    [Serializable]
    public class GameMapDataLevel
    {
        private const int DefaultTerrain = 6;
        
        public string name;

        [SerializeField] private List<int[,]> terrains;
        [SerializeField] private List<int[,]> winterTerrains;

        public string[,] Improvement;

        public GameMapDataLevel()
        {
            terrains = new List<int[,]>();
            winterTerrains = new List<int[,]>();
        }

        public void AddLayer(int [][] data)
        {
            int height = data.Length;
            int[,] d = new int[data[0].Length,data.Length];
            int[,] w = new int[data[0].Length,height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < data[0].Length; x++)
                {
                    d[x, height-1-y] = data[y][x];
                    string b = data[y][x]==-1?null:L.b.terrain.Terrain(data[y][x]).Winter;
                    w[x, height-1-y] = string.IsNullOrEmpty(b)?data[y][x]:L.b.terrain[b].DefaultTile;
                }
            }
            
            terrains.Add(d);
            winterTerrains.Add(w);
        }

        public int At(Vector3Int pos, bool winter = false)
        {
            return winter?winterTerrains[pos.z][pos.x,pos.y]:terrains[pos.z][pos.x,pos.y];
        }

        public void Set(Vector3Int pos, int val, bool winter = false)
        {
            _ = winter?winterTerrains[pos.z][pos.x,pos.y] = val:terrains[pos.z][pos.x,pos.y] = val;
        }

        public int LayerCount()
        {
            return terrains.Count;
        }

        public int Width()
        {
            return terrains.First().GetLength(0);
        }

        public int Height()
        {
            return terrains.First().GetLength(1);
        }
        
        public BTerrain Terrain(int x, int y, bool winter = false)
        {
            //Vector3Int v = new Vector3Int(x * 2, y * 2, 0);
            for (int z = terrains.Count - 1; z >= 0; z--)
            {
                //Debug.LogWarning(z+" "+x+" "+y+" "+DataLevel.Terrains.Count);
                int d = winter?winterTerrains[z][x,y]:terrains[z][x,y];

                if (d == -1)
                {
                    continue;
                }
                
                return L.b.terrain.Terrain(d);
            }
            
            return L.b.terrain.Terrain(DefaultTerrain);
        }
    }
}