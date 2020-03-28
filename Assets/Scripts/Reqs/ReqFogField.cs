using System;
using System.Linq;
using Buildings;
using DataTypes;
using Endless;
using Game;
using Libraries.Terrains;
using NesScripts.Controls.PathFind;
using Players;
using Tools;
using Towns;
using UnityEngine;

namespace reqs
{
    
    public class ReqFogField : BaseReqOnlyPlayer
    {
        
        public override bool Check(Player player, string sett)
        {
            return player.fog.Visible(new NVector(sett));
        }

        public override bool Final(Player player, string sett)
        {
            return false;
        }

        public override string Desc(Player player, string sett)
        {
            string e = "Explore a field";

            if (player == null)
            {
                return e;
            }
            
            DataTerrain n = GameMgmt.Get().newMap.Terrain(new NVector(sett));
            return $"You need to explore a {n.name}. Status: " + (Check(player,sett) ? "Not found" : "Found");
        }
    }
}