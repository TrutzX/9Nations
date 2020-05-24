using System;
using System.Collections.Generic;
using Classes.Actions;
using Libraries;
using Libraries.FActions;
using Libraries.FActions.General;
using Players.Infos;
using reqs;
using UI;
using UnityEngine;

namespace Players.Quests
{
    [Serializable]
    public class Quest : BaseData
    {
        public string name;
        [SerializeField] private ReqHolder reqs;
        [SerializeField] private ActionHolders actions;

        public bool main;
        
        [SerializeField] private int status;

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
            Icon = icon;
            
            reqs = new ReqHolder();
            actions = new ActionHolders();
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
                if (reqs.Check(player, true))
                {
                    status = 1;
                    //inform player
                    Info i = new Info(name, Icon);
                    if (main)
                        i.Important(Desc);
                    player.info.Add(i);
                }
                else
                {
                    return;
                }
            }
            
            //fullfilled?
            //Debug.Log(name+" "+ReqHelper.Check(player, reqs));
            
            if (reqs.Check(player))
            {
                status = 2;
                actions.Performs(ActionEvent.Quest, player);
            }
        }
        
        public void ShowInfo(PanelBuilder panel, Player player = null)
        {
            ShowLexicon(panel);
            panel.AddSubLabel("Status", (IsFinish() ? "Finish":"In Progress"),IsFinish() ?"yes":"round");
            
            reqs.BuildPanel(panel, "Requirement", player);
            actions.BuildPanel(panel, "Actions", new ActionDisplaySettings(panel,null));
        }
    }
}