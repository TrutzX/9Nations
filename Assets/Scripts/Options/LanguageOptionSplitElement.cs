using Game;
using Libraries;
using UI;
using UI.Show;
using UnityEngine;

namespace Options
{
    public class LanguageOptionSplitElement : SplitElement
    {
        public LanguageOptionSplitElement() : base(S.T("language"), "language")
        {
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            panel.AddHeaderLabelT("languageSelect");
            panel.AddLabelT("languageInfo");
            panel.AddImageTextButton(S.T("languageSystem"), "language", (() => UpdateLang("languageSystem")));
            foreach (var lang in LSys.tem.languages.Values())
            {
                panel.AddImageTextButton(lang.Name(), lang.Icon, (() => UpdateLang(lang.id)));
            }
        }

        private void UpdateLang(string lang)
        {
            LSys.tem.options["language"].SetValue(lang);
            window.Reload();
        }
        
        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}