using System;
using Buildings;
using Game;
using Libraries;
using Libraries.Buildings;
using Libraries.PlayerOptions;
using MapElements;
using Players;
using Tools;
using UI;
using UI.Show;
using UnityEngine;

namespace Classes.Actions.Addons
{
    public class BaseSelectElementSplitElement<T> : SplitElement where T : BaseData
    {
        protected string id;
        protected ISplitManager ism;
        protected T ele;
        protected MapElementInfo go;
        protected NVector pos;
        protected Action<MapElementInfo, NVector> perform;
        protected Action<PanelBuilder> inform;
        
        public BaseSelectElementSplitElement(string id, T ele, MapElementInfo go, NVector pos, ISplitManager ism, Action<MapElementInfo, NVector> perform, Action<PanelBuilder> inform = null) : base(ele.Name(), ele.Sprite())
        {
            this.id = id;
            this.ele = ele;
            this.pos = pos;
            this.ism = ism;
            this.go = go;
            this.perform = perform;
            this.inform = inform;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            ele.ShowLexicon(panel, go, pos);
            inform?.Invoke(panel);
        }

        public override void Perform()
        {
            UpdatePref("last"+id);
            ism.Close();
            perform.Invoke(go, pos);
        }

        protected void UpdatePref(string key)
        {
            PlayerOption po = L.b.playerOptions[key];
            
            //remove old?
            string val = string.IsNullOrEmpty(po.Value())?"":po.Value().Replace(ele.id + ";", "");
            
            val = ele.id + ";" + val;
            
            //trim?
            int count = 0;
            foreach (char c in val) 
                if (c == ';') count++;

            if (count > 10)
            {
                val = val.Substring(0, val.LastIndexOf(';'));
            }
            
            po.SetValue(val);
        }
    }
}