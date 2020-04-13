using Buildings;
using Classes.Actions.Addons;
using Game;
using Libraries.FActions;
using Libraries.FActions.General;
using Players;
using Tools;
using Towns;
using UI;
using Units;

namespace Classes.Actions
{
    public class ActionTrade : BasePerformAction
    {
        public ActionTrade() : base("trade"){}

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            new ActionTradeWindow(info.Town(), holder.data["trade"]);
        }

        protected override void Perform(ActionEvent evt, Player player, ActionHolder holder)
        {
            var towns = S.Towns().GetByPlayer(player.id);

            if (towns.Count == 0)
            {
                OnMapUI.Get().ShowPanelMessageError("No town for trading.");
            }

            if (towns.Count == 1)
            {
                new ActionTradeWindow(towns[0], holder.data["trade"]);
            }

            WindowPanelBuilder wpb = WindowPanelBuilder.Create("Where do you want to trade?");
            foreach (var town in towns)
            {
                wpb.panel.AddImageTextButton(town.name, town.GetIcon(),
                    () => { new ActionTradeWindow(town, holder.data["trade"]); wpb.Close(); });
            }
            
            wpb.AddClose();
            wpb.Finish();
        }

        public override ActionHolder Create(string setting)
        {
            ActionHolder conf = base.Create(setting);
            conf.trigger = ActionEvent.Direct;
            conf.data["trade"] = setting;
            return conf;
        }
    }
}