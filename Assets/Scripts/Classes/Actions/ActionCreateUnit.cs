using Buildings;
using Game;
using Libraries;
using Libraries.Buildings;
using Libraries.FActions;
using Libraries.FActions.General;
using MapElements;
using Players;
using Tools;
using Units;
using UnityEngine;

namespace Classes.Actions
{
    public class ActionCreateUnit : BasePerformAction
    {
        protected ActionCreateUnit(string id) : base(id){}
        public ActionCreateUnit() : base("createUnit"){}

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            int playerId = holder.data.ContainsKey("player") ? ConvertHelper.Int(holder.data["player"]) : player.id;
            S.Unit().Create(playerId, holder.data["type"], pos).FinishConstruct();
        }

        protected override void Perform(ActionEvent evt, Player player, ActionHolder holder)
        {
            throw new System.NotImplementedException();
        }

        protected virtual BaseDataBuildingUnit Get(string type)
        {
            return L.b.units[type];
        }

        public override void BuildPanel(ActionDisplaySettings sett)
        {
            var create = Get(sett.holder.data["type"]);
            var txt = sett.holder.data.ContainsKey("player") ? S.T("createBelong", create.Name(),S.Player(ConvertHelper.Int(sett.holder.data["player"])).name) : S.T("createDetail", create.Name());
            if (sett.compact)
            {
                sett.panel.AddImageLabel(txt,create.Icon);
                
                return;
            }

            sett.header = txt;
            base.BuildPanel(sett);
        }

        public override ActionHolder Create(string setting)
        {
            ActionHolder conf = base.Create(setting);
            conf.trigger = ActionEvent.Direct;

            var split = SplitHelper.Separator(setting);
            conf.data["type"] = split[0];
            if (split.Length >= 2)
                conf.data["player"] = split[1];

            return conf;
        }
    }
}