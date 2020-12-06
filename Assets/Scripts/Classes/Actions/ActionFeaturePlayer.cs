using Buildings;
using Libraries.FActions;
using Libraries.FActions.General;
using Players;
using Tools;

namespace Classes.Actions
{
    public class ActionFeaturePlayer : BasePlayerPerformAction
    {
        public ActionFeaturePlayer() : base("featurePlayer"){}

        protected override void Perform(ActionEvent evt, Player player, ActionHolder holder)
        {
            var v = SplitHelper.Split(holder.data["feature"]);
            player.SetFeature(v.key, v.value);
        }

        public override ActionHolder Create(string setting)
        {
            ActionHolder conf = base.Create(setting);
            conf.data["feature"] = setting;
            conf.trigger = ActionEvent.FinishConstruct;
            return conf;
        }
    }
}