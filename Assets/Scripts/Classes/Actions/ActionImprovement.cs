using Buildings;
using Game;
using Libraries;
using Libraries.FActions;
using Libraries.FActions.General;
using Players;
using Tools;
using UI;
using Units;

namespace Classes.Actions
{
    public class ActionImprovement : BasePerformAction
    {
        public ActionImprovement() : base("improvement"){}

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            var i = SplitHelper.Split(holder.data["improvement"]);
            
            //set improvement
            L.b.improvements.Set(i.key,pos);
            
            //kill?
            if (i.value == "kill")
            {
                info.Kill();
            }
        }

        protected override void Perform(ActionEvent evt, Player player, ActionHolder holder)
        {
            throw new System.NotImplementedException();
        }

        public override ActionHolder Create(string setting)
        {
            ActionHolder conf = base.Create(setting);
            conf.data["improvement"] = setting;
            conf.trigger = ActionEvent.FinishConstruct;

            return conf;
        }
    }
}