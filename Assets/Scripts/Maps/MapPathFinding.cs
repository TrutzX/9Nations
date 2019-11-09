using System.Collections.Generic;
using DataTypes;
using Game;
using Help;
using Maps;
using NesScripts.Controls.PathFind;
using UnityEngine;

namespace Map
{
    public class MapPathFinding
    {
        private Dictionary<string, PGrid> _grids;

        public MapPathFinding()
        {
            _grids = new Dictionary<string, PGrid>();
        }

        public string Key(string nation, string moveType)
        {
            return nation + "-" + moveType;
        }
        
        private void CalcGrid(string nation, string moveType)
        {
            float[,] costMap = new float[GameMgmt.Get().data.mapwidth, GameMgmt.Get().data.mapheight];

            for (int x = 0; x < GameMgmt.Get().data.mapwidth; x++)
            {
                for (int y = 0; y < GameMgmt.Get().data.mapheight; y++)
                {
                    NTerrain land = MapMgmt.Get().GetTerrain(x, y);
                    costMap[x,y] = TerrainHelper.GetMoveCost(land, moveType, nation);
                    //Debug.LogWarning($"Cost for {X},{Y} for {nation}-{moveType} is {costMap[x,y]}");
                }
            }
            
            _grids[Key(nation,moveType)] = new PGrid(costMap);
        }

        public PGrid Get(string nation, string moveType)
        {
            string key = Key(nation, moveType);
            if (!_grids.ContainsKey(key))
            {
                CalcGrid(nation,moveType);
            }

            return _grids[key];
        }

        public List<PPoint> Path(string nation, string moveType, int startX, int startY, int endX, int endY)
        {
            return Pathfinding.FindPath(Get(nation,moveType), new PPoint(startX, startY), new PPoint(endX, endY));
        }

        public int Cost(string nation, string moveType, int startX, int startY, int endX, int endY)
        {
            List<PPoint> points = Path(nation,moveType, startX,startY,endX,endY);
            PGrid grid = Get(nation, moveType);
            int price = 0;

            foreach (PPoint point in points)
            {
                price += (int)grid.nodes[point.x,point.y].price;
            }

            return price;
        }
    }
}