using System.Collections.Generic;
using Buildings;
using Game;
using Libraries.FActions;
using Libraries.FActions.General;
using Libraries.Units;
using MapElements;
using MapElements.Units;
using NesScripts.Controls.PathFind;
using Players;
using Tools;
using UI;
using Units;
using UnityEngine;

namespace Classes.Actions
{
    public class ActionExamine : BaseRepeatAction
    {
        public ActionExamine() : base("examine"){}

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            Debug.Log(evt+" "+info.name);
            if (evt == ActionEvent.NextRound)
            {
                Repeat(player, info, pos, holder);
                return;
            }

            PerformRepeat(player, info, pos, holder);
            //build it
            WindowPanelBuilder wpb = WindowPanelBuilder.Create(S.T("examine"));
            //show all res
            S.Map().Terrain(pos).ShowRes(wpb.panel, player, pos);
            wpb.panel.AddButtonT("examineRepeat", () =>
            {
                SetRepeat(info, pos, holder);
                wpb.Close();
            });
            wpb.AddClose();
            wpb.Finish();

        }

        protected override void PerformRepeat(Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            player.overlay.Set("res", pos, 1);
        }
        
        protected override bool AddGoal(UnitInfo unit, ActionHolder holder, NVector pos)
        {
            if (unit.Player().overlay.Get("res", pos)==1)
                return false;
            
            //has res?
            if (GameMgmt.Get().data.map.levels[pos.level].ResGenKey(pos.x, pos.y) == null)
                return false;

            return base.AddGoal(unit, holder, pos);
        }
    }
}