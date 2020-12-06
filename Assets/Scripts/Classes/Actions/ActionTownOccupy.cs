using Buildings;
using Libraries.FActions;
using Libraries.FActions.General;
using MapElements;
using Players;
using Tools;
using Units;
using UnityEngine;

namespace Classes.Actions
{
    public class ActionTownOccupy : BasePerformAction
    {
        public ActionTownOccupy() : base("townOccupy"){}

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
            conf.data["level"] = string.IsNullOrEmpty(setting) ? "1": setting;

            return conf;
        }
    }
}