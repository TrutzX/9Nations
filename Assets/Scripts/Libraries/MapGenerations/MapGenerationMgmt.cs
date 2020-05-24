using System;
using UnityEngine;

namespace Libraries.MapGenerations
{
    [Serializable]
    public class MapGenerationMgmt : BaseMgmt<MapGeneration>
    {
        public MapGenerationMgmt() : base("mapgeneration")
        {
        }

        protected override void ParseElement(MapGeneration ele, string header, string data)
        {
            if (header == "generator")
            {
                ele.generator = data;
                return;
            }

            if (header == "name")
            {
                base.ParseElement(ele, header, data);
                return;
            }

            //add it
            if (!string.IsNullOrEmpty(data))
                ele.terrains.Add(header, data);
        }
    }
}