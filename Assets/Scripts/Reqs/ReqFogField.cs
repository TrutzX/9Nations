using System;
using System.Linq;
using Buildings;
using DataTypes;
using Endless;
using Game;
using NesScripts.Controls.PathFind;
using Players;
using Terrains;
using Towns;
using UnityEngine;

namespace reqs
{
    
    public class ReqFogField : BaseReqOnlyPlayer
    {

        private PPoint Point(string sett)
        {
            string[] s = sett.Split(',');
            return new PPoint(Int32.Parse(s[0]),Int32.Parse(s[1]));
        }
        
        public override bool Check(Player player, string sett)
        {
            PPoint p = Point(sett);
            return player.fog.visible[p.x, p.y];
        }

        public override bool Final(Player player, string sett)
        {
            return false;
        }

        public override string Desc(Player player, MapElementInfo onMap, string sett, int x, int y)
        {
            PPoint p = Point(sett);
            BTerrain n = GameMgmt.Get().map.GetTerrain(p.x, p.y);
            
            return $"You need to explore a {n.Name}. Status: " + (Check(player,sett) ? "Not found" : "Found");
        }

        public override string Desc(string sett)
        {
            return "Explore a field";
        }
    }
}