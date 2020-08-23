using System;
using System.Linq;
using Libraries.Rounds;
using UnityEngine;

namespace Libraries.Translations
{
    [Serializable]
    public class TranslationMgmt : BaseMgmt<Translation>
    {
        public string lang = "english";

        public TranslationMgmt() : base("translation")
        {
            //add base text
            GetOrCreate("language").Add(lang,id);
            GetOrCreate(id).Add(lang,id);
        }
        
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

        public string GetPlural(string key, object p0)
        {
            //is p0 a number?
            var isNumeric = int.TryParse(p0.ToString(), out int n);
            return isNumeric && n != 1 && ContainsKey(key + "Plural") ? key + "Plural" : key;
        }

        public string Translate(string key, object p0, object p1, object p2)
        {
            return string.Format(Translate(GetPlural(key, p0)), p0, p1, p2);
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