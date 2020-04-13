using System;
using Game;
using Players;
using UnityEngine;

namespace reqs
{
    
    public class ReqQuestCount : ReqMinMaxPlayer
    {

        protected override int ValueMax(Player player, string element, string sett)
        {
            //TODO
            return 10;
        }

        protected override int ValueAct(Player player, string element, string sett)
        {
            return player.quests.quests.Count;
        }

        protected override string Name(string element, string sett)
        {
            return "quest";
        }
    }
}