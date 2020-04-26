using Game;
using Libraries;
using Libraries.Campaigns;
using Players;
using Tools;
using Towns;
using Units;
using UnityEngine;

namespace Classes.Scenarios
{
    public class DebugScenario : IRun
    {

        public void Run()
        {
            //L.b.gameOptions["fog"].SetValue("false");
            
            int pid = PlayerMgmt.Get().CreatePlayer("userx", "forger");
            PlayerMgmt.Get(pid).elements.elements.Add("shadow");
            PlayerMgmt.Get(pid).elements.elements.Add("earth");
            //GameMgmt.Get().NextPlayer();

            NVector pos = new NVector(6, 6, 0);
            NVector p1 = new NVector(7, 7, 0);
            
            int tid = S.Towns().Create(LClass.s.NameGenerator("town"), pid, pos);
            //S.Towns().Get(tid).level++;

            S.Towns().Get(tid).AddRes("wood",60, ResType.Gift);
            S.Towns().Get(tid).AddRes("cobblestone",60, ResType.Gift);
            S.Towns().Get(tid).AddRes("stone",60, ResType.Gift);
            //S.Towns().Get(tid).AddRes("brick",50, ResType.Gift);
            //S.Towns().Get(tid).AddRes("plank",50, ResType.Gift);
            S.Towns().Get(tid).AddRes("gold",50, ResType.Gift);
            S.Towns().Get(tid).AddRes("worker",50, ResType.Gift);
            S.Towns().Get(tid).AddRes("sand",50, ResType.Gift);
            S.Towns().Get(tid).AddRes("tool",50, ResType.Gift);

            //S.Building().Create(tid, "library", pos.DiffX(1)).data.construction["buildtime"] = 1;
            S.Building().Create(tid, "stair", pos.DiffX(1));
            
            //UnitMgmt.Get().Create(pid, "light", GameMgmt.Get().newMap.tools.GetStartPos("north"));
            S.Unit().Create(pid, "shadow", pos);
            S.Unit().Create(pid, "sworker", p1).FinishConstruct();
            GameMgmt.Get().data.map.ResGenAdd(p1, "copper", 1-GameMgmt.Get().data.map.ResGen(p1, "copper"));
            S.Unit().Create(pid, "swarrior", pos.DiffX(2)).FinishConstruct();
            
            S.Building().Create(tid, "tent", pos.DiffY(1).DiffX(1)).FinishConstruct();
            S.Building().Create(tid, "sshrine", pos.DiffY(-1).DiffX(1)).FinishConstruct();
            
            
            //GameMgmt.Get().newMap.levels[0].SetTile(new Vector3Int(7,7,0), L.b.terrains["grass"]);
            
            p1 = pos.Diff(3, 3);
            
            //pid = PlayerMgmt.Get().CreatePlayer("usery", "north");
            //PlayerMgmt.Get(pid).elements.elements.Add("light");
            //S.Unit().Create(pid, "lworker", pos.DiffX(3));
            S.Unit().Create(pid, "sexplorer", p1).FinishConstruct();
            S.Building().Create(tid, "mine", p1).FinishConstruct();

            /*UnitMgmt.Get().Create(pid,"nking", new NVector(13,10,GameMgmt.Get().data.map.standard));
            UnitMgmt.Get().Create(pid,"nsoldier", GameMgmt.Get().newMap.tools.GetStartPos("north"));
            UnitMgmt.Get().Create(pid,"nworker", GameMgmt.Get().newMap.tools.GetStartPos("north"));
            int tid = S.Towns().Create(NGenTown.GetTownName("north"), pid, new NVector(6,6,GameMgmt.Get().data.map.standard));
            TownMgmt.Get(tid).level = 4;
            
            
            BuildingMgmt.Get().Create(tid, "nlibrary",new NVector(6,5,GameMgmt.Get().data.map.standard));
            
            //PlayerMgmt.Get(pid).quests.AddQuest(QuestHelper.Win().AddReq("season","summer"));
            //PlayerMgmt.Get(pid).quests.AddQuest(QuestHelper.Lose().AddReq("daytime","afternoon"));

            L.b.improvements.Set("way",new NVector(7,4,0));
            L.b.improvements.Set("way",new NVector(8,4,0));
            L.b.improvements.Set("way",new NVector(9,4,0));
            L.b.improvements.Set("way",new NVector(9,5,0));
            L.b.improvements.Set("way",new NVector(9,3,0));
            
            /*
            BuildingMgmt.Get().Create(tid, "nwall",8, 8);
            BuildingMgmt.Get().Create(tid, "nwall",9, 9);
            BuildingMgmt.Get().Create(tid, "nwall",9, 10);
            BuildingMgmt.Get().Create(tid, "nwall",10, 10);
            BuildingMgmt.Get().Create(tid, "nwall",11, 10);
            BuildingMgmt.Get().Create(tid, "nwall",12, 10);
            */

            //L.b.improvements.Set("way",new NVector(6,5,0));
            //L.b.improvements.Set("way",new NVector(6,6,0));

        }

        public string ID()
        {
            return "debug";
        }
    }
}