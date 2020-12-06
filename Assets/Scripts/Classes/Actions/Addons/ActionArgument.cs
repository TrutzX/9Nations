using Buildings;
using Libraries.FActions;
using MapElements;
using Players;
using reqs;
using Tools;

namespace Classes.Actions.Addons
{
    public class ActionArgument : BaseReqArgument
    {
        public ActionEvent evt;
        public ActionHolder holder;

        public ActionArgument(ActionEvent evt, ActionHolder holder, Player player, MapElementInfo onMap = null, NVector pos = null) : base(player, null, onMap, pos)
        {
        }
    }
}