using System;
using System.Collections.Generic;
using Classes;
using Classes.MapGenerator;
using Game;
using Maps;
using Maps.GameMaps;
using UnityEngine;

namespace Libraries.MapGenerations
{
    [Serializable]
    public class MapGeneration : BaseData
    {
        public Dictionary<string, string> terrains;
        public string generator;

        public MapGeneration()
        {
            terrains = new Dictionary<string, string>();
        }

        public BaseMapGenerator Generator()
        {
            LClass.s.mapGenerators[generator].mapGeneration = this;
            return LClass.s.mapGenerators[generator];
        }
    }
}