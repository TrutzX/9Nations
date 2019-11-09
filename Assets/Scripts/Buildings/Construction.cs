using System.Collections.Generic;
using System.Linq;
using Buildings;
using Towns;
using UnityEngine;

namespace Game
{
    public class Construction : MonoBehaviour
    {
        private BuildingUnitData data;
        private MapElementInfo info;

        public void Init(BuildingUnitData data, Dictionary<string, int> construction, MapElementInfo info, int buildTime)
        {
            data.construction = construction;
            data.construction.Add("buildtime",buildTime+1);
            data.buildTime = buildTime;
            this.data = data;
            this.info = info;
        }

        public void Load(BuildingUnitData data)
        {
            this.info = GetComponent<MapElementInfo>();
            this.data = data;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>true == under construction</returns>
        public bool RoundConstruct()
        {
            Town t = info.Town();
        
            foreach (KeyValuePair<string, int> cost in data.construction.ToDictionary(entry => entry.Key,entry => entry.Value))
            {
                //buildtime?
                if (cost.Key == "buildtime")
                {
                    continue;
                }
                
                //normal ress?
                if (t.GetRes(cost.Key) >= cost.Value)
                {
                    t.AddRes(cost.Key, -cost.Value);
                    data.construction.Remove(cost.Key);
                } else if (t.GetRes(cost.Key) >= 1)
                {
                    int val = t.GetRes(cost.Key);
                    data.construction[cost.Key] -= val;
                    t.AddRes(cost.Key,-val);
                    info.SetLastInfo($"Need {data.construction[cost.Key]} more {Data.ress[cost.Key].name} for construction of {data.name}.");
                }
                else
                {
                    info.SetLastInfo($"Need {data.construction[cost.Key]} more {Data.ress[cost.Key].name} for construction of {data.name}.");
                }
            }
        
            //buildtime
            if (data.construction["buildtime"] > 0)
            {
                data.construction["buildtime"] -= 1;
                //set opacity
                GetComponent<SpriteRenderer>().color = new Color(1,1,1,GetConstructionProcent());
                
            }

            //finish?
            if (data.construction.Count == 1 && data.construction["buildtime"] == 0)
            {
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                data.construction = null;
                data.buildTime = 0;
                info.SetLastInfo($"Finish construction of {info.name}.");
                info.FinishConstruct();
                Destroy(this);
                return false;
            }

            return true;
        }

        public float GetConstructionProcent()
        {
            if (data.construction == null)
            {
                return 1;
            }
            return (1f * data.buildTime - data.construction["buildtime"]) / data.buildTime;
        }

        public bool IsUnderConstrution()
        {
            return data.construction != null;
        }
    }
}