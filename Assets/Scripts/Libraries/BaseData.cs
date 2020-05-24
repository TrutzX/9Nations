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
            panel.AddImageLabel(S.Debug()?S.T("debugName",Name(),id):Name(), Sprite());
        }

        public virtual void AddImageLabel(PanelBuilder panel, int count)
        {
            var text = S.T("plural", S.T(id,count), count);
            panel.AddImageLabel(text,Sprite());
        }

        public void AddSubLabel(PanelBuilder panel, int count, string header=null, string display=null)
        {
            if (string.IsNullOrEmpty(display)) display = count.ToString();
            var text = string.IsNullOrEmpty(header) ? display : S.T("plural", S.T(header,count), display);
            panel.AddSubLabel(Name(),text, Sprite());
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