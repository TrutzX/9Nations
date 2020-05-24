using System;
using Buildings;
using Game;
using Libraries;
using Players;
using Tools;
using Towns;
using UnityEngine;

namespace reqs
{
    
    public class ReqUpgradeField : BaseReqOnlyMapElement
    {
        public override bool Check(Player player, MapElementInfo onMap, string sett, NVector pos)
        {
            if (S.Building().Free(pos)) return false;
            
            var bi = S.Building().At(pos).dataBuilding;
            string[] builds = SplitHelper.Separator(sett);
            foreach (var b in builds)
            {
                if (bi.id == b) return true;
            }

            return false;
        }

        public override bool Final(Player player,MapElementInfo onMap, string sett, NVector pos)
        {
            return true;
        }

        public override string Desc(Player player,MapElementInfo onMap, string sett, NVector pos)
        {
            return Desc(player, sett);
        }

        public override string Desc(Player player, string sett)
        {
            var l = SplitHelper.Separator(sett);
            return S.T(LSys.tem.translations.GetPlural("reqUpgradeFieldPlural",l.Length), L.b.buildings.NameList(l));
        }
    }
}