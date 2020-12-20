using System;
using Game;
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
                    Delimiter(ele.movement, data);
                    break;
                case "tile":
                    ele.defaultTile = Int(data);
                    break;
                case "winter":
                    ele.winter = data;
                    break;
                case "modi":
                    Delimiter(ele.modi, data);
                    break;
                case "res":
                    Delimiter(ele.res, data);
                    break;
                default:
                    base.ParseElement(ele, header, data);
                    break;
            }
        }

        [Obsolete]
        public DataTerrain Terrain(int id)
        {
            return ids[id];
        }

        public string GenDesc(int count)
        {
            foreach (var c in new int[]{0,10,25,60,100,500,1000})
            {
                if (count <= c)
                {
                    string s = S.T("resCount" + c);
                    return S.Debug() ? S.T("resCountDebug",s,count) : s;
                }
            }
            
            return S.T("resCountMore");
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