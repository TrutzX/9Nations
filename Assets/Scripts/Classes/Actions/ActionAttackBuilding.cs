using Buildings;
using Game;
using Libraries.FActions;
using Libraries.FActions.General;
using Players;
using Tools;
using Units;
using UnityEngine;

namespace Classes.Actions
{
    public class ActionAttackBuilding : ActionAttackUnit
    {
        public ActionAttackBuilding() : base("attackBuilding"){}

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            Perform(info, S.Unit().At(pos));
        }
    }
}