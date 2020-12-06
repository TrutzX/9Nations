using System;
using System.IO;
using Game;
using MapElements;
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
        public string desc;
        public string Icon;
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

        public void Id(string nid)
        {
            if (!string.IsNullOrEmpty(id))
                throw new InvalidDataException("can not set id again");

            id = nid;
            desc = id + "Desc";
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
            var text = Plural(count, header, display);
            panel.AddSubLabel(Name(),text, Sprite());
        }

        public string Plural(int count, string header=null, string display=null)
        {
            return string.IsNullOrEmpty(header) ? display : S.T("plural", S.T(header,count), display); ;
        }

        /// <summary>
        /// Return the translated name
        /// </summary>
        /// <returns></returns>
        public virtual string Name()
        {
            return S.T(id);
        }

        /// <summary>
        /// Return the translated name
        /// </summary>
        /// <returns></returns>
        public virtual string Desc()
        {
            return LSys.tem.translations.ContainsKey(desc) ? S.T(desc) : null;
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
            panel.RichText(Desc());
        }

        public virtual void ShowLexicon(PanelBuilder panel, MapElementInfo onMap, NVector pos)
        {
            ShowLexicon(panel);
            req.BuildPanel(panel, onMap, pos);
        }

        public virtual void ShowLexicon(PanelBuilder panel, Player player)
        {
            ShowLexicon(panel);
            req.BuildPanel(panel, player);
        }
    }
}