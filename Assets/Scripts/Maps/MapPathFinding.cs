using System.Collections.Generic;
using DataTypes;
using Game;
using Help;
using Libraries;
using Maps;
using Maps.GameMap;
using NesScripts.Controls.PathFind;
using Players;
using Terrains;
using UnityEngine;

namespace Map
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
            GameMapLevel level = GameMgmt.Get().map.Levels[_id];
            float[,] costMap = new float[gmap.width, gmap.height];

            for (int x = 0; x < gmap.width; x++)
            {
                for (int y = 0; y < gmap.height; y++)
                {
                    costMap[x, y] = L.b.modifiers["move"].CalcModi(level.Terrain(x, y).MoveCost(moveType), player,
                        new Vector3Int(x, y, _id));// level.Terrain(x, y).MoveCost(moveType, nation);
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

        public List<PPoint> Path(Player player, string moveType, int startX, int startY, int endX, int endY)
        {
            return Pathfinding.FindPath(Get(player,moveType), new PPoint(startX, startY), new PPoint(endX, endY));
        }

        public int Cost(Player player, string moveType, Vector3Int start, int endX, int endY)
        {
            List<PPoint> points = Path(player,moveType, start.x,start.y,endX,endY);
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