using System;
using System.Collections.Generic;
using Classes;
using Game;
using Tools;
using UI;
using UnityEngine;

namespace Libraries.Options
{
    [Serializable]
    public class Option : BaseData
    {
        public string standard;
        public string type;
        
        
        public void AddOption(PanelBuilder panel)
        {
            if (type == "bool")
            {
                panel.AddCheckbox(Bool(), name, s => { SetValue(Convert.ToString(s)); });
                return;
            }

            if (type.StartsWith("scale"))
            {
                panel.AddHeaderLabel(name);
                var t = SplitHelper.SeparatorInt(SplitHelper.Delimiter(type).value);
                panel.AddSlider(t[0], t[1], PlayerPrefs.GetInt(id,Int32.Parse(Value())), s =>
                {
                    SetValue(s.ToString());
                });
                return;
            }

            if (type == "text")
            {
                panel.AddInput(name, Value(), SetValue);
            }

            if (type.StartsWith("dropdown"))
            {
                var values = SplitHelper.Separator(SplitHelper.Delimiter(type).value);
                List<string> titles = new List<string>();
                foreach (var value in values)
                {
                    titles.Add(S.T(value));
                }
                panel.AddHeaderLabel(name);
                panel.AddDropdown(values,Value(), titles.ToArray(), SetValue);
            }

        }

        public bool Bool()
        {
            return Convert.ToBoolean(Value());
        }

        public virtual bool Exist()
        {
            return PlayerPrefs.HasKey(id);
        }

        public int Int()
        {
            return ConvertHelper.Int(Value());
        }

        public virtual string Value()
        {
            return PlayerPrefs.GetString(id,standard);
        }

        public bool Same(string val)
        {
            return Value().ToLower() == val;
        }

        public virtual void SetValue(string value)
        {
            PlayerPrefs.SetString(id,value);
            PlayerPrefs.Save();
            
            //inform?
            if (LClass.s.optionRuns.ContainsKey(id))
            {
                LClass.s.optionRuns[id].Run();
            }
        }
    }
    
}