using Audio;
using Game;
using Libraries;
using UnityEngine;

namespace Classes.Options
{
    public class OptionLanguage : ScriptableObject, IRun
    {
        public void Run()
        {
            LSys.tem.translations.UpdateLang();
        }

        public string ID()
        {
            return "language";
        }
    }
}