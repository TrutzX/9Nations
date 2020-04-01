using Buildings;
using Libraries.FActions;
using Libraries.FActions.General;
using Players;
using Tools;
using Units;

namespace Classes.Actions
{
    public class ActionTownLevel : BasePerformAction
    {
        public ActionTownLevel() : base("townLevel"){}

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            info.Town().Evolve(ConvertHelper.Int(holder.data["level"]));
        }

        protected override void Perform(ActionEvent evt, Player player, ActionHolder holder)
        {
            throw new System.NotImplementedException();
        }

        public override ActionHolder Create(string setting)
        {
            ActionHolder conf = base.Create(setting);
            conf.trigger = ActionEvent.FinishConstruct;

            //add time?
            conf.data["level"] = setting ?? "1";

            return conf;
        }
    }
}