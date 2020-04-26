using System.Collections.Generic;
using Game;
using GameMapLevels;
using Libraries;
using Maps.GameMaps;
using NesScripts.Controls.PathFind;
using Players;
using Tools;
using UnityEngine;

namespace Maps
{
    public class MapPathFinding
    {
        private Dictionary<string, PGrid> _grids;
        private int _id;

        public MapPathFinding(int id)
        {
            _grids = new Dictionary<string, PGrid>();
            _id = id;
        }

        public string Key(Player player, string moveType)
        {
            return player.id + "-" + moveType;
        }
        
        private void CalcGrid(Player player, string moveType)
        {
            GameMapData gmap = GameMgmt.Get().data.map;
            GameMapDataLevel level = GameMgmt.Get().newMap[_id].dataLevel;
            float[,] costMap = new float[gmap.width, gmap.height];

            for (int x = 0; x < gmap.width; x++)
            {
                for (int y = 0; y < gmap.height; y++)
                {
                    costMap[x, y] = L.b.modifiers["move"].CalcModi(level.Terrain(x, y).MoveCost(moveType), player,
                        new NVector(x, y, _id));// level.Terrain(x, y).MoveCost(moveType, nation);
                    //Debug.LogWarning($"Cost for {X},{Y} for {nation}-{moveType} is {costMap[x,y]}");
                }
            }
            
            _grids[Key(player,moveType)] = new PGrid(costMap);
        }

        public PGrid Get(Player player, string moveType)
        {
            string key = Key(player, moveType);
            if (!_grids.ContainsKey(key))
            {
                CalcGrid(player,moveType);
            }

            return _grids[key];
        }

        public void ResetAll()
        {
            _grids.Clear();
        }

        public void Reset(Player player, string moveType)
        {
            _grids.Remove(Key(player, moveType));
        }

        /// <summary>
        /// Calc the route for moving, ignore the level vector, only 2d
        /// </summary>
        /// <param name="player"></param>
        /// <param name="moveType"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<PPoint> Path(Player player, string moveType, NVector start, NVector end)
        {
            return Pathfinding.FindPath(Get(player,moveType), new PPoint(start.x, start.y), new PPoint(end.x, end.y));
        }

        public int CostNode(Player player, string moveType, NVector point)
        {
            return (int) Get(player, moveType).nodes[point.x, point.y].price;
        }

        /// <summary>
        /// Calc the points for moving, ignore the level vector, only 2d
        /// </summary>
        /// <param name="player"></param>
        /// <param name="moveType"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public int Cost(Player player, string moveType, NVector start, NVector end)
        {
            List<PPoint> points = Path(player,moveType, start, end);
            PGrid grid = Get(player, moveType);
            int price = 0;

            foreach (PPoint point in points)
            {
                price += (int)grid.nodes[point.x,point.y].price;
            }

            return price;
        }
    }
}