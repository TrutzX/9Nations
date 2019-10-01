using System.Collections;
using System.Collections.Generic;
using Buildings;
using DataTypes;
using Players;
using reqs;
using UI;
using Units;
using UnityEngine;

namespace Actions
{
    public abstract class BaseAction : ScriptableObject
    {
        public string id;
        
        /// <summary>
        /// Run on a object
        /// </summary>
        /// <param name="player"></param>
        /// <param name="gameObject"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="settings"></param>
        protected abstract void ButtonAction(Player player, GameObject gameObject, int x, int y, string settings);
        
        /// <summary>
        /// Run for the player
        /// </summary>
        /// <param name="player"></param>
        /// <param name="settings"></param>
        protected abstract void ButtonAction(Player player, string settings);

        /// <summary>
        /// Perform the action for the unit
        /// </summary>
        /// <param name="info"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="settings"></param>
        public void ButtonRun(MapElementInfo info, int x, int y, string settings, MapElementUI ui)
        {
            string mess = ActionRun(PlayerMgmt.ActPlayer(), info, x, y, settings);
            if (mess != null)
            {
                ui.SetPanelMessage(mess);
                NAudio.PlayBuzzer();
            }
        }

        /// <summary>
        /// Perform the action for the unit
        /// </summary>
        /// <param name="info"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="settings"></param>
        public void BackgroundRun(MapElementInfo info, int x, int y, string settings)
        {
            string mess = ActionRun(info.Player(),info, x, y, settings);
            if (mess != null)
            {
                info.data.lastError = mess;
            }
        }

        /// <summary>
        /// Perform the action for the unit
        /// </summary>
        /// <param name="info"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="settings"></param>
        private string ActionRun(Player player, MapElementInfo info, int x, int y, string settings)
        {
            NAction action = Data.nAction[id];
            //has ap?
            if (action.cost > info.data.ap)
            {
                return $"Action {action.name} need {action.cost - info.data.ap} AP more. Please wait a round to refill your AP.";
            }

            //check pref
            Debug.Log($"call {action.id}");
            //can use?
            if (!ReqHelper.Check(player, ActionHelper.GenReq(action), info.gameObject, x, y))
            {
                return ReqHelper.Desc(player, ActionHelper.GenReq(action), info.gameObject, x, y);
            }

            info.data.ap -= action.cost;
            ButtonAction(player, info.gameObject, x, y, settings);
            return null;
        }

        public void QuestRun(Player player, string settings)
        {
            ButtonAction(player, settings);
        }

        protected WindowBuilderSplit CreateSplit()
        {
            NAction a = Data.nAction[id];
            return WindowBuilderSplit.Create(a.desc,a.name);
        }
    }
}