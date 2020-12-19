using System;
using System.Collections;
using System.Linq;
using Game;
using IniParser.Model;
using IniParser.Parser;
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
            GetOrCreate("loadingSys").Add(lang,"Loading {0}");
            GetOrCreate("loadingSysSub").Add(lang,"Reading {0}/{1}");
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
            try
            {
                return string.Format(Translate(GetPlural(key, p0)), p0, p1, p2);
            }
            catch (Exception e)
            {
                throw new FormatException("Can not translate "+key, e);
            }
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
        
        public override IEnumerator ParseIni(string path)
        {
            var l = path.Split('/').Last();
            yield return LSys.tem.Load.ShowMessage(S.T("loadingSys",l));
            
            IniData iniData = new IniDataParser().Parse(Read(path));
            
            //language
            int i = 0;
            
            //add level
                foreach (KeyData key in iniData.Global)
                {
                    if (i % 100 == 0)
                    {
                        yield return LSys.tem.Load.ShowSubMessage(S.T("loadingSysSub",l,iniData.Global.Count));
                    }
                    i++;
                    
                    if (string.IsNullOrEmpty(key.Value))
                    {
                        continue;
                    }
                    if (key.KeyName.StartsWith("//")) continue;
                    
                    //Debug.Log(key.KeyName+"="+key.Value);
                    GetOrCreate(key.KeyName).Add(l, key.Value);
                }
        }
    }
}