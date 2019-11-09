using System;
using Players;
using Units;
using UnityEngine;

namespace reqs
{
    
    public class ReqOldSameField : BaseReqOld
    {
        public override bool Check(Player player, GameObject onMap, string sett, int x, int y)
        {
            string type = sett.Split(',')[0];
            string id = sett.Split(',')[1];

            //check unit
            if (type == "unit")
            {
                UnitInfo unit = UnitMgmt.At(x, y);
                return unit != null && unit.config.id == id;
            }
            
            //check building
            BuildingInfo build = BuildingMgmt.At(x, y);
            return build != null && build.config.id == id;
        }

        public override bool Check(Player player, string sett)
        {
            Debug.LogWarning("Not implemented");
            return false;
        }

        public override bool Final(Player player, GameObject onMap, string sett, int x, int y)
        {
            return true;
        }

        public override string Desc(Player player, GameObject onMap, string sett, int x, int y)
        {
            string type = sett.Split(',')[0];
            string id = sett.Split(',')[1];

            //check unit
            if (type == "unit")
            {
                UnitInfo unit = UnitMgmt.At(x, y);
                return Desc(sett)+" Here is "+(unit == null?"nothing":unit.name);
            }
            
            //check building
            BuildingInfo build = BuildingMgmt.At(x, y);
            return Desc(sett)+" Here is "+(build == null?"nothing":build.name);
        }

        public override string Desc(string sett)
        {
            
            string type = sett.Split(',')[0];
            string id = sett.Split(',')[1];
            
            //check unit
            if (type == "unit")
            {
                return $"Needs the unit {Data.unit[id].name} on this field.";
            }
            
            //check building
            return $"Needs the building {Data.building[id].name} on this field.";
        }
    }
}