using System;
using Buildings;
using Classes.Actions.Addons;
using Game;
using Libraries.FActions;
using Libraries.FActions.General;
using Players;
using Tools;
using Units;
using UnityEngine;

namespace Classes.Actions
{
    public class ActionStat : BasePerformAction
    {
        public ActionStat() : base("stat"){}

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            foreach (var ele in holder.data)
            {
                Set(info, ele.Key, ConvertHelper.Int(ele.Value));
            }
        }

        private void Set(MapElementInfo info, string key, int value)
        {
            switch (key)
            {
                case "ap": 
                    Set(ref info.data.ap, value); 
                    break;
                case "apMax":
                    Set(ref info.data.apMax, value); 
                    break;
                case "hp":
                    Set(ref info.data.hp, value); 
                    break;
                case "hpMax":
                    Set(ref info.data.hpMax, value); 
                    break;
                case "atk":
                    Set(ref info.data.atk, value); 
                    break;
                case "def":
                    Set(ref info.data.def, value); 
                    break;
                default:
                    throw new MissingMemberException("Stat "+key+" is not supported.");
            }
        }

        private void Set(ref int var, int val)
        {
            var = Math.Max(0, var+val);
        }
        
        public override void Remove(ActionArgument arg)
        {
            arg.NeedMapException();
            foreach (var ele in arg.holder.data)
            {
                Set(arg.onMap, ele.Key, ConvertHelper.Int(ele.Value)*-1);
            }
        }
        
        private string Icon(string key)
        {
            if (key == "apMax")
                key = "ap";
            if (key == "hpMax")
                key = "hp";
            return key;
        }

        public override void BuildPanel(ActionDisplaySettings sett)
        {
            foreach (var ele in sett.holder.data)
            {
                int val = ConvertHelper.Int(ele.Value);
                sett.panel.AddImageLabel(S.T(val>0?"statAdd":"statRemove", val,S.T(ele.Key)), Icon(ele.Key));
            }

            if (!sett.compact)
                base.BuildPanel(sett);
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