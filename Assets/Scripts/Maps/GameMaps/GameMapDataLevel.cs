using System;
using System.Collections.Generic;
using System.Linq;
using Game;
using Libraries;
using Libraries.Terrains;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace Maps.GameMaps
{
    [Serializable]
    public class GameMapDataLevel
    {
        private const int DefaultTerrain = 6;
        
        public string name;
        public string generate;
        public int level;
        public bool isWinter;

        [SerializeField] private Dictionary<string,int>[,] resGenerate;
        [SerializeField] private List<int[,]> terrains;
        [SerializeField] private List<int[,]> winterTerrains;

        public string[,] improvement;

        public GameMapDataLevel()
        {
            terrains = new List<int[,]>();
            winterTerrains = new List<int[,]>();
        }

        public void Init(int level, string name)
        {
            this.name = $"{level} {name}";
            this.level = level;
            improvement = new string[GameMgmt.Get().data.map.width,GameMgmt.Get().data.map.height];
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
                    string b = data[y][x]==-1?null:L.b.terrain[data[y][x]].winter;
                    w[x, height-1-y] = string.IsNullOrEmpty(b)?data[y][x]:L.b.terrain[b].defaultTile;
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
        
        public DataTerrain Terrain(int x, int y, bool winter = false)
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
                
                return L.b.terrain[d];
            }
            
            return L.b.terrain[DefaultTerrain];
        }

        public bool TerrainNear(int x, int y, string terrain, int radius)
        {
            for (int i = Math.Max(0, x-radius); i < Math.Min(Width(), x+radius); i++)
            {
                for (int j = Math.Max(0, y-radius); j < Math.Min(Height(), y+radius); j++)
                {
                    if (Terrain(i, j).id == terrain) return true;
                }
            }

            return false;
        }
        
        public int ResGen(int x, int y, string res)
        {
            //has it?
            return ResGenContains(x, y, res) ? resGenerate[x, y][res] : -1;
        }

        public bool ResGenContains(int x, int y, string res)
        {
            //has it?
            if (resGenerate[x, y] == null) return false;
            if (!resGenerate[x, y].ContainsKey(res)) return false;
            return true;
        }
        
        public void ResGenAdd(int x, int y, string res, int amount)
        {
            //has it?
            if (resGenerate[x, y] == null)
            {
                resGenerate[x, y] = new Dictionary<string, int>();
            }
            if (!resGenerate[x, y].ContainsKey(res))
            {
                resGenerate[x, y].Add(res,amount);
            }
            else
            {
                resGenerate[x, y][res] += amount;
            }
            
            //remove terrain?
            if (resGenerate[x, y][res] > 0) return;
            resGenerate[x, y][res] = 0;
            
            //find highest layer
            for (int layer = LayerCount()-1; layer >= 0; layer++)
            {
                Vector3Int v = new Vector3Int(x,y,layer);
                if (At(v)==-1) continue;
                Set(v, -1);
                Set(v, -1, true);
                //rebuild
                
            }
        }
        
        public Dictionary<string, int>.KeyCollection ResGenKey(int x, int y)
        {
            return resGenerate[x, y]?.Keys;
        }
        
        public void FinishBuild()
        {
            //check generate
            if (terrains.Count == 0)
            {
                Assert.IsNotNull(generate,$"Data and generate for level {name} is missing.");
                L.b.mapGeneration[generate].Generator().Generate(this);
            }
            
            //set res
            resGenerate = new Dictionary<string,int>[Width(),Height()];
            for (int x = 0; x < Width(); x++)
            {
                for (int y = 0; y < Height(); y++)
                {
                    DataTerrain bt = Terrain(x, y);
                    //has res?
                    if (bt.Res.Count == 0) continue;
                    
                    //add it
                    resGenerate[x,y] = new Dictionary<string, int>();
                    foreach (KeyValuePair<string, string> r in bt.Res)
                    {
                        string[] c = r.Value.Split('-');
                        resGenerate[x,y].Add(r.Key,Random.Range(Int32.Parse(c[0]), Int32.Parse(c[1])));
                    }
                }
                
            }
        }
    }
}