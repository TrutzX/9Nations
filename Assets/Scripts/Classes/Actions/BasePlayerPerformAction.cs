using Buildings;
using Libraries.FActions;
using Libraries.FActions.General;
using MapElements;
using Players;
using Tools;
using Units;

namespace Classes.Actions
{
    public abstract class BasePlayerPerformAction : BasePerformAction
    {
        protected BasePlayerPerformAction(string id) : base(id){}

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            Perform(evt, player, holder);
        }
    }
}