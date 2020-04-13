using System;
using System.Collections.Generic;
using Buildings;

using Game;
using Libraries.FActions;
using reqs;
using Tools;
using UI;

namespace Classes.Actions.Addons
{
    public static class ActionHelper
    {

        public static void WaitRound(ActionHolders holder, ActionHolder action, MapElementInfo info, NVector pos)
        {
            FDataAction da = action.DataAction();
            int round = (int) Math.Ceiling((1f * da.cost - info.data.ap) / info.data.apMax);

            WindowPanelBuilder wpb = WindowPanelBuilder.Create($"Wait for {da.name}?");
            wpb.panel.AddLabel(da.Desc);
            wpb.panel.AddImageLabel($"Action {da.name} need {da.cost - info.data.ap} AP more. You can wait {round} rounds.",
                "round");
            wpb.panel.AddButton($"Wait {round} rounds", () =>
            {
                info.SetWaitingAction(holder.actions.IndexOf(action), pos);
                OnMapUI.Get().UpdatePanel(info.Pos());
                wpb.Close();
            });
            wpb.AddClose();
            wpb.Finish();
        }
    }
}