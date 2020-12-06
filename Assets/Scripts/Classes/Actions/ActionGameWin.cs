using Buildings;
using Libraries.FActions;
using Libraries.FActions.General;
using Players;
using Tools;
using UnityEngine;

namespace Classes.Actions
{
    public class ActionGameWin : BasePlayerPerformAction
    {
        public ActionGameWin() : base("endGameWin"){}

        protected override void Perform(ActionEvent evt, Player player, ActionHolder holder)
        {
            player.status = "win";
        }

        public override ActionHolder Create(string setting)
        {
            ActionHolder conf = base.Create(setting);
            conf.trigger = ActionEvent.Quest;
            return conf;
        }
    }
}