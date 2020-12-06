using Buildings;
using Classes.Actions.Addons;
using Game;
using Libraries.FActions;
using Libraries.FActions.General;
using MapElements;
using Players;
using Tools;
using Towns;
using UI;
using Units;
using UnityEngine;

namespace Classes.Actions
{
    public class ActionVision : BasePerformAction
    {
        public ActionVision() : base("vision"){}

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            player.fog.Clear(pos, info.CalcVisibleRange(pos,2));
        }

        protected override void Perform(ActionEvent evt, Player player, ActionHolder holder)
        {
            throw new System.NotImplementedException();
        }

        public override ActionHolder Create(string setting)
        {
            ActionHolder conf = base.Create(setting);
            conf.trigger = ActionEvent.Direct;
            return conf;
        }
    }
}