using System;
using Classes.Actions;
using Libraries.FActions;
using Tools;

namespace MapElements
{
    [Serializable]
    public class ActionWaiting
    {
        public int ap, apMax, actionPos;
        public NVector pos;
        public string sett;
        public ActionEvent evt;
        public bool endless;
        public bool needPerform;

        public ActionWaiting(ActionHolder action, ActionHolders holder, NVector pos)
        {
            this.pos = pos;
            apMax = action.cost;
            actionPos = holder.actions.IndexOf(action);
            evt = ActionEvent.Direct;
        }
        
    }
}