using System;
using System.Collections.Generic;
using Buildings;
using DataTypes;
using Players;
using UnityEngine;

namespace reqs
{
    public class ReqHelper
    {
        public static bool Check(Dictionary<string, string> reqs, MapElementInfo onMap, int x, int y)
        {
            return Check(PlayerMgmt.ActPlayer(), reqs, onMap, x, y);
        }
        
        /// <summary>
        /// Check if the req allow it
        /// </summary>
        /// <param name="player"></param>
        /// <param name="reqs"></param>
        /// <param name="onMap"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>true=can use it, false=not possible</returns>
        public static bool Check(Player player, Dictionary<string, string> reqs, MapElementInfo onMap, int x, int y)
        {
            foreach (KeyValuePair<string, string> req in reqs)
            {
                //can use it?
                if (!NLib.GetReq(req.Key).Check(player, onMap, req.Value, x, y))
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
                if (!NLib.GetReq(req.Key).Check(player, req.Value))
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
                BaseReq r = NLib.GetReq(req.Key);
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
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>true=can use it, false=not possible</returns>
        public static bool CheckOnlyFinal(Player player, Dictionary<string, string> reqs, MapElementInfo onMap, int x, int y)
        {
            foreach (KeyValuePair<string, string> req in reqs)
            {
                //can use it?
                BaseReq r = NLib.GetReq(req.Key);
                if (!r.Check(player, onMap, req.Value, x, y) && r.Final(player, onMap, req.Value, x, y))
                {
                    return false;
                }
            }

            return true;
        }

        public static string Desc(Dictionary<string, string> reqs, MapElementInfo onMap, int x, int y)
        {
            return Desc(PlayerMgmt.ActPlayer(), reqs, onMap, x, y);
        }

        /// <summary>
        /// Return the description, why the check its not working or null
        /// </summary>
        /// <param name="player"></param>
        /// <param name="reqs"></param>
        /// <param name="onMap"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>the error message or null</returns>
        public static string Desc(Player player, Dictionary<string, string> reqs, MapElementInfo onMap, int x, int y)
        {
            foreach (KeyValuePair<string, string> req in reqs)
            {
                BaseReq br = NLib.GetReq(req.Key);
                
                //can use it?
                if (!br.Check(player, onMap, req.Value, x, y))
                {
                    return br.Desc(player, onMap, req.Value, x, y);
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
                BaseReq br = NLib.GetReq(req.Key);
                
                //can use it?
                if (!br.Check(player, req.Value))
                {
                    return br.Desc(req.Value);
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
    }
}