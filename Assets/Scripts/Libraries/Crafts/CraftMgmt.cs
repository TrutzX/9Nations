using System;
using System.Collections.Generic;
using Classes.Actions;
using Libraries.FActions;
using Libraries.Rounds;
using Tools;
using UnityEngine;

namespace Libraries.Crafts
{
    [Serializable]
    public class CraftMgmt : BaseMgmt<Craft>
    {
        public CraftMgmt() : base("craft") { }

        protected override void ParseElement(Craft ele, string header, string data)
        {
            switch (header)
            {
                case "craft":
                    CreateCraft(ele, data);
                    break;
                default:
                    base.ParseElement(ele, header, data);
                    break;
            }
        }

        private void CreateCraft(Craft ele, string data)
        {
                var d = SplitHelper.SplitInt(data);
                if (d.value < 0)
                {
                    ele.req.Add("res",$">{d.value*-1}:{d.key}");
                }
                ele.res.Add(d.key, d.value);
        }
        public static void RebuildAfter(int i, Dictionary<string, string> data)
        {
            //rebuild it
            while (data.ContainsKey("craft" + i))
            {
                if (!data.ContainsKey("craft" + (i + 1)))
                {
                    data.Remove("craft" + i);
                    break;
                }

                data["craft" + i] = data["craft" + (i + 1)];
                i++;
            }
        }
    }
}