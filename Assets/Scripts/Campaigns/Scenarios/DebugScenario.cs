using Game;
using Libraries;
using Maps;
using Players;
using Units;
using UnityEngine;

namespace Campaigns.Scenarios
{
    public class DebugScenario : IScenarioRun
    {
        public void Run()
        {
            int pid = PlayerMgmt.Get().CreatePlayer("userx", "north");
            /*
            
            UnitMgmt.Get().Create(pid,"nsoldier", GameMgmt.Get().map.GetStartPos("north"));
            UnitMgmt.Get().Create(pid,"nworker", GameMgmt.Get().map.GetStartPos("north"));
            int tid = TownMgmt.Get().Create(NGenTown.GetTownName("north"), pid, 6, 6);
            TownMgmt.Get(tid).level = 4;
            TownMgmt.Get(tid).AddRes("stone",60);
            TownMgmt.Get(tid).AddRes("brick",50);
            TownMgmt.Get(tid).AddRes("plank",50);
            
            BuildingMgmt.Get().Create(tid, "nlibrary",6, 5);
            
            //PlayerMgmt.Get(pid).quests.AddQuest(QuestHelper.Win().AddReq("season","summer"));
            //PlayerMgmt.Get(pid).quests.AddQuest(QuestHelper.Lose().AddReq("daytime","afternoon"));
            
            
            pid = PlayerMgmt.Get().CreatePlayer("user2", "north");
            UnitMgmt.Get().Create(pid,"nking", 7,4);
            
            L.b.improvements.Set("way",new Vector3Int(7,4,0));
            L.b.improvements.Set("way",new Vector3Int(8,4,0));
            L.b.improvements.Set("way",new Vector3Int(9,4,0));
            L.b.improvements.Set("way",new Vector3Int(9,5,0));
            L.b.improvements.Set("way",new Vector3Int(9,3,0));
            
            
            BuildingMgmt.Get().Create(tid, "nwall",8, 8);
            BuildingMgmt.Get().Create(tid, "nwall",9, 9);
            BuildingMgmt.Get().Create(tid, "nwall",9, 10);
            BuildingMgmt.Get().Create(tid, "nwall",10, 10);
            BuildingMgmt.Get().Create(tid, "nwall",11, 10);
            BuildingMgmt.Get().Create(tid, "nwall",12, 10);
            
            L.b.improvements.Set("way",new Vector3Int(6,5,0));
            L.b.improvements.Set("way",new Vector3Int(6,6,0));*/
            
            UnitMgmt.Get().Create(pid,"nking", 13,10);//.data.ap = 50;
        }
    }
}