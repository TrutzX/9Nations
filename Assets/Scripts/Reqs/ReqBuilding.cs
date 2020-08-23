using System;
using Buildings;
using Game;
using Libraries;
using Players;
using Tools;
using Towns;
using Units;
using UnityEngine;
using MapElementInfo = Buildings.MapElementInfo;

namespace reqs
{
    
    public class ReqBuilding : BaseReqMinMax
    {

        protected override int ValueMax(Player player, MapElementInfo onMap, string element, string sett, NVector pos)
        {
            throw new NotImplementedException();
        }

        protected override int ValueMax(Player player, string element, string sett)
        {
            throw new NotImplementedException();
        }
        protected override int ValueAct(Player player, MapElementInfo onMap, string element, string sett, NVector pos)
        {
            Town t;
            if (onMap != null)
            {
                t = onMap.Town();
                if (t == null)
                {
                    return ValueAct(player, element, sett);
                }
            }
            else
            {
                t = S.Towns().NearestTown(player, pos, false);
            }

            return S.Building().GetByTownType(t.id, element, TryParseBool(sett)).Length;
        }

        protected override int ValueAct(Player player, string element, string sett)
        {
            return S.Building().GetByPlayerType(player.id, element, TryParseBool(sett)).Length;
        }

        protected override string Name(string element, string sett)
        {
            return L.b.buildings[element].Name();
        }

        private bool TryParseBool(string sett)
        {
            Boolean.TryParse(sett, out bool erg);
            return erg;
        }
    }
}