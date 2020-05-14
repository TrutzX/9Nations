using System;
using Game;
using Players;
using reqs;
using Tools;
using UI;
using UnityEngine;

namespace Libraries
{
    [Serializable]
    public class BaseData
    {
        public string id;
        public string name;
        public string Icon;
        public string Desc;
        public string Sound;
        public bool Hidden;
        public string category;
        public ReqHolder req;

        public BaseData()
        {
            Sound = "click";
            Icon = "logo";
            req = new ReqHolder();
        }
        
        public void AddImageLabel(PanelBuilder panel)
        {
            var s = string.IsNullOrEmpty(name) ? Name() : name;
            panel.AddImageLabel(S.Debug()?$"{s} ({id})":s, Sprite());
        }

        /// <summary>
        /// Return the translated name
        /// </summary>
        /// <returns></returns>
        public virtual string Name()
        {
            return S.T(id);
        }

        public virtual Sprite Sprite()
        {
            if (string.IsNullOrEmpty(Icon))
            {
                Debug.LogWarning($"Icon for {id} missing");
                return SpriteHelper.Load("logo");
            }
            return SpriteHelper.Load(Icon);
        }

        public virtual void ShowLexicon(PanelBuilder panel)
        {
            AddImageLabel(panel);
            panel.RichText(Desc);
        }
    }
}