using System;
using Campaigns;
using Game;
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
            while (GameMgmt.startConfig.ContainsKey(id + "name"))
            {
                //add player
                int pid = PlayerMgmt.Get().CreatePlayer(GameMgmt.startConfig[id+"name"], GameMgmt.startConfig[id+"nation"]);
                Player p = PlayerMgmt.Get(pid);
                UnitMgmt.Get().Create(pid,Data.nation[GameMgmt.startConfig[id+"nation"]].leader, GameMgmt.Get().map.GetStartPos(GameMgmt.startConfig[id + "nation"]));
            
                //add quests
                if (Boolean.Parse(GameMgmt.startConfig[id+"winGold"]))
                {
                    p.quests.Add(QuestHelper.Win().AddReq("resMin","gold,1000"));
                }
            
                //add quests
                if (Boolean.Parse(GameMgmt.startConfig[id+"loseKing"]))
                {
                    p.quests.Add(QuestHelper.Lose().AddReq("maxUnitPlayer",Data.nation[GameMgmt.startConfig[id+"nation"]].leader+",0"));
                }

                id++;
            }
        }
    }
}