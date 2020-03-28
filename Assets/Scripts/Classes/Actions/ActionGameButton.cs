using Buildings;
using GameButtons;
using Libraries;
using Libraries.FActions;
using Libraries.FActions.General;
using Players;
using Tools;

namespace Classes.Actions
{
    public class ActionGameButton : BasePerformAction
    {
        public ActionGameButton() : base("gameButton"){}

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            throw new System.NotImplementedException();
        }

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