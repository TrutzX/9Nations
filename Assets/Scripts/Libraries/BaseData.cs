using System;
using UI;
using UnityEngine;

namespace Libraries
{
    [Serializable]
    public class BaseData
    {
        public string Id;
        public string Name;
        public string Icon;
        public string Desc;
        public string Sound;
        public bool Hidden;

        public BaseData()
        {
            Sound = "click";
        }

        public virtual Sprite Sprite()
        {
            if (string.IsNullOrEmpty(Icon))
            {
                Debug.LogWarning($"Icon for {Id} missing");
                return SpriteHelper.Load("logo");
            }
            return SpriteHelper.Load(Icon);
        }

        public virtual void ShowDetail(PanelBuilder panel)
        {
            panel.AddImageLabel(Data.features.debug.Bool()?$"{Name} ({Id})":Name, Sprite());
            panel.RichText(Desc);
        }
    }
}