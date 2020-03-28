using System;
using Classes.MapGenerator;
using Game;
using Libraries.Terrains;
using Maps.GameMaps;
using UnityEngine;

namespace Libraries.MapGenerations
{
    public class UndergroundMapGenerator : BaseMapGenerator
    {

        protected override void CreateTile(GameMapDataLevel std, int x, int y, int layer, int[][] layerData, int height,
            DataTerrain invisible)
        {
            
            int oTerrain = std.At(new Vector3Int(x, y, layer));
            if (oTerrain == -1)
            {
                layerData[height - y - 1][x] = L.b.terrain["deep_wall"].defaultTile;
                return;
            }

            base.CreateTile(std, x, y, layer, layerData, height, invisible);
        }
    }
}