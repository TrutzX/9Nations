using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    public abstract class BaseRepeatAction : BasePerformAction
    {
        protected BaseRepeatAction(string id) : base(id){}

        protected void SetRepeat(MapElementInfo info, NVector pos, ActionHolder holder)
        {
            info.SetRepeatAction(new ActionWaiting(holder, info.data.action, pos));
            OnMapUI.Get().UpdatePanel(info.Pos());
        }

        protected abstract void PerformRepeat(Player player, MapElementInfo info, NVector pos,
            ActionHolder holder);
        
        protected void Repeat(Player player, MapElementInfo info, NVector pos, ActionHolder holder)
        {
            UnitInfo unit = ((UnitInfo) info);

            if (!FindPos(player, unit, pos, holder)) return;

            if (holder.data.ContainsKey("pos"))
                Debug.Log("Move to "+holder.data["pos"]);
            
            MoveToPos(player, unit, holder);
        }

        private bool FindPos(Player player, UnitInfo unit, NVector pos, ActionHolder holder)
        {
            if (holder.data.ContainsKey("pos")) return true;
            DataUnit dUnit = unit.dataUnit;
            
                //find next field
                foreach (var p in CircleGenerator.Gen(pos, unit.data.apMax / 5 * 2))
                {
                    if (AddGoal(unit, holder, p))
                        return true;
                }
                
                //has nothing?
                //search the whole map
                for (int x = 0; x < GameMgmt.Get().data.map.width; x++)
                {
                    for (int y = 0; y < GameMgmt.Get().data.map.height; y++)
                    {
                        if (AddGoal(unit, holder, new NVector(x, y, pos.level)))
                            return true;
                    }
                }

            //has a field?
            unit.SetLastInfo(S.T("actionRepeatErrorDestination",holder.DataAction().Name()));
            unit.SetWaitingAction(null);
            return false;
        }

        private void MoveToPos(Player player, UnitInfo unit, ActionHolder holder)
        {
            DataUnit dUnit = unit.dataUnit;
            NVector dPos = new NVector(holder.data["pos"]);
            int level = unit.Pos().level;
            //go to this field
            List<PPoint> points = S.Map().PathFinding(level).Path(player, dUnit.movement, unit.Pos(), dPos);
            NVector last = null;

            Debug.Log("Go to Goal "+unit.Pos()+" "+dPos);
            
            //find last possible point
            foreach (PPoint pp in points)
            {
                NVector p = new NVector(pp.x, pp.y, level);
                
                //free field?
                string s = unit.Passable(p);
                Debug.Log(p+": "+s);
                if (s != null) break;
                
                //save it
                last = p;
            }

            Debug.Log("Go to Goal "+last);
            
            //move their?
            if (last == null)
            {
                if (holder.data.ContainsKey("posTried"))
                {
                    unit.SetLastInfo(S.T("actionRepeatErrorMove",holder.DataAction().Name()));
                    unit.SetRepeatAction(null);
                }
                else
                {
                    unit.SetLastInfo(S.T("actionRepeatErrorPath", holder.DataAction().Name()));
                    if (unit.data.ap == unit.data.apMax)
                        holder.data["posTried"] = "yes";
                }
                
                return;
            } 
            
            //move their
            unit.MoveTo(last, true);
            holder.data.Remove("posTried");

            if (dPos.Equals(last))
            {
                //perform it
                holder.data.Remove("pos");
                PerformRepeat(player, unit, dPos, holder);
            }

            //has enough ap for a next exploration?
            //todo dynamic
            if (dUnit.ap >= 10)
            {
                //Explore(player, unit, pos, holder);
            }
        }

        protected virtual bool AddGoal(UnitInfo unit, ActionHolder holder, NVector pos)
        {
            //found it and movable?
            int cost = GameMgmt.Get().newMap.PathFinding(unit.Pos().level).Cost(unit.Player(), unit.dataUnit.movement, unit.Pos(), pos);
            
            if (cost > 0)
            {
                Debug.Log("Set Goal "+cost+" "+unit.Pos()+" "+pos);
                holder.data["pos"] = pos.ToString();
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