using System;
using System.Collections.Generic;
using System.Linq;
using Libraries;
using Libraries.Researches;
using Players.Infos;
using Tools;
using Towns;

namespace Players.PlayerResearches
{
    [Serializable]
    public class PlayerResearchMgmt
    {
        public Dictionary<string, bool> finish;

        public List<string> actual;
        public int cost;
        public BaseInfoMgmt info;
        
        [NonSerialized] public Player player;

        public PlayerResearchMgmt()
        {
            finish = new Dictionary<string, bool>();
            info = new BaseInfoMgmt();
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
            info.NextRound();
            if (cost <= 0)
            {
                SetLastInfo("No research at the moment");
                player.AddResTotal("research", -player.GetResTotal("research"), ResType.Produce);
                return;
            }

            cost -= player.GetResTotal("research");
            player.AddResTotal("research", -player.GetResTotal("research"), ResType.Produce);
            
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
            Research r = NRandom<Research>.Rand(av);
            finish.Add(r.id,true);
            SetLastInfo($"Eureka! Finish the research {r.Name()}");

            //research again
            BeginNewResearch(actual);
        }

        public List<Research> AvailableResearch()
        {
            return L.b.researches.Values().Where(r=>!IsFinish(r.id) && r.req.Check(player)).ToList();
        }
        
        public List<Research> AvailableResearch(List<string> eles)
        {
            List<Research> list = new List<Research>();
            foreach (Research r in AvailableResearch())
            {
                List<string> ele =new List<string>(r.elements);
                
                //check the elements
                foreach (var e in eles)
                {
                    if (ele.Contains(e))
                    {
                        ele.Remove(e);
                    }
                    //empty list? or not included?
                    else
                    {
                        break;
                    }
                }
                
                //list finish?
                if (ele.Count == 0)
                {
                    list.Add(r);
                }
            }

            return list;
        }

        public void BeginNewResearch(List<string> ele)
        {
            actual = ele;
            cost = (int) Math.Pow(ele.Count-Convert.ToInt32(ele.Contains(player.elements.elements[0])), 2) + 1;
        }

        public void SetLastInfo(string mess)
        {
            var i = new Info(mess, "research").AddAction("gameButton", "research");
            player.info.Add(i);
            info.Add(i);
        }
    }
}