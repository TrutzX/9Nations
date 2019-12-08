using System;
using System.Collections.Generic;
using System.Linq;
using DataTypes;
using Players.Infos;
using reqs;
using TMPro;
using Tools;
using UnityEngine;

namespace Players
{
    [Serializable]
    public class ResearchMgmt
    {
        public Dictionary<string, bool> finish;

        public List<string> actual;
        public int cost;
        public string lastInfo;
        
        [NonSerialized] public Player player;

        public ResearchMgmt()
        {
            finish = new Dictionary<string, bool>();
        }

        public bool IsFinish(string id)
        {
            return finish.ContainsKey(id) && finish[id];
        }

        public void Set(string id, bool value)
        {
            finish[id] = value;
        }
        
        public void NextRound()
        {
            lastInfo = null;
            if (cost <= 0)
            {
                SetLastInfo("No research at the moment");
                player.AddRessTotal("research", -player.GetRessTotal("research"));
                return;
            }

            cost -= player.GetRessTotal("research");
            player.AddRessTotal("research", -player.GetRessTotal("research"));
            
            //can research?
            if (cost > 0)
            {
                return;
            }

            //found something?
            List<Research> av = AvailableResearch(actual);
            if (av.Count == 0)
            {
                SetLastInfo("Found nothing in your areas.");
                return;
            }
            
            //finish it
            Research r = NRandom<Research>.Rand(AvailableResearch(actual));
            finish.Add(r.id,true);
            SetLastInfo($"Eureka! Finish the research {r.name}");

            //research again
            BeginNewResearch(actual);
        }

        public List<Research> AvailableResearch()
        {
            List<Research> list = new List<Research>();
            foreach (Research r in Data.research)
            {
                    //finished?
                    if (IsFinish(r.id))
                    {
                        continue;
                    }

                    if (ReqHelper.Check(player, r.GenReq()))
                    {
                        list.Add(r);
                    }
            }

            return list;
        }
        
        public List<Research> AvailableResearch(List<string> eles)
        {
            List<Research> list = new List<Research>();
            foreach (Research r in AvailableResearch())
            {
                //check ele
                if (!eles.Except(r.GetElements()).Any())
                    list.Add(r);
            }

            return list;
        }

        public void BeginNewResearch(List<string> ele)
        {
            actual = ele;
            cost = (int) Math.Pow(ele.Count-Convert.ToInt32(ele.Contains(player.Nation().ResearchElement)), 2) + 1;
        }

        public void SetLastInfo(string mess)
        {
            lastInfo = mess;
            player.info.Add(new Info(mess,"magic:research").AddAction("gameButton","research"));
        }
    }
}