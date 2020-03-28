using System;
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
        public ReqHolder req;

        public BaseData()
        {
            Sound = "click";
            Icon = "logo";
            req = new ReqHolder();
        }
        
        public void AddImageLabel(PanelBuilder panel)
        {
            panel.AddImageLabel(Data.features.debug.Bool()?$"{name} ({id})":name, Sprite());
        }

        [Obsolete]
        public void AddReq(PanelBuilder panel, string header = "Requirements", Player player = null)
        {
            req.BuildPanel(panel, header, player);
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