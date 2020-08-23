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
    public class ScreenshotScenario : IRun
    {

        public void Run()
        {
            //L.b.gameOptions["fog"].SetValue("false");
            
            //1. player
            int pid = S.Players().CreatePlayer("forger", "forger");
            S.Player(pid).elements.elements.Add("shadow");
            S.Player(pid).elements.elements.Add("earth");
            
            //GameMgmt.Get().NextPlayer();

            NVector pos = new NVector(10, 8, 1);
            int tid = S.Towns().Create(LClass.s.NameGenerator("town"), pid, pos);
            L.b.improvements.Set("way",pos);
            L.b.improvements.Set("way",pos.DiffX(1));
            L.b.improvements.Set("way",pos.DiffX(1).DiffY(1));
            S.Building().Create(tid, "sandG", pos.DiffX(1).DiffY(1)).FinishConstruct();
            L.b.improvements.Set("way",pos.DiffX(-1));
            L.b.improvements.Set("way",pos.DiffX(-2));
            S.Building().Create(tid, "stair", pos.DiffX(-2)).FinishConstruct();
            S.Building().Create(tid, "sfarm", pos.DiffX(1).DiffY(-1)).FinishConstruct();
            S.Building().Create(tid, "rootplantation", pos.DiffY(-1)).FinishConstruct();
            S.Building().Create(tid, "rootplantation", pos.DiffX(1)).FinishConstruct();
            S.Building().Create(tid, "tent", pos.DiffX(-1)).FinishConstruct();
            
            S.Building().Create(tid, "quarry", pos.DiffX(-1).DiffY(1)).FinishConstruct();
            S.Building().Create(tid, "sshrine", pos.DiffX(-1).DiffY(-1)).FinishConstruct();
            
            S.Unit().Create(pid, "sworker", pos.DiffX(1)).FinishConstruct();
            S.Unit().Create(pid, "sexplorer", pos.DiffX(-2)).FinishConstruct();
            S.Unit().Create(pid, "swarrior", pos.DiffY(-2)).FinishConstruct();

            S.Town(tid).AddRes("swordc",3, ResType.Gift);
            S.Town(tid).AddRes("shieldc",3, ResType.Gift);
            S.Town(tid).AddRes("shoec",3, ResType.Gift);
            
            //2. player
            pos = new NVector(17, 9, 1);
            pid = S.Players().CreatePlayer("north", "north");
            S.Player(pid).elements.elements.Add("light");
            tid = S.Towns().Create(LClass.s.NameGenerator("town"), pid, pos);
            L.b.improvements.Set("way",pos.DiffX(-2));
            L.b.improvements.Set("way",pos.DiffX(-1));
            L.b.improvements.Set("way",pos);
            L.b.improvements.Set("way",pos.DiffX(1));
            L.b.improvements.Set("way",pos.DiffX(1).DiffY(-1));
            L.b.improvements.Set("way",pos.DiffX(1).DiffY(-2));
            L.b.improvements.Set("way",pos.DiffX(2).DiffY(-2));
            S.Building().Create(tid, "cobblestoneC", pos.DiffX(2).DiffY(-2)).FinishConstruct();
            L.b.improvements.Set("way",pos.DiffX(3).DiffY(-2));
            L.b.improvements.Set("way",pos.DiffX(4).DiffY(-2));
            L.b.improvements.Set("way",pos.DiffX(4).DiffY(-1));
            S.Building().Create(tid, "logger", pos.DiffX(4).DiffY(-1)).FinishConstruct();
            L.b.improvements.Set("way",pos.DiffX(5).DiffY(-2));
            S.Building().Create(tid, "hunter", pos.DiffX(5).DiffY(-2)).FinishConstruct();
            
            S.Building().Create(tid, "lshrine", pos.DiffY(1)).FinishConstruct();
            S.Building().Create(tid, "barrack", pos.DiffY(-1).DiffX(-1)).FinishConstruct();
            S.Building().Create(tid, "tent", pos.DiffX(2)).FinishConstruct();
            S.Building().Create(tid, "library", pos.DiffX(-1)).FinishConstruct();
            S.Building().Create(tid, "fisher", pos.DiffX(-2).DiffY(1)).FinishConstruct();
            S.Building().Create(tid, "samplingpoint", pos.DiffX(-2)).FinishConstruct();
            
            
            S.Unit().Create(pid, "lworker", pos.DiffX(1)).FinishConstruct();
            S.Unit().Create(pid, "lexplorer", pos.DiffX(4).DiffY(-2)).FinishConstruct();
            S.Unit().Create(pid, "lwarrior", pos.DiffY(-2)).FinishConstruct();
                
            
                
            
                
            
                
            
            //S.Unit().Create(pid, "light", pos);
        }

        public string ID()
        {
            return "screenshot";
        }
    }
}