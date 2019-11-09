using System;
using System.Collections.Generic;
using Actions;
using Players.Infos;
using reqs;
using UI;
using UnityEngine;

namespace Players
{
    [Serializable]
    public class Quest
    {
        private Dictionary<string, string> reqs;
        private Dictionary<string, string> actions;

        public string id;
        public string name;
        public string icon;
        public string desc;

        public bool main;
        
        private int status;

        /// <summary>
        /// For loading only
        /// </summary>
        public Quest()
        {
        }
        
        public Quest(string id, string name, string icon)
        {
            this.id = id;
            this.name = name;
            this.icon = icon;
            
            reqs = new Dictionary<string, string>();
            actions = new Dictionary<string, string>();
        }

        public bool IsFinish()
        {
            return status == 2;
        }

        public bool InProgress()
        {
            return status == 1;
        }
        
        public Quest AddReq(string key, string sett)
        {
            reqs.Add(key, sett);
            return this;
        }

        public Quest AddAction(string key, string sett)
        {
            actions.Add(key, sett);
            return this;
        }

        public void NextRound(Player player)
        {
            if (IsFinish())
            {
                return;
            }

            if (!InProgress())
            {
                //can be started?
                if (ReqHelper.CheckOnlyFinal(player, reqs))
                {
                    status = 1;
                    //inform player
                    Info i = new Info(name, icon);
                    if (main)
                        i.Important(desc);
                    player.info.Add(i);
                }
                else
                {
                    return;
                }
            }
            
            //fullfilled?
            //Debug.Log(name+" "+ReqHelper.Check(player, reqs));
            if (ReqHelper.Check(player, reqs))
            {
                foreach (KeyValuePair<string, string> action in actions)
                {
                    BaseAction b = NLib.GetAction(action.Key);
                    b.QuestRun(player, action.Value);
                }

                status = 2;
            }
        }
        
        public void ShowInfo(PanelBuilder panel)
        {
            panel.AddImageLabel(name,SpriteHelper.Load(icon));
            panel.AddLabel("Status: "+ (IsFinish() ? "Finish":"In Progress"));
            if (desc != null)
                panel.RichText(desc);
            panel.AddReqCheck("Requirement",reqs);
            panel.AddAction("Result",actions);
        }
    }
}