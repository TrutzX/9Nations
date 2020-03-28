using Buildings;
using Game;
using Libraries.FActions;
using Libraries.FActions.General;
using Players;
using Tools;
using UI;

namespace Classes.Actions
{
    public class ActionDestroy : BasePerformAction
    {
        public ActionDestroy() : base("destroy"){}

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {

            //dying?
            if (holder.trigger == ActionEvent.NextRound)
            {
                info.Kill();
                info.SetLastInfo($"The unit {info.name} has died.");
                return;
            }

            WindowPanelBuilder wpb = WindowPanelBuilder.Create($"Kill {info.name}?");
            wpb.panel.AddButton($"Kill {info.name}", (() =>
            {
                info.Kill();
                OnMapUI.Get().UpdatePanel(info.Pos());
                wpb.Close();
            }));
            wpb.AddClose();
            wpb.Finish();
    }

        protected override void Perform(ActionEvent evt, Player player, ActionHolder holder)
        {
            throw new System.NotImplementedException();
        }

        public override ActionHolder Create(string setting)
        {
            ActionHolder conf = base.Create(setting);

            //add time?
            if (!string.IsNullOrEmpty(setting))
            {
                CreateAddCount(conf, ActionEvent.NextRound, setting, true);
            }
            else
            {
                CreateTrigger(conf, ActionEvent.Direct);
            }

            return conf;
        }
    }
}