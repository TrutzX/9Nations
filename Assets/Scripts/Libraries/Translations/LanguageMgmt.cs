using System;
using Libraries.Rounds;

namespace Libraries.Translations
{
    [Serializable]
    public class LanguageMgmt : BaseMgmt<Language>
    {
        public LanguageMgmt() : base("language")
        {
            
        }
        
        protected override void ParseElement(Language ele, string header, string data)
        {
            switch (header)
            {
                case "standard":
                    ele.standard = Bool(data);
                    break;
                default:
                    base.ParseElement(ele, header, data);
                    break;
            }
        }
    }
}