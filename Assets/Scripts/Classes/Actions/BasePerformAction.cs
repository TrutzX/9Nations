using System;
using Buildings;
using Classes.Actions.Addons;
using Game;
using JetBrains.Annotations;
using Libraries;
using Libraries.FActions;
using Libraries.FActions.General;
using MapElements;
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

        public virtual bool Is(ActionHolder holder, ActionEvent type)
        {
            return holder.trigger == type;
        }
        
        public void PerformCheck(ActionEvent evt, Player player, [CanBeNull] MapElementInfo info, NVector pos, ActionHolder holder)
        {
            Perform(evt, player, info, pos, holder);
        }
        
        protected abstract void Perform(ActionEvent evt, Player player, [CanBeNull] MapElementInfo info, NVector pos,
            ActionHolder holder);

        public virtual void Remove(ActionArgument arg)
        {
            //todo
            throw new NotImplementedException();
        }

        public void PerformCheck(ActionEvent evt, Player player, ActionHolder holder)
        {
            Perform(evt, player, holder);
        }
        
        protected abstract void Perform(ActionEvent evt, Player player, ActionHolder holder);
        
        public virtual void BuildPanel(ActionDisplaySettings sett)
        {
            FDataAction da = L.b.actions[id];
            string h = sett.header ?? (S.Debug() ? $"{da.Name()} ({id})" : da.Name());

            if (sett.compact)
            {
                sett.panel.AddImageLabel(h, da.Icon);
                return;
            }
            
            sett.panel.AddHeaderLabel(h);

            if (S.Debug())
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
                        sett.panel.AddImageLabel($"You can performed it directly for {sett.holder.cost} AP","ap");
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
                    sett.holder.req.BuildPanel(sett.panel);
                else
                    sett.holder.req.BuildPanel(sett.panel, sett.mapElement, sett.pos);
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
            dic.cost = dic.DataAction().cost;
            
            //copy req
            foreach (var data in dic.DataAction().req.reqs)
            {
                dic.req.Add(data[0], data[1]);
            }
            
            return dic;
        }

        protected void CreateAddCount(ActionHolder holder, ActionEvent type, string count, bool wait)
        {
            holder.trigger = type;
            holder.triggerCount = ConvertHelper.Int(count);
            holder.triggerWait = wait;
        }
    }

    [Serializable]
    public enum ActionEvent
    {
        Direct, NextRound, FinishConstruct, Quest, All
    }
}