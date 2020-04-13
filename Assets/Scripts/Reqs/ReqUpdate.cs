using System;
using System.Linq;
using Buildings;
using Libraries;
using Players;
using Players.Quests;
using Tools;
using Towns;
using UI;
using UnityEngine;

namespace reqs
{
    
    public class ReqUpdate : BaseReqOnlyPlayer
    {

        public override bool Check(Player player, string sett)
        {
            if (!LSys.tem.options["update"].Bool()) return false;
            
            string[] s = SplitHelper.Separator(PlayerPrefs.GetString("update.txt", "false"));
            return !(s[0].Equals("false"));
        }

        public override bool Final(Player player, string sett)
        {
            return true;
        }

        public override string Desc(Player player, string sett)
        {
            return "Only show, if an update available";
        }
    }
}