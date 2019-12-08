using System;
using Campaigns;
using Game;
using Libraries;
using Players;
using Players.Quests;
using Units;

namespace Endless
{
    public class EndlessGame : IScenarioRun
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
                UnitMgmt.Get().Create(pid,L.b.nations[GameMgmt.StartConfig[id+"nation"]].Leader, GameMgmt.Get().map.GetStartPos(GameMgmt.StartConfig[id + "nation"]));
            
                //add quests
                if (Boolean.Parse(GameMgmt.StartConfig[id+"winGold"]))
                {
                    p.quests.Add(QuestHelper.Win().AddReq("resMin","gold,1000"));
                }
            
                //add quests
                if (Boolean.Parse(GameMgmt.StartConfig[id+"loseKing"]))
                {
                    p.quests.Add(QuestHelper.Lose().AddReq("maxUnitPlayer",L.b.nations[GameMgmt.StartConfig[id+"nation"]].Leader+",0"));
                }

                id++;
            }
        }
    }
}