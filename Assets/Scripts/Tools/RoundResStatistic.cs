using System;
using System.Collections.Generic;
using System.Linq;
using Towns;

namespace Tools
{
    [Serializable]
    public class RoundResStatistic
    {
        public Dictionary<ResType,List<Dictionary<string, double>>> res;

        public RoundResStatistic()
        {
            res = new Dictionary<ResType, List<Dictionary<string, double>>>();
            res[ResType.Produce] = new List<Dictionary<string, double>>();
        }
        
        public void NextRound()
        {
            res[ResType.Produce].Add(new Dictionary<string, double>());
        }

        public void AddRess(string r, double value, ResType type)
        {
            if (type != ResType.Produce)
                return;
            
            if (res[ResType.Produce].Last().ContainsKey(r))
            {
                res[ResType.Produce].Last()[r] += value;
            }
            else
            {
                res[ResType.Produce].Last()[r] = value;
            }
        }

        public Dictionary<string, double> GetLast(ResType type)
        {
            return res[ResType.Produce].Last();
        }
    }
}