using Buildings;
using Game;
using MapElements;
using Players;
using Tools;

namespace reqs
{
    public abstract class BaseReqMinMaxPlayer : BaseReqMinMax
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