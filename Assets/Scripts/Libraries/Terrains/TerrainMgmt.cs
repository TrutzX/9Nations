using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Libraries.Terrains
{
    [Serializable]
    public class TerrainMgmt : BaseMgmt<DataTerrain>
    {
        [SerializeField] private DataTerrain[] ids;
        public TerrainMgmt() : base("terrain")
        {
            ids = new DataTerrain[70];
        }

        public DataTerrain this[int key]
        {
            get
            {
                Assert.IsTrue(key >= 0 && key < ids.Length,$"Key {key} > {ids.Length} is to big");
                Assert.IsNotNull(ids[key],$"Key {key} is missing");
                return ids[key];
            }
        }

        protected override void ParseElement(DataTerrain ele, string header, string data)
        {
            switch (header)
            {
                case "movement":
                    Delimiter(ele.Movement, data);
                    break;
                case "tile":
                    ele.defaultTile = Int(data);
                    break;
                case "winter":
                    ele.winter = data;
                    break;
                case "category":
                    ele.category = data;
                    break;
                case "modi":
                    Delimiter(ele.modi, data);
                    break;
                case "res":
                    Delimiter(ele.Res, data);
                    break;
                default:
                    Debug.LogWarning($"{name} missing {header} for data {data}");
                    break;
            }
        }

        [Obsolete]
        public DataTerrain Terrain(int id)
        {
            return ids[id];
        }
        
        protected override DataTerrain Create()
        {
            return new DataTerrain();
        }

        public string GenDesc(int count)
        {
            string b = global::Data.features.debug.Bool() ? $" ({count})" : "";

            if (count == 0) return "none"+b;
            if (count < 10) return "some"+b;
            if (count < 25) return "several"+b;
            if (count < 60) return "multiple"+b;
            if (count < 100) return "more"+b;
            if (count < 500) return "much"+b;
            if (count < 1000) return "more"+b;
            return "numerous"+b;
        }

        public override void AfterLoad()
        {
            base.AfterLoad();
            foreach (DataTerrain t in Values())
            {
                
                if (t.defaultTile != -1)
                {
                    ids[t.defaultTile] = t;
                }
            }
        }
    }
}