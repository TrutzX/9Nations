using Buildings;
using Libraries.FActions;
using Libraries.FActions.General;
using Players;
using Tools;

namespace Classes.Actions
{
    public abstract class BasePlayerPerformAction : BasePerformAction
    {
        public BasePlayerPerformAction(string id) : base(id){}

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            Perform(evt, player, holder);
        }
    }
}