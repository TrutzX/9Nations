using System;
using Classes;
using Game;
using Libraries;
using Libraries.Campaigns;
using Players;
using Players.Quests;
using Units;

namespace Endless
{
    public class EndlessGameScenario : IRun
    {
        public void Run()
        {
            
            //add player
            int id = 0;
            while (GameMgmt.startConfig.ContainsKey(id + "name"))
            {
                //add player
                int pid = S.Players().CreatePlayer(GameMgmt.startConfig[id+"name"], GameMgmt.startConfig[id+"nation"]);
                Player p = S.Player(pid);
                p.coat = GameMgmt.startConfig[id + "coat"];
                //UnitMgmt.Get().Create(pid,L.b.nations[GameMgmt.StartConfig[id+"nation"]].Leader, GameMgmt.Get().newMap.tools.GetStartPos(GameMgmt.StartConfig[id + "nation"]));
            
                //add quests
                if (Boolean.Parse(GameMgmt.startConfig[id+"winGold"]))
                {
                    p.quests.Add(QuestHelper.Win().AddReq("res",">1000:gold"));
                }
            
                //add quests
                if (Boolean.Parse(GameMgmt.startConfig[id+"loseKing"]))
                {
                    p.quests.Add(QuestHelper.AddNoUnitTown(QuestHelper.Lose()));
                }

                id++;
            }
        }

        public string ID()
        {
            return "endless";
        }
    }
}