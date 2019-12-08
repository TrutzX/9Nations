using System;
using Game;
using Libraries;
using Players;
using Terrains;
using UnityEngine;

namespace Modifiers
{
    public class TerrainModifierCalc : BaseModifierCalc
    {
        public override string Desc(string data)
        {
            string[] d = data.Split(';');

            return base.Desc(d[0]) + " on " + L.b.terrain[d[2]].Name;
        }

        public override bool Check(string data, Player player, Vector3Int pos)
        {
            string[] d = data.Split(';');
            BTerrain terr = GameMgmt.Get().map.Terrain(pos);
            return terr.Id == d[2];
        }
        
        public override void ParseModi(string data, ref int val, ref int proc)
        {
            string[] d = data.Split(';');
            base.ParseModi(d[0], ref val, ref proc);
        }
    }
}