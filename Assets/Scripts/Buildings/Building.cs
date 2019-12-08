using System.Collections.Generic;
using Buildings;
using Game;
using Libraries;
using Players;
using reqs;
using UI;
using UnityEngine;

namespace DataTypes
{
    public partial class Building
    {

        public Sprite GetIcon()
        {
            return BuildingHelper.GetIcon(file);
        }

        
        public Dictionary<string,string> GenBuildReq()
        {
            return ReqHelper.GetReq("nation:"+reqbuildnation,"townLevel:>"+reqbuildtownlevel,reqbuild1,reqbuild2,reqbuild3);
        }

        public Dictionary<string, string> GetActions()
        {
            return ReqHelper.GetReq(action1, action2, action3,action4, action5);
        }

        public Dictionary<string, string> GetActionsOnce()
        {
            return ReqHelper.GetReq(actiononce1, actiononce2);
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
            panel.AddAction("Actions", GetActions());
            panel.AddAction("Action after construction", GetActionsOnce());
        }

        public void ShowInfo(PanelBuilder panel, MapElementInfo onMap, int x, int y)
        {
            //TODO Vector3Int Z
            //calc time
            int newb = L.b.modifiers["build"].CalcModi(buildtime, PlayerMgmt.ActPlayer(), new Vector3Int(x, y, 0));
            string nb = newb == buildtime ? newb + " rounds" : $"{newb} ({buildtime}) rounds";
            
            panel.AddImageLabel(name,GetIcon());
            panel.AddImageLabel($"Build time: {nb}","Icons/icons:NextRound");
            panel.AddRess("Cost for construction",GenCost());
            panel.AddReq("Requirement for construction",GenBuildReq(), onMap, x, y);
            panel.AddRess("Production",GenProduce());
            panel.AddReq("Requirement for production",GenProduceReq(), onMap, x, y);
            panel.AddRess("Production once",GenProduceOnce());
            panel.AddAction("Actions", GetActions());
            panel.AddAction("Action after construction", GetActionsOnce());
        }
        
    }
}