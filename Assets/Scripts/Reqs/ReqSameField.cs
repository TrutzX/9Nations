using System;
using Buildings;
using Game;
using Libraries;
using Players;
using Tools;
using Units;
using UnityEngine;

namespace reqs
{
    
    public class ReqSameField : BaseReqOnlyMapElement
    {
        public override bool Check(Player player, Buildings.MapElementInfo onMap, string sett, NVector pos)
        {
            string type = sett.Split(',')[0];
            string id = sett.Split(',')[1];

            //check unit
            if (type == "unit")
            {
                UnitInfo unit = S.Unit().At(pos);
                return unit != null && unit.baseData.id == id;
            }
            
            //check building
            BuildingInfo build = S.Building().At(pos);
            return build != null && build.baseData.id == id;
        }

        public override bool Check(Player player, string sett)
        {
            Debug.LogWarning("Not implemented");
            return false;
        }

        public override bool Final(Player player, Buildings.MapElementInfo onMap, string sett, NVector pos)
        {
            return true;
        }

        public override string Desc(Player player, Buildings.MapElementInfo onMap, string sett, NVector pos)
        {
            string type = sett.Split(',')[0];
            string id = sett.Split(',')[1];

            //check unit
            if (type == "unit")
            {
                UnitInfo unit = S.Unit().At(pos);
                return Desc(player, sett)+" Here is "+(unit == null?"nothing":unit.name);
            }
            
            //check building
            BuildingInfo build = S.Building().At(pos);
            return Desc(player, sett)+" Here is "+(build == null?"nothing":build.name);
        }

        public override string Desc(Player player, string sett)
        {
            
            string type = sett.Split(',')[0];
            string id = sett.Split(',')[1];
            
            //check unit
            if (type == "unit")
            {
                return $"Needs the unit {L.b.units[id].Name()} on this field.";
            }
            
            //check building
            return $"Needs the building {L.b.buildings[id].Name()} on this field.";
        }
    }
}