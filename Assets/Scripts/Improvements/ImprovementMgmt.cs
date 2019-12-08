using System;
using Game;
using Libraries;
using Terrains;
using Tools;
using UnityEngine;

namespace Improvements
{
    [Serializable]
    public class ImprovementMgmt : BaseMgmt<Improvement>
    {
        public ImprovementMgmt()
        {
            name = "Improvement";
        }

        protected override void ParseElement(Improvement ele, string header, string data)
        {
            switch (header)
            {
                case "modi":
                    Ref(ele.Modi, data);
                    break;
                case "combine":
                    ele.Combine = Bool(data);
                    break;
                case "file":
                    ele.File = data;
                    break;
                default:
                    Debug.LogWarning($"{name} missing {header} for data {data}");
                    break;
            }
        }

        public void Set(string id, Vector3Int pos)
        {
            Debug.Log($"Set {pos} to {id}");

            //set data
            GameMgmt.Get().data.map.levels[pos.z].Improvement[pos.x, pos.y] = id;
            
            //show it
            UpdateSprite(id, pos);
            
            //update neighbors?
            if (Data[id].Combine)
            {
                UpdateSprite(id, VectorHelper.Add(pos, 0, -1));
                UpdateSprite(id, VectorHelper.Add(pos, -1, 0));
                UpdateSprite(id, VectorHelper.Add(pos, 0, 1));
                UpdateSprite(id, VectorHelper.Add(pos, 0, 1));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        /// <returns>Improvement or null</returns>
        public Improvement At(Vector3Int pos)
        {
            if (GameMgmt.Get().data.map.levels[pos.z].Improvement[pos.x, pos.y] == null)
            {
                return null;
            }

            return Data[GameMgmt.Get().data.map.levels[pos.z].Improvement[pos.x, pos.y]];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        /// <returns>Improvement or null</returns>
        public bool Has(Vector3Int pos)
        {
            return (GameMgmt.Get().data.map.levels[pos.z].Improvement[pos.x, pos.y] != null);
        }

        private void UpdateSprite(string id, Vector3Int pos)
        {
            //valide pos?
            if (!GameHelper.Valide(pos))
            {
                return;
            }
            
            //remove or set?
            if (string.IsNullOrEmpty(GameMgmt.Get().data.map.levels[pos.z].Improvement[pos.x, pos.y]))
            {
                GameMgmt.Get().map.Levels[pos.z].Improvement.SetTile(pos.x, pos.y, null);
                return;
            }
            
            //show it
            GameMgmt.Get().map.Levels[pos.z].Improvement.SetTile(pos.x, pos.y, Data[id].CalcSprite(pos));
            
            //reset pathfinding
            GameMgmt.Get().map.Levels[pos.z].ResetPathFinding();
        }

        protected override Improvement Create()
        {
            return new Improvement();
        }
    }
}