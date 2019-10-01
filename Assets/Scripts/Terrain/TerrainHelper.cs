using System;
using DataTypes;

namespace Help
{
    public class TerrainHelper
    {
        public static int GetMoveCost(NTerrain terrain, string moveTyp, string nation)
        {
            int cost = GetBaseMoveCost(terrain, moveTyp);
            if (terrain.name == Data.nation[nation].hometerrain)
            {
                cost = Math.Max(0,cost-5);
            }

            return cost;
        }
        
        public static int GetBaseMoveCost(NTerrain terrain, string moveTyp)
        {
            return moveTyp=="walk"?terrain.walk:moveTyp=="fly"?terrain.fly:terrain.swim;
        }

        public static int GetViewRadius(int x, int y, int visible)
        {
            NTerrain n = MapMgmt.Get().GetTerrain(x, y);
            return Math.Max(0, n.visible + visible);
        }
    }
}