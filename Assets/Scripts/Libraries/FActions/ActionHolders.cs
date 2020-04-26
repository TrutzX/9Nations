using System;
using System.Collections.Generic;
using System.Linq;
using Buildings;
using Classes;
using Classes.Actions;
using JetBrains.Annotations;
using Libraries.FActions.General;
using Players;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.Assertions;

namespace Libraries.FActions
{
    [Serializable]
    public class ActionHolders
    {

        public List<ActionHolder> actions;

        public ActionHolders()
        {
            actions = new List<ActionHolder>();
        }

        public ActionHolders(ActionHolders old)
        {
            actions = new List<ActionHolder>(old.actions);
        }

        public void Add(ActionHolder holder)
        {
            actions.Add(holder);
        }

        public void Add(string key, string sett)
        {
            Add(LClass.s.GetNewAction(key).Create(sett));
        }

        public List<ActionHolder> Is(ActionEvent type)
        {
            List<ActionHolder> act = new List<ActionHolder>();
            foreach (var holder in actions)
            {
                if (holder.PerformAction().Is(holder, type))
                    act.Add(holder);
            }

            return act;
        }

        public string Performs(ActionEvent evt, Player player, [CanBeNull] MapElementInfo info, NVector pos)
        {
            string mess = null;
            foreach (var action in Is(evt))
            {
                mess = mess + (string.IsNullOrEmpty(mess)?null:", ") + Perform(action, evt, player, info, pos);
            }

            return mess;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="evt"></param>
        /// <param name="player"></param>
        /// <param name="info"></param>
        /// <param name="pos"></param>
        /// <returns>null or error message</returns>
        public string Perform(ActionHolder action, ActionEvent evt, Player player, [CanBeNull] MapElementInfo info, NVector pos)
        {
            var erg = action.Perform(evt, player, info, pos);
            //remove?
            if (erg.remove)
            {
                actions.Remove(action);
            }

            return erg.mess;
        }

        public string Perform(ActionHolder action, ActionEvent evt, Player player)
        {
            var erg = action.Perform(evt, player);
            //remove?
            if (erg.remove)
            {
                actions.Remove(action);
            }

            return erg.mess;
        }

        public string Performs(ActionEvent evt, Player player)
        {
            string mess = null;
            foreach (var action in Is(evt))
            {
                mess = mess + (string.IsNullOrEmpty(mess)?null:", ") + Perform(action, evt, player);
            }

            return mess;
        }

        public void BuildPanel(PanelBuilder panel, String title, ActionDisplaySettings sett)
        {
            if (actions.Count == 0)
            {
                return;
            }
            panel.AddHeaderLabel(title);
            sett.compact = true;
            
            //display all actions
            foreach (var act in actions)
            {
                sett.holder = act;
                act.PerformAction().BuildPanel(sett);
            }
        }
        
        public bool Contains(string id)
        {
            return actions.Count(a => a.id == id) >= 1;
        }

        public ActionHolder Get(string id)
        {
            return actions.First(a => a.id == id);
        }

        public override string ToString()
        {
            return base.ToString()+String.Join(", ",actions);
        }
    }
}