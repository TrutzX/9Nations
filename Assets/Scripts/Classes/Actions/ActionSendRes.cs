using Buildings;
using Classes.Actions.Addons;
using Game;
using Libraries;
using Libraries.FActions;
using Libraries.FActions.General;
using MapElements;
using Players;
using Tools;
using Towns;
using UI;
using Units;

namespace Classes.Actions
{
    public class ActionSendRes : BasePerformAction
    {
        public ActionSendRes() : base("sendRes"){}

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            SelectDest(player, holder, info.Town());
        }

        protected override void Perform(ActionEvent evt, Player player, ActionHolder holder)
        {
            var towns = S.Towns().GetByPlayer(player.id);

            if (towns.Count < 2)
            {
                OnMapUI.Get().ShowPanelMessageError("No town for trading.");
                return;
            }

            WindowPanelBuilder wpb = WindowPanelBuilder.Create("Where do you want to trade?");
            foreach (var town in towns)
            {
                wpb.panel.AddImageTextButton(town.name, town.GetIcon(),
                    () => { SelectDest(player, holder, town); wpb.Close(); });
            }
            
            wpb.AddClose();
            wpb.Finish();
        }

        private void SelectDest(Player player, ActionHolder holder, Town t)
        {
            Town dest = null;
            var towns = S.Towns().GetByPlayer(player.id);

            if (towns.Count == 2)
            {
                dest = t == towns[0] ? towns[1] : towns[0];
                new ActionSendResWindow(t, dest, holder.data["trade"]);
                return;
            }

            //ask user
            WindowPanelBuilder wpb = WindowPanelBuilder.Create(S.T("sendResTo", L.b.actions["sendRes"].Name()));
            foreach (var town in towns)
            {
                if (town != t)
                    wpb.panel.AddImageTextButton(town.name, town.GetIcon(),
                        () =>
                        {
                            new ActionSendResWindow(t, town, holder.data["trade"]);
                            wpb.Close();
                        });
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