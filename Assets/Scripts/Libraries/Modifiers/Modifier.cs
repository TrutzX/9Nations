using System;
using System.Collections.Generic;
using Game;
using Players;
using Tools;
using UI;

namespace Libraries.Modifiers
{
    [Serializable]
    public class Modifier : BaseData
    {
        
        private void Check(Dictionary<string, string> dir, Player player, ref int val, ref int proc)
        {
            if (!dir.ContainsKey(id))
            {
                return;
            }

            string data = dir[id];
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
                return L.b.modifiers.classes[d[1]];
            }
            return L.b.modifiers.classes["base"];
        }
        
        public int CalcModi(int standard, Player player)
        {
            int proc = 0;//%
            int val = 0;
            
            Check(player.Modi, player, ref val, ref proc);
            Check(player.Nation().Modi, player, ref val, ref proc);
            Check(GameMgmt.Get().gameRound.Modi(), player, ref val, ref proc);
            
            standard += (standard * proc) / 100;
            standard += val;
            return Math.Max(0,standard);
        }
        
        public int CalcModi(int standard, Player player, NVector pos)
        {
            return (int) CalcModi(Convert.ToDouble(standard), player, pos);
        }
        
        public double CalcModi(double standard, Player player, NVector pos)
        {
            int proc = 0;
            int val = 0;

            Check(S.Game().data.modi, player, pos, ref val, ref proc);
            Check(player.Modi, player, pos, ref val, ref proc);
            Check(player.Nation().Modi, player, pos, ref val, ref proc);
            Check(GameMgmt.Get().newMap.Terrain(pos).modi, player, pos, ref val, ref proc);
            Check(GameMgmt.Get().gameRound.Modi(), player, ref val, ref proc);
            if (L.b.improvements.Has(pos))
                Check(L.b.improvements.At(pos).modi, player, pos, ref val, ref proc);
            
            //calc
            standard += (standard * proc) / 100;
            standard += val;
            return Math.Max(0,standard);
        }
        
        public (int value, string display) CalcText(int standard, Player player, NVector pos)
        {
            //calc time
            int newS = CalcModi(standard, player, pos);
            return (newS, newS == standard ? newS.ToString() : S.T("debugName",newS,standard));
        }
        
        /// <summary>
        /// If standard is null, ignore the modis
        /// </summary>
        /// <param name="standard"></param>
        /// <param name="player"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public int CalcModiNotNull(int standard, Player player, NVector pos)
        {
            if (standard == 0)
            {
                return 0;
            }

            return CalcModi(standard, player, pos);
        }

        private void Check(Dictionary<string, string> dir, Player player, NVector pos, ref int val, ref int proc)
        {
            if (!dir.ContainsKey(id))
            {
                return;
            }

            string data = dir[id];
            BaseModifierCalc calc = Classes(data);
            if (calc.Check(data, player, pos))
            {
                calc.ParseModi(data, ref val, ref proc);
            }
        }
    }
}
