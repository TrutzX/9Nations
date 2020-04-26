using System.Collections.Generic;
using Buildings;
using Game;
using Libraries.FActions;
using Libraries.FActions.General;
using Libraries.Units;
using NesScripts.Controls.PathFind;
using Players;
using Tools;
using UI;
using Units;
using UnityEngine;

namespace Classes.Actions
{
    public class ActionExplore : BasePerformAction
    {
        public ActionExplore() : base("explore"){}

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            if (evt == ActionEvent.Direct)
            {
                info.SetRepeatAction(info.data.action.actions.IndexOf(holder), pos);
                OnMapUI.Get().UpdatePanel(info.Pos());
                return;
            }

            Explore(player, info, pos, holder);
        }

        private void Explore(Player player, MapElementInfo info, NVector pos, ActionHolder holder)
        {
            UnitInfo unit = ((UnitInfo) info);

            if (!FindPos(player, unit, pos, holder)) return;

            MoveToPos(player, unit, pos, holder);
        }

        private bool FindPos(Player player, UnitInfo unit, NVector pos, ActionHolder holder)
        {
            if (holder.data.ContainsKey("pos")) return true;
            DataUnit dUnit = unit.dataUnit;
            
            //find next fog field
                foreach (var p in CircleGenerator.Gen(pos, unit.data.ap / 5 * 2))
                {
                    if (AddGoal(player, pos, holder, dUnit, p))
                        break;
                }
                
                if (holder.data.ContainsKey("pos")) return true;
                
                //has nothing?
                //search the whole map
                for (int x = 0; x < GameMgmt.Get().data.map.width; x++)
                {
                    for (int y = 0; y < GameMgmt.Get().data.map.height; y++)
                    {
                        if (AddGoal(player, pos, holder, dUnit, new NVector(x, y, pos.level)))
                            break;
                    }
                }

                if (holder.data.ContainsKey("pos")) return true;

            //has a field?
            unit.SetLastInfo("Can not explore the world, find no valid fog.");
            unit.SetWaitingAction(-1, null);
                return false;
        }

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
                    unit.SetLastInfo($"Can not move, path {p} is blocked.");
                    return;
                }

                //not enough cost?
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
            
            //has enough ap for a next exploration?
            //todo dynamic
            if (dUnit.ap >= 10)
            {
                //Explore(player, unit, pos, holder);
            }
        }

        private bool AddGoal(Player player, NVector pos, ActionHolder holder, DataUnit dUnit, NVector p)
        {
            if (player.fog.Visible(p))
                return false;

            //found it and movable?
            int cost = GameMgmt.Get().newMap.PathFinding(pos.level).Cost(player, dUnit.movement, pos, p);
            Debug.Log(cost+" "+p);
            if (cost > 0)
            {
                holder.data["pos"] = p.ToString();
                return true;
            }

            return false;
        }


        protected override void Perform(ActionEvent evt, Player player, ActionHolder holder)
        {
            throw new System.NotImplementedException();
        }

        public override ActionHolder Create(string setting)
        {
            ActionHolder conf = base.Create(setting);
            conf.trigger = ActionEvent.Direct;
            return conf;
        }
    }
}