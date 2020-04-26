using Libraries;
using Libraries.Terrains;
using Maps.GameMaps;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Classes.MapGenerator
{
    public class UndergroundMapGenerator : BaseMapGenerator
    {

        protected override void CreateTile(GameMapDataLevel std, int x, int y, int layer, int[][] layerData, int height,
            DataTerrain invisible)
        {
            int oTerrain = std.At(new Vector3Int(x, y, layer));
            
            if (oTerrain == -1 && Random.Range(0,10) <= 6) // 60%
            {
                layerData[height - y - 1][x] = L.b.terrains["deep_wall"].defaultTile;
                return;
            }

            base.CreateTile(std, x, y, layer, layerData, height, invisible);
        }
    }
}