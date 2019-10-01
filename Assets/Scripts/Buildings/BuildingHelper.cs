using System;
using System.Collections.Generic;
using DataTypes;
using reqs;
using UI;
using UnityEngine;

namespace Game
{
    public class BuildingHelper
    {


        /// <summary>
        /// Build the reqs
        /// </summary>
        /// <param name="reqs"></param>
        /// <returns></returns>
        public static Dictionary<string, int> GetCost(params string[] reqs)
        {
            Dictionary<string, string> r = ReqHelper.GetReq(reqs);
            Dictionary<string, int> c = new Dictionary<string, int>();
            foreach (KeyValuePair<string, string> req in r)
            {
                c.Add(req.Key,Int32.Parse(req.Value));
            }

            return c;
        }

        public static Sprite GetIcon(string file)
        {
            return SpriteHelper.Load("Building/" + file);
        }
        
        public static string[] GetIDs()
        {
            List<string> ids = new List<string>();
            foreach (Building b in Data.building)
            {
                ids.Add(b.id);
            }

            return ids.ToArray();
        }
    }
}