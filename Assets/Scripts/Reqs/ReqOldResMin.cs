using System;
using Buildings;
using Game;
using Players;
using Towns;
using Units;
using UnityEngine;

namespace reqs
{
    
    public class ReqOldResMin : BaseReqOld
    {
        public override bool Check(Player player, GameObject onMap, string sett, int x, int y)
        {
            string res = sett.Split(',')[0];
            int amount = Int32.Parse(sett.Split(',')[1]);
            
            MapElementInfo info = GameHelper.GetMapElement(onMap);
            Town t = info.Town();
            return t != null && t.GetRes(res) >= amount;
        }

        public override bool Check(Player player, string sett)
        {
            string res = sett.Split(',')[0];
            int amount = Int32.Parse(sett.Split(',')[1]);
            
            return player.GetRessTotal(res) >= amount;
        }

        public override bool Final(Player player, GameObject onMap, string sett, int x, int y)
        {
            return false;
        }

        public override string Desc(Player player, GameObject onMap, string sett, int x, int y)
        {
            string res = sett.Split(',')[0];
            
            MapElementInfo info = GameHelper.GetMapElement(onMap);
            Town t = info.Town();

            if (t == null)
            {
                return Desc(sett)+$" You have total {player.GetRessTotal(res)}x.";
            }
            return Desc(sett)+$" You have {t.GetRes(res)}x.";
        }

        public override string Desc(string sett)
        {
            string res = sett.Split(',')[0];
            int amount = Int32.Parse(sett.Split(',')[1]);
            
            return $"Need at least {amount}x {Data.ress[res].name}.";
        }
    }
}