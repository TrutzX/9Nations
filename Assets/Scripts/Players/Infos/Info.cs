using System;
using Classes;
using Classes.Actions;
using Game;
using Libraries;
using Libraries.FActions;
using Libraries.FActions.General;
using Tools;
using UI;
using UnityEngine;

namespace Players.Infos
{
    [Serializable]
    public class Info
    {
        public string icon;
        public string overviewIcon;
        public string title;
        public string desc;
        public ActionHolder action;
        public int round;
        public bool read;

        /// <summary>
        /// For Loading only
        /// </summary>
        public Info()
        {
        }

        public Info(string title, string icon, string overviewIcon=null)
        {
            this.icon = icon;
            this.title = title;
            this.overviewIcon = overviewIcon??icon;
        }

        /// <summary>
        /// Add an action to call
        /// </summary>
        /// <param name="action"></param>
        /// <param name="sett"></param>
        /// <returns></returns>
        public Info AddAction(string action, string sett)
        {
            this.action = LClass.s.GetNewAction(action).Create(sett);
            return this;
        }

        public Info CameraMove(NVector pos)
        {
            AddAction("cameraMove",$"{pos.level};{pos.x};{pos.y}");
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
            action.Perform(ActionEvent.Direct, S.ActPlayer());
            read = true;
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
    }
}