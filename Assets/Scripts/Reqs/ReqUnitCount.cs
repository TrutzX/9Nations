using System;
using Buildings;
using Game;
using Players;
using Tools;

namespace reqs
{
    public class ReqUnitCount : ReqMinMax
    {
        protected override int ValueMax(Player player, MapElementInfo onMap, string element, string sett, NVector pos)
        {
            return ValueMax(player, element, sett);
        }

        protected override int ValueMax(Player player, string element, string sett)
        {
            //TODO
            return 100;
        }

        protected override int ValueAct(Player player, MapElementInfo onMap, string element, string sett, NVector pos)
        {
            return ValueAct(player, element, sett);
        }

        protected override int ValueAct(Player player, string element, string sett)
        {
            return S.Unit().GetByPlayer(player.id).Length;
        }

        protected override string Name(string element, string sett)
        {
            return "unit";
        }
    }
    
}