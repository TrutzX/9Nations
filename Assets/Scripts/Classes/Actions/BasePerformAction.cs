using System;
using Buildings;
using JetBrains.Annotations;
using Libraries;
using Libraries.FActions;
using Libraries.FActions.General;
using Players;
using Tools;
using Units;
using UnityEngine;

namespace Classes.Actions
{
    public abstract class BasePerformAction : ScriptableObject
    {
        public string id;

        protected BasePerformAction(string id)
        {
            this.id = id;
        }
        
        public void PerformCheck(ActionEvent evt, Player player, [CanBeNull] MapElementInfo info, NVector pos, ActionHolder holder)
        {
            Perform(evt, player, info, pos, holder);
        }
        
        protected abstract void Perform(ActionEvent evt, Player player, [CanBeNull] MapElementInfo info, NVector pos,
            ActionHolder holder);

        public void PerformCheck(ActionEvent evt, Player player, ActionHolder holder)
        {
            Perform(evt, player, holder);
        }
        
        protected abstract void Perform(ActionEvent evt, Player player, ActionHolder holder);
        
        public virtual void BuildPanel(ActionDisplaySettings sett)
        {
            bool d = Data.features.debug.Bool();
            FDataAction da = L.b.actions[id];
            string h = sett.header ?? (d ? $"{da.name} ({id})" : da.name);

            if (sett.compact)
            {
                sett.panel.AddImageLabel(h, da.Icon);
                return;
            }
            
            sett.panel.AddHeaderLabel(h);

            if (d)
            {
                sett.panel.AddSubLabel("triggerWait",sett.holder.triggerWait.ToString());
            }

            if (sett.holder.triggerCount > 0)
            {
                string s = sett.holder.triggerWait ? " in" : " ";
                if (sett.holder.trigger == ActionEvent.NextRound)
                {
                    sett.panel.AddImageLabel($"Action will be performed{s} {sett.holder.triggerCount} rounds.", "build");
                } 
                else if (sett.holder.trigger == ActionEvent.Direct)
                {
                    sett.panel.AddLabel($"You can perform this action{s} {sett.holder.triggerCount} times.");
                }
            }
            else
            {
                switch (sett.holder.trigger)
                {
                    case ActionEvent.Direct:
                        sett.panel.AddImageLabel($"You can performed it directly for {da.cost} AP","ap");
                        break;
                    case ActionEvent.FinishConstruct:
                        sett.panel.AddLabel("It will be performed after finish construction.");
                        break;
                    case ActionEvent.NextRound:
                        sett.panel.AddLabel("It will be performed every round.");
                        break;
                    default:
                        sett.panel.AddLabel($"It will be performed {sett.holder.trigger}");
                        break;
                }
            }
            
            //add req?
            if (sett.addReq)
            {
                if (sett.pos == null)
                    sett.holder.req.BuildPanel(sett.panel, "Requirement");
                else
                    sett.holder.req.BuildPanel(sett.panel, "Requirement", sett.mapElement, sett.pos);
            }
                
        }

        public FDataAction DataAction()
        {
            return L.b.actions[id];
        }
        
        public virtual ActionHolder Create(string setting)
        {
            ActionHolder dic = new ActionHolder();
            dic.id = id;
            
            //copy req
            foreach (var data in dic.DataAction().req.reqs)
            {
                dic.req.Add(data[0], data[1]);
            }
            
            return dic;
        }

        protected void CreateAddCount(ActionHolder holder, ActionEvent type, string count, bool wait)
        {
            CreateTrigger(holder, type);
            holder.triggerCount = ConvertHelper.Int(count);
            holder.triggerWait = wait;
        }
        
        protected void CreateTrigger(ActionHolder holder, ActionEvent type)
        {
            holder.trigger = type;
        }
    }

    [Serializable]
    public enum ActionEvent
    {
        Direct, NextRound, FinishConstruct, Quest
    }
}