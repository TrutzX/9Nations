using System;
using System.Collections.Generic;
using Game;
using Libraries;
using Libraries.Buildings;
using Tools;

namespace MapElements
{
    public class CalculatedData
    {
        public int hp;
        public int ap;
        public int buildTime;
        
        private CalculatedData(){}

        /// <summary>
        /// Calculate the ap & hp based on this material
        /// </summary>
        /// <param name="ele"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        public static CalculatedData Calc(BaseDataBuildingUnit ele, Dictionary<string, int> cost)
        {
            float hp = ele.hp;
            float ap = ele.ap;
            decimal buildTime = 0;
            int buildCount = 0;

            foreach (var r in cost)
            {
                var res = L.b.res[r.Key];
                hp += res.hp * r.Value;
                ap += res.ap * r.Value;

                if (res.modi.ContainsKey(C.BuildRes))
                {
                    buildTime += ConvertHelper.Proc(res.modi[C.BuildRes]) * r.Value;
                    buildCount += r.Value;
                }
            }

            if (buildCount > 0)
            {
                buildTime = buildTime/buildCount*ele.buildTime;
            }
            else
            {
                buildTime = ele.buildTime;
            }

            var data = new CalculatedData();
            data.hp = (int) Math.Round(hp);
            data.ap = (int) Math.Round(ap);
            data.buildTime = (int) Math.Round(buildTime);
            
            return data;
        }
    }
}