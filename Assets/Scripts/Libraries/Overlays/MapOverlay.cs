using System;
using System.Collections.Generic;
using Game;
using Maps.GameMaps;
using Players;
using Tools;
using UnityEngine;

namespace Libraries.Overlays
{
    [Serializable]
    public class MapOverlay
    {
        [SerializeField] private Dictionary<string, List<int[,]>> data;

        public MapOverlay()
        {
            data = new Dictionary<string, List<int[,]>>();
        }
        
        public void Set(string id, NVector pos, int val)
        {
            if (!data.ContainsKey(id))
            {
                data[id] = new List<int[,]>();
                foreach (GameMapDataLevel l in GameMgmt.Get().data.map.levels)
                {
                    data[id].Add(new int[GameMgmt.Get().data.map.width,GameMgmt.Get().data.map.height]);
                }
            }

            data[id][pos.level][pos.x, pos.y] = val;
        }
        
        public void Add(string id, NVector pos, int val)
        {
            Set(id, pos, Get(id, pos) + val);
        }
        
        public void Add(string id, List<NVector> pos, int val)
        {
            foreach (var p in pos)
            {
                Add(id, p, val);
            }
        }
        
        public int Get(string id, NVector pos)
        {
            if (!data.ContainsKey(id))
            {
                return 0;
            }

            return data[id][pos.level][pos.x, pos.y];
        }
        
        public void ViewOverlay(string id)
        {
            L.b.playerOptions["overlay"].SetValue(id);
            var o = S.Map().levels[S.Map().view.ActiveLevel].overlay;

            //clear it
            for (int x = 0; x < GameMgmt.Get().data.map.width; x++)
            {
                for (int y = 0; y < GameMgmt.Get().data.map.height; y++)
                {
                    o.SetTile(x,y);
                }
            }
            
            if (string.IsNullOrEmpty(id))
            {
                return;
            }

            //build it
            L.b.overlays[id].RunCode().Run(S.ActPlayer(), o);
        }
    }
}