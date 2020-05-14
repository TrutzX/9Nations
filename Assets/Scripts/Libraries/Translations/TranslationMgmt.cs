using System;
using System.Linq;
using Libraries.Rounds;
using UnityEngine;

namespace Libraries.Translations
{
    [Serializable]
    public class TranslationMgmt : BaseMgmt<Translation>
    {
        public string lang;
        
        public TranslationMgmt() : base("translation") { }
        
        protected override void ParseElement(Translation ele, string header, string data)
        {
            ele.Add(header, data);
        }

        public string Translate(string key)
        {
            if (!ContainsKey(key))
            {
                Debug.LogWarning($"No translation for {key} exist");
                return key;
            }
            
            return this[key].Value(lang);
        }

        public string Translate(string key, object p0, object p1)
        {
            string text = Translate(key);
            return string.Format(text, p0, p1);
        }
        
        public void UpdateLang()
        {
            string selected = LSys.tem.options["language"].Value();

            lang = selected == "langSystem" ? Application.systemLanguage.ToString().ToLower() : selected;
            
            //language exist?
            if (!LSys.tem.languages.ContainsKey(lang))
            {
                lang = LSys.tem.languages.Values().First(l => l.standard).id;
            }
        }
    }
}