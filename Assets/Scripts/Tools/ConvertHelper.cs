using System;
using UnityEngine;

namespace Tools
{
    public static class ConvertHelper
    {
        public static int Int(string data)
        {
            if (!Int32.TryParse(data, out var erg))
            {
                Debug.LogError($"Can not parse number {data}");
            }

            return erg;
        }

        public static decimal Proc(string s)
        {
            if (s.EndsWith("%"))
            {
                return Int(s.Substring(0, s.Length - 1)) * Decimal.One / 100;
            }
            return Int(s) * Decimal.One / 100;
        }
    }
}