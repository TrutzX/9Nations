using System;
using Game;
using UI;
using UnityEngine;

namespace DataTypes
{
    public partial class Features
    {
        public void AddOption(PanelBuilder panel)
        {
            panel.AddCheckbox(Bool(), name, s => { SetValue(Convert.ToString(s)); });
        }

        public bool Bool()
        {
            return Convert.ToBoolean(Value());
        }

        public string Value()
        {
            //check scope
            if (scope == "option")
            {
                return PlayerPrefs.GetString(id,standard);
            }
            
            if (GameMgmt.Get().data.features.ContainsKey(id))
            {
                return GameMgmt.Get().data.features[id];
            }

            return standard;
        }

        public bool Same(string val)
        {
            return Value().ToLower() == val;
        }

        public void SetValue(string value)
        {
            //check scope
            if (scope == "option")
            {
                PlayerPrefs.SetString(id,value);
                return;
            }

            GameMgmt.Get().data.features[id] = value;
        }
    }
}