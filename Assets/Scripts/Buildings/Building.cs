using System.Collections.Generic;
using Game;
using reqs;
using UI;
using UnityEngine;

namespace DataTypes
{
    public partial class Building
    {

        public Sprite GetIcon()
        {
            return SpriteHelper.Load("Building/" + file);
        }

        
        public Dictionary<string,string> GenBuildReq()
        {
            return ReqHelper.GetReq("nation:"+reqbuildnation,"townLevel:"+reqbuildtownlevel,reqbuild1,reqbuild2,reqbuild3);
        }

        public Dictionary<string, string> GetActions()
        {
            return ReqHelper.GetReq(action1, action2, action3,
                action4, action5);
        }
        
        public Dictionary<string,int> GenCost()
        {
            return BuildingHelper.GetCost(cost1,cost2,cost3,cost4,cost5);
        }
        
        public Dictionary<string,int> GenProduce()
        {
            return BuildingHelper.GetCost(produce1,produce2,produce3,produce4,produce5);
        }
        
        public Dictionary<string,int> GenProduceOnce()
        {
            return BuildingHelper.GetCost(produceonce1,produceonce2,produceonce3,produceonce4,produceonce5,produceonce6);
        }
        
        public Dictionary<string,string> GenProduceReq()
        {
            return ReqHelper.GetReq(reqproduce1,reqproduce2,reqproduce3,reqproduce4,reqproduce5);
        }
        
        public void ShowInfo(PanelBuilder panel)
        {
            panel.AddImageLabel(name,GetIcon());
            panel.AddLabel($"Build time: {buildtime}");
            panel.AddRess("Cost for construction",GenCost());
            panel.AddReq("Requirement for construction",GenBuildReq());
            panel.AddRess("Production",GenProduce());
            panel.AddReq("Requirement for production",GenProduceReq());
            panel.AddRess("Production once",GenProduceOnce());
        }

        public void ShowInfo(PanelBuilder panel, GameObject onMap, int x, int y)
        {
            panel.AddImageLabel(name,GetIcon());
            panel.AddLabel($"Build time: {buildtime}");
            panel.AddRess("Cost for construction",GenCost());
            panel.AddReq("Requirement for construction",GenBuildReq(), onMap, x, y);
            panel.AddRess("Production",GenProduce());
            panel.AddReq("Requirement for production",GenProduceReq(), onMap, x, y);
            panel.AddRess("Production once",GenProduceOnce());
        }
        
    }
}