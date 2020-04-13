using System;
using UnityEngine;

namespace Tools
{
    public static class SplitHelper
    {
        private static readonly char SplitK=':';
        private static readonly char Sep=';';
        private const char Del = '=';


        /// <summary>
        /// Split with :
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static (string key, string value) Split(string data)
        {
            string[] split = data.Split(SplitK);
            if (split.Length != 2)
            {
                Debug.LogError($"Can not parse {data}");
            }
            return (split[0], split[1]);
        }
        
        /// <summary>
        /// Split with :
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static (string key, int value) SplitInt(string data)
        {
            var v = Split(data);
            return (v.key, ConvertHelper.Int(v.value));
        }

        /// <summary>
        /// Split with =
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static (string key, string value) Delimiter(string data)
        {
            string[] split = data.Split(Del);
            if (split.Length == 1)
            {
                return (split[0], "");
            }
            if (split.Length > 2)
                throw new MissingMemberException($"Not valid formed: {data}");
            return (split[0], split[1]);
        }

        /// <summary>
        /// Split with =
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static (string key, int value) DelimiterInt(string data)
        {
            var v = Delimiter(data);
            return (v.key, ConvertHelper.Int(v.value));
        }

        /// <summary>
        /// Split with ; for sub points
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string[] Separator(string data)
        {
            string[] split = data.Split(Sep);
            return split;
        }

        /// <summary>
        /// Split with ; for sub points
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int[] SeparatorInt(string data)
        {
            return Array.ConvertAll(Separator(data), int.Parse);
        }
    }
}