using Buildings;
using Game;
using Libraries.FActions;
using Libraries.FActions.General;
using MapElements;
using MapElements.Units;
using ModIO;
using Players;
using Tools;
using Units;
using UnityEngine;

namespace Classes.Actions
{
    public class ActionPresent : ActionChestOpen
    {
        public ActionPresent() : base("present"){}

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            var unit = S.Unit().At(pos);

            if (!CreateBonus(unit.Player(), unit, pos, unit.Town()))
            {
                info.UI().ShowPanelMessageError(S.T("presentNone",info.data.name, unit.data.name));
                return;
            }

            info.data.apMax += 1;
            info.UI().ShowPanelMessage(S.T("presentSuccessful", info.data.name, unit.data.name));
            
            //gift self? add costs
            if (unit.Player() == info.Player())
            {
                holder.cost += Random.Range(1,1+holder.cost/4);
                info.AddNoti(S.T("presentOwn"),DataAction().Icon);
            }
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