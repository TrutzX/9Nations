using System.Collections.Generic;
using Game;
using reqs;
using UnityEngine;

namespace DataTypes
{
    public partial class Unit
    {

        public Sprite GetIcon(int id = 1)
        {
            Sprite[] s = Resources.LoadAll<Sprite>("Units/" + file);

            if (s==null || s.Length != 12)
            {
                Debug.LogWarning($"Sprite Units/{file} is wrong formated.");
                return Resources.Load<Sprite>("Units/" + file);
            }
            return s[id];
        }
        
        public Dictionary<string, string> GetActions()
        {
            return ReqHelper.GetReq(action1, action2, action3,
                action4, action5);
        }
        
        public Dictionary<string,string> GenBuildReq()
        {
            return ReqHelper.GetReq("nation:"+reqbuildnation,"townLevel:"+reqbuildtownlevel,reqbuild1,reqbuild2,reqbuild3);
        }
        
        public Dictionary<string,int> GenCost()
        {
            return BuildingHelper.GetCost(cost1,cost2,cost3,cost4,cost5);
        }
        
        
        public Dictionary<string,int> GenProduce()
        {
            return BuildingHelper.GetCost(produce1,produce2,produce3);
        }
        
        public Dictionary<string,string> GenProduceReq()
        {
            return ReqHelper.GetReq(reqproduce1,reqproduce2,reqproduce3);
        }

        public void ShowInfo(PanelBuilder panel)
        {
            panel.AddImageLabel(name,GetIcon());
            panel.AddRess("Cost for construction",GenCost());
            panel.AddReq("Requirement for construction",GenBuildReq());
            panel.AddRess("Production",GenProduce());
            panel.AddReq("Requirement for production",GenProduceReq());
        }

        public void ShowInfo(PanelBuilder panel, GameObject onMap, int x, int y)
        {
            panel.AddImageLabel(name,GetIcon());
            panel.AddRess("Cost for construction",GenCost());
            panel.AddReq("Requirement for construction",GenBuildReq(), onMap, x, y);
            panel.AddRess("Production",GenProduce());
            panel.AddReq("Requirement for production",GenProduceReq());
        }
    }
}