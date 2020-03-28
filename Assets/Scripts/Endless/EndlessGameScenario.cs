using System;
using Game;
using Libraries;
using Libraries.Campaigns;
using Players;
using Players.Quests;
using Units;

namespace Endless
{
    public class EndlessGameScenario : IScenarioRun
    {
        public void Run()
        {
            
            //add player
            int id = 0;
            while (GameMgmt.StartConfig.ContainsKey(id + "name"))
            {
                //add player
                int pid = PlayerMgmt.Get().CreatePlayer(GameMgmt.StartConfig[id+"name"], GameMgmt.StartConfig[id+"nation"]);
                Player p = PlayerMgmt.Get(pid);
                //UnitMgmt.Get().Create(pid,L.b.nations[GameMgmt.StartConfig[id+"nation"]].Leader, GameMgmt.Get().newMap.tools.GetStartPos(GameMgmt.StartConfig[id + "nation"]));
            
                //add quests
                if (Boolean.Parse(GameMgmt.StartConfig[id+"winGold"]))
                {
                    p.quests.Add(QuestHelper.Win().AddReq("resMin","gold:1000"));
                }
            
                //add quests
                if (Boolean.Parse(GameMgmt.StartConfig[id+"loseKing"]))
                {
                    p.quests.Add(QuestHelper.Lose().AddReq("unitCount","<0").AddReq("round",">1"));
                }

                id++;
            }
        }
    }
}