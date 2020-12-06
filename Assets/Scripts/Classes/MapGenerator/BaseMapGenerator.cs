using System;
using Game;
using Libraries;
using Libraries.MapGenerations;
using Libraries.Terrains;
using Maps.GameMaps;
using UnityEngine;

namespace Classes.MapGenerator
{
    public class BaseMapGenerator
    {
        public MapGeneration mapGeneration;
        
        public void Generate(GameMapDataLevel gmdl)
        {
            int height = GameMgmt.Get().data.map.height;
            DataTerrain invisible = L.b.terrains["invisible"];
            GameMapDataLevel std = GameMgmt.Get().data.map.levels[GameMgmt.Get().data.map.standard];
            
            Debug.Log($"Generate layer {mapGeneration.Name()}:{GameMgmt.Get().data.map.width}/{height}");
            
            for(int layer = 0; layer < std.LayerCount(); layer++)
            {
                int[][] layerData = new int[height][];
                for (int y = 0; y < height; y++)
                {
                    layerData[height-y-1] = new int[GameMgmt.Get().data.map.width];
                    for (int x = 0; x < GameMgmt.Get().data.map.width; x++)
                    {
                        CreateTile(std, x, y, layer, layerData, height, invisible);
                    }
                }
                gmdl.AddLayer(layerData);
            }
        }

        protected virtual void CreateTile(GameMapDataLevel std, int x, int y, int layer, int[][] layerData, int height,
            DataTerrain invisible)
        {
            int oTerrain = std.At(new Vector3Int(x, y, layer));
            if (oTerrain == -1)
            {
                layerData[height - y - 1][x] = -1;
                return;
            }

            DataTerrain org = L.b.terrains[oTerrain];

            //has element?
            try
            {
                if (mapGeneration.terrains.ContainsKey(org.id))
                {
                    layerData[height - y - 1][x] = L.b.terrains[mapGeneration.terrains[org.id]].defaultTile;
                }
                else if (layer == 0)
                {
                    layerData[height - y - 1][x] = invisible.defaultTile;
                }
                else
                {
                    layerData[height - y - 1][x] = -1;
                }
            }
            catch (Exception e)
            {
                Debug.Log($"{y}/{height - y - 1}/{height},{x}/{GameMgmt.Get().data.map.width}");
                throw e;
            }
        }
    }
}