using System;
using System.Collections.Generic;
using System.Linq;
using Audio;
using Buildings;
using Game;
using Libraries.FActions;
using Libraries.Units;
using NesScripts.Controls.PathFind;
using Players;
using Tools;
using Units;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Classes.Actions
{
    public class ActionMoveTo : BaseActiveAction
    {
        public ActionMoveTo() : base("moveTo") { }

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos, ActionHolder holder)
        {
            if (evt == ActionEvent.Direct)
            {
                base.Perform(evt, player, info, pos, holder);
                return;
            }
                
            //move to point?
            MoveToPos(player, (UnitInfo) info, pos, holder);
        }
        
        public override void PreRun()
        {
        }

        protected override void RemoveAfter()
        {
        }

        public override string PanelMessage()
        {
            return "Select the field, where you want to move.";
        }

        public override void ClickFirst()
        {
            
            //is visible?
            if (!player.fog.Visible(LastClickPos))
            {
                OnMapUI.Get().SetActiveAction(null,false);
                NAudio.PlayCancel();
                return;
            }
            
            
            Debug.Log("Click first on "+LastClickPos);

            UnitInfo unit = (UnitInfo) mapElementInfo;
            string moveTyp = unit.dataUnit.movement;
            var path = GameMgmt.Get().newMap.PathFinding(mapElementInfo.Pos().level);
            List<PPoint> way = path.Path(player, moveTyp, mapElementInfo.Pos(), new NVector(LastClickPos.x, LastClickPos.y, mapElementInfo.Pos().level));
            
            if (way.Count == 0)
            {
                OnMapUI.Get().unitUI.ShowPanelMessage($"Can not move to this field. No path found.");
                NAudio.PlayBuzzer();
                return;
            }
            
            //show way
            Remove();
            int round = 0;
            int cost = 0;
            Color(LastClickPos, 0);

            foreach (var p in way)
            {
                NVector pos = new NVector(p.x, p.y, LastClickPos.level);
                cost += path.CostNode(player, moveTyp, pos);
                if (cost > unit.data.ap + round * unit.data.apMax * round)
                    round++;
                Color(pos, round%4);
            }
            
            OnMapUI.Get().unitUI.ShowPanelMessage($"You want to move to this field for {cost} AP? Click again!");
        }

        public override void ClickSecond()
        {
            action.data["pos"] = LastClickPos.ToString();
            mapElementInfo.SetRepeatAction(mapElementInfo.data.action.actions.IndexOf(action), mapElementInfo.Pos());
            
            //set move to
            Debug.Log("Click second on "+LastClickPos);
        }

        //Todo combine with action explore
        private void MoveToPos(Player player, UnitInfo unit, NVector pos, ActionHolder holder)
        {
            DataUnit dUnit = unit.dataUnit;
            NVector dPos = new NVector(holder.data["pos"]);
            //go to this field
            List<PPoint> points = S.Map().PathFinding(pos.level).Path(player, dUnit.movement, pos, dPos);

            foreach (PPoint pp in points)
            {
                NVector p = new NVector(pp.x, pp.y, pos.level);
                //free field?
                if (!S.Unit().Free(p))
                {
                    unit.SetLastInfo($"Can not explore the world, path {p} is blocked.");
                    return;
                }

                //not enough cost?
                var terr = S.Map().Terrain(pos);
                if (S.Map().PathFinding(pos.level).CostNode(player, dUnit.movement, pos) > dUnit.ap)
                {
                    return;
                }

                //explore terrain
                player.fog.Clear(pos);
                
                //move their
                unit.MoveTo(p, true);
            }

            holder.data.Remove("pos");
            unit.SetRepeatAction(-1, null);
        }
    }
}