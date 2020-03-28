using Buildings;
using Game;
using Players;
using Tools;

namespace reqs
{
    public abstract class ReqMinMaxPlayer : ReqMinMax
    {
        protected override int ValueMax(Player player, MapElementInfo onMap, string element, string sett, NVector pos)
        {
            return ValueMax(player, element, sett);
        }

        protected override int ValueAct(Player player, MapElementInfo onMap, string element, string sett, NVector pos)
        {
            return ValueAct(player, element, sett);
        }
    }
}