using Maps;
using Players;
using Units;

namespace Campaigns.Scenarios
{
    public class DebugScenario : IScenarioRun
    {
        public void Run()
        {
            int pid = PlayerMgmt.Get().CreatePlayer("userx", "north");
            UnitMgmt.Get().Create(pid,"nking", 6,4).data.ap = 50;
            UnitMgmt.Get().Create(pid,"nsoldier", MapMgmt.Get().GetStartPos("north"));
            UnitMgmt.Get().Create(pid,"nworker", MapMgmt.Get().GetStartPos("north"));
            int tid = TownMgmt.Get().Create(NGenTown.GetTownName("north"), pid, 6, 6);
            TownMgmt.Get(tid).AddRes("stone",6);
            BuildingMgmt.Get().Create(tid, "nlibrary",6, 5);
            
            //PlayerMgmt.Get(pid).quests.AddQuest(QuestHelper.Win().AddReq("season","summer"));
            //PlayerMgmt.Get(pid).quests.AddQuest(QuestHelper.Lose().AddReq("daytime","afternoon"));
            
            
            pid = PlayerMgmt.Get().CreatePlayer("user2", "north");
            UnitMgmt.Get().Create(pid,"nking", 7,4);
        }
    }
}