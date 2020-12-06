using System;
using System.Collections.Generic;
using System.Linq;
using Game;
using Libraries;
using MapElements;
using Towns;
using UnityEngine;

namespace Buildings
{
    public class Construction : MonoBehaviour
    {
        private BuildingUnitData _data;
        private MapElementInfo _info;

        public void Init(BuildingUnitData data, Dictionary<string, int> construction, MapElementInfo info, int buildTime)
        {
            data.constructionOrg = new Dictionary<string, int>(construction);
            data.construction = construction;
            data.construction.Add(C.BuildRes,buildTime+1);
            data.buildTime = buildTime;
            this._data = data;
            this._info = info;
        }

        public void Load(BuildingUnitData data)
        {
            this._info = GetComponent<MapElementInfo>();
            this._data = data;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>true == under construction</returns>
        public bool NextRound()
        {
            Town t = _info.Town();
        
            foreach (KeyValuePair<string, int> cost in _data.construction.ToDictionary(entry => entry.Key,entry => entry.Value))
            {
                //buildtime?
                if (cost.Key == C.BuildRes)
                {
                    continue;
                }
                
                //normal ress?
                if (t.GetRes(cost.Key) >= cost.Value)
                {
                    t.AddRes(cost.Key, -cost.Value, ResType.Construction);
                    _data.construction.Remove(cost.Key);
                } else if (t.GetRes(cost.Key) >= 1)
                {
                    int val = t.GetRes(cost.Key);
                    _data.construction[cost.Key] -= val;
                    t.AddRes(cost.Key,-val, ResType.Construction);
                    _info.SetLastInfo($"Need {_data.construction[cost.Key]} more {L.b.res[cost.Key].Name()} for construction of {_data.name}.");
                }
                else
                {
                    _info.SetLastInfo($"Need {_data.construction[cost.Key]} more {L.b.res[cost.Key].Name()} for construction of {_data.name}.");
                }
            }
        
            //buildtime
            if (_data.construction[C.BuildRes] > 0)
            {
                _data.construction[C.BuildRes] -= 1;
                //set opacity
                GetComponent<SpriteRenderer>().color = new Color(GetConstructionProcent(),1,GetConstructionProcent(),0.5f+GetConstructionProcent()/2);
            }

            //finish?
            if (_data.construction.Count == 1 && _data.construction[C.BuildRes] == 0)
            {
                _data.construction = null;
                _data.buildTime = 0;
                _info.FinishConstruct();
                return false;
            }

            return true;
        }

        public float GetConstructionProcent()
        {
            if (_data.construction == null)
            {
                return 1;
            }
            return (1f * _data.buildTime - _data.construction[C.BuildRes]) / _data.buildTime;
        }

        public bool IsUnderConstruction()
        {
            return _data.construction != null;
        }
    }
}