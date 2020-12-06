using Game;
using Libraries;
using Libraries.Animations;
using Libraries.Campaigns;
using Players;
using Players.Quests;
using Tools;
using Towns;
using UI;
using Units;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

namespace Classes.Scenarios
{
    public class DebugScenario : ScriptableObject, IRun
    {

        public void Run()
        {
            L.b.gameOptions["chestGen"].SetValue("false");
            L.b.gameOptions["fog"].SetValue("false");

            int pid = S.Players().CreatePlayer("userx", "forger");
            S.Player(pid).elements.elements.Add("shadow");
            S.Player(pid).quests.Add(QuestHelper.AddNoUnitTown(QuestHelper.Lose()));
            
            NVector pos = new NVector(8, 6, 1);
            S.Unit().Create(pid, "fsoldier", pos).FinishConstruct();

            int pid2 = S.Players().CreatePlayer("usery", "customn");
            S.Player(pid2).elements.elements.Add("light");
            S.Player(pid2).quests.Add(QuestHelper.AddNoUnitTown(QuestHelper.Lose()));
            
            NVector p2 = new NVector(9, 6, 1);
            var u = S.Unit().Create(pid2, "lcurer", p2);
            u.FinishConstruct();

            var tid = S.Towns().Create("ua", pid2, p2.DiffX(1));
            S.Building().Create(tid, "logger", p2).FinishConstruct();

            //var at = L.b.animations.Hp(-12,u.Pos(), u);
            //Debug.Log("ds"+at.transform.position);

            /*S.Player(pid).elements.elements.Add("earth");
            //GameMgmt.Get().NextPlayer();

            NVector pos = new NVector(6, 6, 0);
            S.Unit().Create(pid, "shadow", pos);
            
            //if (1==1) return;
            NVector p1 = new NVector(7, 7, 0);
            
            int tid = S.Towns().Create(LClass.s.NameGenerator("town"), pid, pos);
            //S.Towns().Get(tid).level++;

            S.Town(tid).AddRes("wood",60, ResType.Gift);
            S.Towns().Get(tid).AddRes("cobblestone",60, ResType.Gift);
            S.Towns().Get(tid).AddRes("stone",60, ResType.Gift);
            //S.Towns().Get(tid).AddRes("brick",50, ResType.Gift);
            //S.Towns().Get(tid).AddRes("plank",50, ResType.Gift);
            S.Towns().Get(tid).AddRes("gold",50, ResType.Gift);
            S.Towns().Get(tid).AddRes("worker",50, ResType.Gift);
            S.Towns().Get(tid).AddRes("sand",50, ResType.Gift);
            S.Towns().Get(tid).AddRes("tool",50, ResType.Gift);
            S.Towns().Get(tid).AddRes("telescope",2, ResType.Gift);
            S.Towns().Get(tid).AddRes("swordc",2, ResType.Gift);
            S.Towns().Get(tid).AddRes("shieldi",2, ResType.Gift);
            S.Towns().Get(tid).AddRes("potionap",2, ResType.Gift);
                
            S.Building().Create(tid, "stair", pos.DiffX(1));
            
            //UnitMgmt.Get().Create(pid, "light", GameMgmt.Get().newMap.tools.GetStartPos("north"));
            //S.Unit().Create(pid, "shadow", pos);
            S.Unit().Create(pid, "sworker", p1.DiffX(1)).FinishConstruct();
            GameMgmt.Get().data.map.ResGenAdd(p1, "copper", 1-GameMgmt.Get().data.map.ResGen(p1, "copper"));
            var mage = S.Unit().Create(pid, "emage", pos.DiffY(1).DiffX(1));
            mage.FinishConstruct();
            mage.data.spells.Learn("addAP");
            mage.data.spells.Learn("vision");
            
            S.Building().Create(tid, "emagicschool", pos.DiffY(1).DiffX(1)).FinishConstruct();
            
            S.Building().Create(tid, "tent", pos.DiffY(5).DiffX(1)).FinishConstruct();
            S.Building().Create(tid, "sshrine", pos.DiffY(-1).DiffX(1)).FinishConstruct();
            
            S.Building().Create(tid, "shall2", pos.DiffY(-2).DiffX(1)).FinishConstruct();
            
            S.Building().Create(tid, "mint", pos.DiffY(-3).DiffX(1)).FinishConstruct();
            S.Building().Create(tid, "mine", pos.DiffY(-3).DiffX(2)).FinishConstruct();
            S.Building().Create(tid, "toolshop", pos.DiffY(-3).DiffX(3)).FinishConstruct();
            
            //GameMgmt.Get().newMap.levels[0].SetTile(new Vector3Int(7,7,0), L.b.terrains["grass"]);
            
            p1 = pos.Diff(3, 0);
            S.Building().Create(tid, "earthWall", p1).FinishConstruct();
            S.Building().Create(tid, "earthWall", p1.DiffY(1)).FinishConstruct();
            S.Building().Create(tid, "earthWallGate", p1.DiffY(2));
            S.Building().Create(tid, "earthWall", p1.DiffY(3)).FinishConstruct();
            S.Building().Create(tid, "earthWall", p1.DiffX(-1));//.FinishConstruct();
            
            p1 = p1.Diff(0, 4);
            
            //pid = PlayerMgmt.Get().CreatePlayer("usery", "north");
            //PlayerMgmt.Get(pid).elements.elements.Add("light");
            //S.Unit().Create(pid, "lworker", pos.DiffX(3));
            S.Unit().Create(pid, "sexplorer", p1).FinishConstruct();
            S.Unit().Create(pid, "geologist", p1.DiffX(1)).FinishConstruct();
            S.Building().Create(tid, "mine", p1).FinishConstruct();

            
            pid = S.Players().CreatePlayer("usery", "north");
            
            /*UnitMgmt.Get().Create(pid,"nking", new NVector(13,10,GameMgmt.Get().data.map.standard));
            UnitMgmt.Get().Create(pid,"nsoldier", GameMgmt.Get().newMap.tools.GetStartPos("north"));
            UnitMgmt.Get().Create(pid,"nworker", GameMgmt.Get().newMap.tools.GetStartPos("north"));
            int tid = S.Towns().Create(NGenTown.GetTownName("north"), pid, new NVector(6,6,GameMgmt.Get().data.map.standard));
            TownMgmt.Get(tid).level = 4;
            
            
            PlayerMgmt.Get(pid).elements.elements.Add("light");
            
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