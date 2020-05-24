using System;
using System.Collections.Generic;
using Buildings;
using Game;
using Players;
using Tools;
using UnityEngine;

namespace reqs
{
    [Obsolete]
    public class ReqHelper
    {
        public static bool Check(Dictionary<string, string> reqs, MapElementInfo onMap, NVector pos)
        {
            return Check(S.ActPlayer(), reqs, onMap, pos);
        }

        /// <summary>
        /// Check if the req allow it
        /// </summary>
        /// <param name="player"></param>
        /// <param name="reqs"></param>
        /// <param name="onMap"></param>
        /// <param name="pos"></param>
        /// <returns>true=can use it, false=not possible</returns>
        public static bool Check(Player player, Dictionary<string, string> reqs, MapElementInfo onMap, NVector pos)
        {
            foreach (KeyValuePair<string, string> req in reqs)
            {
                //can use it?
                if (!OLib.GetReq(req.Key).Check(player, onMap, req.Value, pos))
                {
                    return false;
                }
            }

            return true;
        }
        
        /// <summary>
        /// Check if the req allow it
        /// </summary>
        /// <param name="player"></param>
        /// <param name="reqs"></param>
        /// <returns>true=can use it, false=not possible</returns>
        public static bool Check(Player player, Dictionary<string, string> reqs)
        {
            foreach (KeyValuePair<string, string> req in reqs)
            {
                //can use it?
                if (!OLib.GetReq(req.Key).Check(player, req.Value))
                {
                    return false;
                }
            }

            return true;
        }
        
        /// <summary>
        /// Check if the req allow it
        /// </summary>
        /// <param name="player"></param>
        /// <param name="reqs"></param>
        /// <returns>true=can use it, false=not possible</returns>
        public static bool CheckOnlyFinal(Player player, Dictionary<string, string> reqs)
        {
            foreach (KeyValuePair<string, string> req in reqs)
            {
                //can use it?
                BaseReq r = OLib.GetReq(req.Key);
                if (!r.Check(player, req.Value) && r.Final(player, req.Value))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check if the req allow it
        /// </summary>
        /// <param name="player"></param>
        /// <param name="reqs"></param>
        /// <param name="onMap"></param>
        /// <param name="pos"></param>
        /// <returns>true=can use it, false=not possible</returns>
        public static bool CheckOnlyFinal(Player player, Dictionary<string, string> reqs, MapElementInfo onMap, NVector pos)
        {
            foreach (KeyValuePair<string, string> req in reqs)
            {
                //can use it?
                BaseReq r = OLib.GetReq(req.Key);
                if (!r.Check(player, onMap, req.Value, pos) && r.Final(player, onMap, req.Value, pos))
                {
                    return false;
                }
            }

            return true;
        }

        public static string Desc(Dictionary<string, string> reqs, MapElementInfo onMap, NVector pos)
        {
            return Desc(S.ActPlayer(), reqs, onMap, pos);
        }

        /// <summary>
        /// Return the description, why the check its not working or null
        /// </summary>
        /// <param name="player"></param>
        /// <param name="reqs"></param>
        /// <param name="onMap"></param>
        /// <param name="pos"></param>
        /// <returns>the error message or null</returns>
        public static string Desc(Player player, Dictionary<string, string> reqs, MapElementInfo onMap, NVector pos)
        {
            foreach (KeyValuePair<string, string> req in reqs)
            {
                BaseReq br = OLib.GetReq(req.Key);
                
                //can use it?
                if (!br.Check(player, onMap, req.Value, pos))
                {
                    return br.Desc(player, onMap, req.Value, pos);
                }
            }

            return null;
        }
        

        /// <summary>
        /// Return the description, why the check its not working
        /// </summary>
        /// <param name="player"></param>
        /// <param name="reqs"></param>
        /// <param name="onMap"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>the error message or null</returns>
        public static string Desc(Player player, Dictionary<string, string> reqs)
        {
            foreach (KeyValuePair<string, string> req in reqs)
            {
                BaseReq br = OLib.GetReq(req.Key);
                
                //can use it?
                if (!br.Check(player, req.Value))
                {
                    return br.Desc(player, req.Value);
                }
            }

            return null;
        }

        /// <summary>
        /// Build the reqs
        /// </summary>
        /// <param name="reqs"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetReq(params string[] reqs)
        {
            Dictionary<string, string> r = new Dictionary<string, string>();
            foreach (string req in reqs)
            {
                if (String.IsNullOrEmpty(req))
                {
                    continue;
                }

                if (!req.Contains(":"))
                {
                    r.Add(req,"");
                    continue;
                }
                
                string[] re = req.Split(':');
                if (re.Length != 2 || String.IsNullOrEmpty(re[1]))
                {
                    continue;
                }
                r.Add(re[0],re[1]);
            }

            return r;
        }
        
        public static Dictionary<string, string> BuildNewReq(Dictionary<string, string> o)
        {
            Dictionary<string, string> r = new Dictionary<string, string>();
            int id = 0;
            while (o.ContainsKey("req" + id))
            {
                var re = SplitHelper.Delimiter(o["req" + id]);
                id++;
                if (string.IsNullOrEmpty(re.value))
                {
                    o.Add(re.key,"");
                    continue;
                }
                r.Add(re.key,re.value);
                
            }

            return r;
        }
    }
}