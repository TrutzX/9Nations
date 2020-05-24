using System;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;

namespace Libraries.Translations
{
    [Serializable]
    public class Translation : BaseData
    {
        private Dictionary<string, string> trans;

        public Translation()
        {
            trans = new Dictionary<string, string>();
        }

        public void Add(string key, string value)
        {
            trans[key] = value;
        }
        
        public string Value(string lang)
        {

            if (trans.ContainsKey(lang))
            {
                return trans[lang];
            }
            
            if (trans.Count > 0)
            {
                Debug.LogWarning($"Can not find translated {id} for {lang}");
                return trans.First().Value;
            }

            Debug.LogWarning($"Can not find any translation {id} for {lang}");
            return id;
        }

        public override void ShowLexicon(PanelBuilder panel)
        {
            base.ShowLexicon(panel);
            foreach (var k in trans)
            {
                panel.AddHeaderLabel(k.Key);
                panel.AddLabel(k.Value);
            }
        }
    }
}