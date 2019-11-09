using System;
using UI;

namespace Players.Infos
{
    [Serializable]
    public class Info
    {
        public string icon;
        public string title;
        public string desc;
        public string action;
        public string sett;
        public int round;
        public bool read;

        public Info(string title, string icon)
        {
            this.icon = icon;
            this.title = title;
        }

        /// <summary>
        /// Add an action to call
        /// </summary>
        /// <param name="action"></param>
        /// <param name="sett"></param>
        /// <returns></returns>
        public Info AddAction(string action, string sett)
        {
            this.action = action;
            this.sett = sett;
            return this;
        }

        public Info Important(string desc)
        {
            this.desc = desc;
            return this;
        }

        public void ShowImportant()
        {
            WindowPanelBuilder w = WindowPanelBuilder.Create(title);
            w.panel.RichText(desc);
            
            if (action==null)
                w.AddClose();
            else
                w.panel.AddButton("Ok",CallAction);
            
            w.Finish();
        }

        public void CallAction()
        {
            NLib.GetAction(action).QuestRun(PlayerMgmt.ActPlayer(), sett);
        }
        
        public void AddToPanel(PanelBuilder panel)
        {
            if (desc != null)
            {
                panel.AddImageTextButton(title, SpriteHelper.Load(icon), ShowImportant);
            } else if (action == null)
            {
                panel.AddImageLabel(title, SpriteHelper.Load(icon));
            }
            else
            {
                panel.AddImageTextButton(title, SpriteHelper.Load(icon), CallAction);
            }
        }

        /// <summary>
        /// For Loading only
        /// </summary>
        public Info()
        {
        }
    }
}