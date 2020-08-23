using System;
using Buildings;
using Classes.Actions.Addons;
using Game;
using Libraries;
using Libraries.FActions;
using Libraries.FActions.General;
using Players;
using Tools;
using Units;

namespace Classes.Actions
{
    public class ActionModi : BasePerformAction
    {
        public ActionModi() : base("modi"){}

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            foreach (var ele in holder.data)
            {
                info.data.modi[ele.Key] = ele.Value;
            }
        }

        public override void BuildPanel(ActionDisplaySettings sett)
        {
            sett.panel.AddModi(sett.holder.data);

            if (!sett.compact)
                base.BuildPanel(sett);
        }

        private void Set(ref int var, string val)
        {
            int v = ConvertHelper.Int(val);
            var = Math.Min(0, var+v);
        }
        
        public override void Remove(ActionArgument arg)
        {
            arg.NeedMapException();
            foreach (var ele in arg.holder.data)
            {
                arg.onMap.data.modi.Remove(ele.Key);
            }
        }

        protected override void Perform(ActionEvent evt, Player player, ActionHolder holder)
        {
            throw new System.NotImplementedException();
        }

        public override ActionHolder Create(string setting)
        {
            ActionHolder conf = base.Create(setting);
            conf.trigger = ActionEvent.Quest;
            
            foreach (var ele in SplitHelper.Separator(setting))
            {
                var s = SplitHelper.Split(ele);
                conf.data[s.key] = s.value;
            }
            
            return conf;
        }
    }
}