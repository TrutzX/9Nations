using System;
using Game;
using Libraries.Terrains;
using Maps;
using Maps.GameMaps;
using Tools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Classes.Elements
{
    public class ShadowElement : LightElement
    {
        public ShadowElement() : base ("shadow") {}

        protected override NVector CreateNewSpot()
        {
            int level = Math.Max(GameMgmt.Get().data.map.standard - 1, 0); //underground
            GameMapData gmap = GameMgmt.Get().data.map;
            GameMapDataLevel gmdl = GameMgmt.Get().data.map.levels[level];
            
            int i = 0;
            while (i < 1000)
            {
                i++;
                int x = Random.Range(0, gmap.width);
                int y = Random.Range(0, gmap.height);
                DataTerrain t = gmdl.Terrain(x, y);
            
                
                if (t.MoveCost("float") == 0 || t.MoveCost("float") > 10) continue; // can walk?
                if (!S.Unit().Free(new NVector(x,y,gmap.standard))) continue; //near water?
                
                return new NVector(x,y,level);
            }
            
            Debug.Log($"{level}/{gmap.levels.Count}");

            NVector pos = new NVector(Random.Range(0, gmap.width), Random.Range(0, gmap.height), gmap.standard);
            Debug.LogError($"Can not find a start position using {pos}");
            return pos;
        }
    }
}