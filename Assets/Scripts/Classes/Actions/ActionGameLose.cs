using Buildings;
using Libraries.FActions;
using Libraries.FActions.General;
using Players;
using Tools;

namespace Classes.Actions
{
    public class ActionGameLose : BasePlayerPerformAction
    {
        public ActionGameLose() : base("endGameLose"){}

        protected override void Perform(ActionEvent evt, Player player, ActionHolder holder)
        {
            player.status = "lose";
        }

        public override ActionHolder Create(string setting)
        {
            ActionHolder conf = base.Create(setting);
            conf.trigger = ActionEvent.Direct;
            return conf;
        }
    }
}