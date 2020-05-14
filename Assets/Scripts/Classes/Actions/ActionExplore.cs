using System.Collections.Generic;
using Buildings;
using Game;
using Libraries.FActions;
using Libraries.FActions.General;
using Libraries.Units;
using NesScripts.Controls.PathFind;
using Players;
using Tools;
using UI;
using Units;
using UnityEngine;

namespace Classes.Actions
{
    public class ActionExplore : BaseRepeatAction
    {
        public ActionExplore() : base("explore"){}

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            if (evt == ActionEvent.Direct)
            {
                SetRepeat(info, pos, holder);
                return;
            }

            Repeat(player, info, pos, holder);
        }

        protected override void PerformRepeat(Player player, MapElementInfo info, NVector pos, ActionHolder holder)
        {
        }

        protected override bool AddGoal(UnitInfo unit, ActionHolder holder, NVector pos)
        {
            if (unit.Player().fog.Visible(pos))
                return false;

            return base.AddGoal(unit, holder, pos);
        }
    }
}