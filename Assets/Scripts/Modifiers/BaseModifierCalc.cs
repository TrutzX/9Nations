using System;
using Players;
using UnityEngine;

namespace Modifiers
{
    public class BaseModifierCalc
    {
        public string id;

        public virtual string Desc(string data)
        {
            return data.StartsWith("-")?data:"+"+data;
        }

        public virtual bool Check(string data, Player player)
        {
            return true;
        }

        public virtual bool Check(string data, Player player, Vector3Int pos)
        {
            return true;
        }
        
        public virtual void ParseModi(string data, ref int val, ref int proc)
        {
            if (data.EndsWith("%"))
            {
                proc += Int32.Parse(data.Substring(0,data.Length-1));
                return;
            }
            val += Int32.Parse(data);
        }
    }
}