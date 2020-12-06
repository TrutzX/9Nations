using System;
using Game;
using Libraries;
using Libraries.Improvements;
using Tools;
using UnityEngine;

namespace Improvements
{
    [Serializable]
    public class ImprovementMgmt : BaseMgmt<Improvement>
    {
        public ImprovementMgmt() : base("improvement") { }

        protected override void ParseElement(Improvement ele, string header, string data)
        {
            switch (header)
            {
                case "modi":
                    Delimiter(ele.modi, data);
                    break;
                case "combine":
                    ele.combine = Bool(data);
                    break;
                default:
                    base.ParseElement(ele, header, data);
                    break;
            }
        }

        public void Set(string id, NVector pos)
        {
            Debug.Log($"Set {pos} to {id}");

            //set data
            GameMgmt.Get().data.map.levels[pos.level].improvement[pos.x, pos.y] = id;
            
            //show it
            UpdateSprite(id, pos);
            
            //update neighbors?
            if (Data[id].combine)
            {
                UpdateSprite(id, pos.DiffX(-1));
                UpdateSprite(id, pos.DiffX(1));
                UpdateSprite(id, pos.DiffY(-1));
                UpdateSprite(id, pos.DiffY(1));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        /// <returns>Improvement or null</returns>
        public Improvement At(NVector pos)
        {
            if (GameMgmt.Get().data.map.levels[pos.level].improvement[pos.x, pos.y] == null)
            {
                return null;
            }

            return Data[GameMgmt.Get().data.map.levels[pos.level].improvement[pos.x, pos.y]];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        /// <returns>Improvement or null</returns>
        public bool Has(NVector pos)
        {
            return (GameMgmt.Get().data.map.levels[pos.level].improvement[pos.x, pos.y] != null);
        }

        private void UpdateSprite(string id, NVector pos)
        {
            //valide pos?
            if (!pos.Valid()) return;
            
            //remove or set?
            if (string.IsNullOrEmpty(GameMgmt.Get().data.map.levels[pos.level].improvement[pos.x, pos.y]))
            {
                GameMgmt.Get().newMap[pos.level].improvement.SetTile(pos.x, pos.y, null);
                return;
            }
            
            //show it
            GameMgmt.Get().newMap[pos.level].improvement.SetTile(pos.x, pos.y, Data[id].CalcSprite(pos));
            
            //reset pathfinding
            GameMgmt.Get().newMap.levels[pos.level].ResetPathFinding();
        }
    }
}