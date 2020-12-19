using Buildings;
using Classes.Actions.Addons;
using Game;
using Libraries.FActions;
using Libraries.FActions.General;
using MapElements;
using Players;
using Tools;
using Units;

namespace Classes.Actions
{
    public class ActionOccupy : BasePerformAction
    {
        public ActionOccupy() : base("occupy"){}

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            //info.data.ap = 0;
            
            if (evt == ActionEvent.Direct)
            {
                //is the townhall or unit?
                if (info.IsBuilding())
                {
                    //transform to the other unit?
                    var unit = S.Unit(info.Pos());
                    var cost = ConvertHelper.Int(holder.data["cost"]);

                    if (unit.data.ap < cost)
                    {
                        //clone action
                        var ua = Create(holder.data["cost"]);
                        CreateAddCount(ua, ActionEvent.Direct, "1", false);
                        ua.cost = cost;
                        ua.data["pos"] = unit.Pos().ToString();
                        unit.data.action.Add(ua);
                    
                        ActionHelper.WaitRound(unit.data.action, ua, unit, unit.Pos());
                        return;
                    }
                
                    info.SetRepeatAction(new ActionWaiting(holder, info.data.action, pos));
                    OnMapUI.Get().UpdatePanel(info.Pos());
                    return;
                }

                //moved?
                var opos = new NVector(holder.data["pos"]);
                if (!opos.Equals(info.Pos()))
                {
                    OnMapUI.Get().unitUI.ShowPanelMessageError(S.T("occupyMoved"));
                    return;
                }
                
                //transfer
                var build = S.Building(info.Pos());
                build.Town().Transfer(info.data.playerId);
                OnMapUI.Get().UpdatePanel(info.Pos());
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
            conf.data["cost"] = setting;
            return conf;
        }
    }
}