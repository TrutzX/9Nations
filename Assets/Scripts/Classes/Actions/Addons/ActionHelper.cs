using System;
using System.Collections.Generic;
using Buildings;

using Game;
using Libraries;
using Libraries.Animations;
using Libraries.FActions;
using MapElements;
using reqs;
using Tools;
using UI;

namespace Classes.Actions.Addons
{
    public static class ActionHelper
    {

        public static void WaitRound(ActionHolders holder, ActionHolder action, MapElementInfo info, NVector pos, int cost=-1, string sett=null, ActionEvent evt = ActionEvent.Direct)
        {
            FDataAction da = action.DataAction();
            cost = cost == -1 ? action.cost : cost;
            int round = (int) Math.Ceiling((1f * cost - info.data.ap) / info.data.apMax);

            WindowPanelBuilder wpb = WindowPanelBuilder.Create($"Wait for {da.Name()}?");
            wpb.panel.RichText(da.Desc());
            wpb.panel.AddImageLabel($"Action {da.Name()} need {cost - info.data.ap} AP more. You can wait {round} rounds.",
                "round");
            wpb.panel.AddButton($"Wait {round} rounds", () =>
            {
                var aw = new ActionWaiting(action, holder, pos);
                aw.apMax = cost;
                aw.sett = sett;
                aw.evt = evt;
                info.SetWaitingAction(aw);
                L.b.animations.Create("sandClock", pos);

                OnMapUI.Get().UpdatePanel(info.Pos());
                wpb.Close();
            });
            wpb.AddClose();
            wpb.Finish();
        }
    }
}