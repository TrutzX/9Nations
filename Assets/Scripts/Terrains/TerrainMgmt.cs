using System;
using Libraries;
using UI;
using UnityEngine;

namespace Terrains
{
    [Serializable]
    public class TerrainMgmt : BaseMgmt<BTerrain>
    {
        [SerializeField] private BTerrain[] _ids;
        public TerrainMgmt()
        {
            name = "Terrain";
            _ids = new BTerrain[70];
        }

        protected override void ParseElement(BTerrain ele, string header, string data)
        {
            switch (header)
            {
                case "walk":
                    ele.Walk = Int(data);
                    break;
                case "fly":
                    ele.Fly = Int(data);
                    break;
                case "swim":
                    ele.Swim = Int(data);
                    break;
                case "tile":
                    ele.DefaultTile = Int(data);
                    break;
                case "winter":
                    ele.Winter = data;
                    break;
                case "category":
                    ele.Category = data;
                    break;
                case "modi":
                    Ref(ele.Modi, data);
                    break;
                default:
                    Debug.LogWarning($"{name} missing {header} for data {data}");
                    break;
            }
        }

        public BTerrain Terrain(int id)
        {
            return _ids[id];
        }
        
        protected override BTerrain Create()
        {
            return new BTerrain();
        }

        public override void AfterLoad()
        {
            base.AfterLoad();
            foreach (BTerrain t in Values())
            {
                
                if (t.DefaultTile != -1)
                {
                    _ids[t.DefaultTile] = t;
                }
            }
        }
    }
}