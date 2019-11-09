using System;
using System.Collections.Generic;
using Game;
using reqs;
using UI;
using UnityEngine;

namespace DataTypes
{
    public partial class Research
    {
        public Sprite GetIcon()
        {
            if (icon.Contains(":"))
            {
                return BuildingHelper.GetIcon(icon);
            }
            return UnitHelper.GetIcon(icon);
        }
        
        public Dictionary<string,string> GenReq()
        {
            return ReqHelper.GetReq("nation:"+reqnation,"townLevel:>"+reqtownlevel,req3);
        }

        public Dictionary<string, string> GetActions()
        {
            return ReqHelper.GetReq(action1, action2, action3);
        }

        public List<string> GetElements()
        {
            List<string> ele = new List<string>();
            foreach (string e in new []{research1,research2,research3,research4,research5,research6,research7,research8,research9})
            {
                if (e.Length > 0)
                {
                    ele.Add(e);
                }
            }

            return ele;
        }
    }
}