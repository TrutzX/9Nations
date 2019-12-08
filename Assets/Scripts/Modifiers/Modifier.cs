using System;
using System.Collections.Generic;
using Game;
using Libraries;
using Players;
using Terrains;
using UnityEngine;

namespace Modifiers
{
    [Serializable]
    public class Modifier : BaseData
    {
        public int CalcModi(int standard, Player player)
        {
            int proc = 0;
            int val = 0;
            
            Check(player.Modi, player, ref val, ref proc);
            Check(player.Nation().Modi, player, ref val, ref proc);
            
            standard += (standard * proc) / 100;
            standard += val;
            return Math.Max(0,standard);
        }

        private void Check(Dictionary<string, string> dir, Player player, ref int val, ref int proc)
        {
            if (!dir.ContainsKey(Id))
            {
                return;
            }

            string data = player.Modi[Id];
            BaseModifierCalc calc = Classes(data);
            if (calc.Check(data, player))
            {
                calc.ParseModi(data, ref val, ref proc);
            }
        }

        public BaseModifierCalc Classes(string data)
        {
            if (data.Contains(";"))
            {
                string[] d = data.Split(';');
                return L.b.modifiers.Classes[d[1]];
            }
            return L.b.modifiers.Classes["base"];
        }
        
        public int CalcModi(int standard, Player player, Vector3Int pos)
        {
            int proc = 0;
            int val = 0;

            Check(player.Modi, player, pos, ref val, ref proc);
            Check(player.Nation().Modi, player, pos, ref val, ref proc);
            Check(GameMgmt.Get().map.Terrain(pos).Modi, player, pos, ref val, ref proc);
            if (L.b.improvements.Has(pos))
                Check(L.b.improvements.At(pos).Modi, player, pos, ref val, ref proc);
            
            //calc
            standard += (standard * proc) / 100;
            standard += val;
            return Math.Max(0,standard);
        }
        
        /// <summary>
        /// If standard is null, ignore the modis
        /// </summary>
        /// <param name="standard"></param>
        /// <param name="player"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public int CalcModiNotNull(int standard, Player player, Vector3Int pos)
        {
            if (standard == 0)
            {
                return 0;
            }

            return CalcModi(standard, player, pos);
        }

        private void Check(Dictionary<string, string> dir, Player player, Vector3Int pos, ref int val, ref int proc)
        {
            if (!dir.ContainsKey(Id))
            {
                return;
            }

            string data = dir[Id];
            BaseModifierCalc calc = Classes(data);
            if (calc.Check(data, player, pos))
            {
                calc.ParseModi(data, ref val, ref proc);
            }
        }
    }
}
