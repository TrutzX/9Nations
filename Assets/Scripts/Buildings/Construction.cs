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
        private IMapElement info;

        public void Init(BuildingUnitData data, Dictionary<string, int> construction, IMapElement info, int buildTime, int town)
        {
            data.construction = construction;
            data.construction.Add("buildtime",buildTime-1);
            data.buildTime = buildTime;
            this.data = data;
            this.info = info;
        }

        public void Load(BuildingUnitData data)
        {
            this.data = data;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>true == under construction</returns>
        public bool RoundConstruct()
        {
            Town t = TownMgmt.Get(data.town);
        
            foreach (KeyValuePair<string, int> cost in data.construction.ToDictionary(entry => entry.Key,entry => entry.Value))
            {
                //buildtime?
                if (cost.Key == "buildtime")
                {
                    continue;
                }
                
                //normal ress?
                if (t.GetRess(cost.Key) >= cost.Value)
                {
                    t.AddRess(cost.Key, -cost.Value);
                    data.construction.Remove(cost.Key);
                } else if (t.GetRess(cost.Key) >= 1)
                {
                    int val = t.GetRess(cost.Key);
                    data.construction[cost.Key] -= val;
                    t.AddRess(cost.Key,-val);
                    data.lastError = $"Need {data.construction[cost.Key]} more {Data.ress[cost.Key].name} for construction.";
                }
                else
                {
                    data.lastError = $"Need {data.construction[cost.Key]} more {Data.ress[cost.Key].name} for construction.";
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
                return 100;
            }
            return (1f * data.buildTime - data.construction["buildtime"]) / data.buildTime;
        }

        public bool IsUnderConstrution()
        {
            return data.construction != null;
        }
    }
}