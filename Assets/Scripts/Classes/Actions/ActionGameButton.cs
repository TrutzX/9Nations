using Buildings;
using Libraries;
using Libraries.FActions;
using Libraries.FActions.General;
using Players;
using Tools;
using Units;

namespace Classes.Actions
{
    public class ActionGameButton : BasePlayerPerformAction
    {
        public ActionGameButton() : base("gameButton"){}

        protected override void Perform(ActionEvent evt, Player player, ActionHolder holder)
        {
            L.b.gameButtons[holder.data["gameButton"]].Call(player);
        }

        public override ActionHolder Create(string setting)
        {
            ActionHolder conf = base.Create(setting);
            conf.data["gameButton"] = setting;
            return conf;
        }
    }
}