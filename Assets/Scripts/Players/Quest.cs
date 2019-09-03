using System;
using System.Collections.Generic;
using Actions;
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

        public string name;
        public string icon;

        private int status;

        public Quest(string name, string icon)
        {
            this.name = name;
            this.icon = icon;
            
            reqs = new Dictionary<string, string>();
            actions = new Dictionary<string, string>();
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
            if (status == 1)
            {
                return;
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

                status = 1;
            }
        }
        
        public void ShowInfo(PanelBuilder panel)
        {
            panel.AddImageLabel(name,SpriteHelper.LoadIcon(icon));
            panel.AddLabel("Status: "+ (status == 1 ? "Finish":" Not finish"));
            panel.AddReqCheck("Requirement",reqs);
            panel.AddAction("Result",actions);
        }
    }
}