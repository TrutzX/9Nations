using System.Collections;
using System.Collections.Generic;
using DataTypes;
using Players;
using reqs;
using UI;
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
        /// <param name="gameObject"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="settings"></param>
        public void ButtonUnitRun(GameObject gameObject, int x, int y, string settings)
        {
            UnitInfo unitInfo = gameObject.GetComponent<UnitInfo>();
            NAction action = Data.nAction[id];
            //has ap?
            if (action.cost > unitInfo.data.ap)
            {
                OnMapUI.Get().unitUI
                    .SetPanelMessage(
                        $"Action {action.name} need {action.cost - unitInfo.data.ap} AP more. Please wait a round to refill your AP.");
                return;
            }

            //check pref
            string pref = CheckPref(gameObject, x, y, action);
            if (pref != null)
            {
                OnMapUI.Get().unitUI.SetPanelMessage(pref);
                return;
            }

            unitInfo.data.ap -= action.cost;
            ButtonAction(PlayerMgmt.ActPlayer(), gameObject, x, y, settings);
        }

        public void QuestRun(Player player, string settings)
        {
            ButtonAction(player, settings);
        }

        private string CheckPref(GameObject gameObject, int x, int y, NAction action)
        {
            Debug.Log($"call {action.id}");
            //can use?
            if (!ReqHelper.Check(PlayerMgmt.ActPlayer(), ActionHelper.GenReq(action), gameObject, x, y))
            {
                return ReqHelper.Desc(PlayerMgmt.ActPlayer(), ActionHelper.GenReq(action), gameObject, x, y);
            }

            return null;
        }

        /// <summary>
        /// Perform the action
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="action"></param>
        /// <param name="settings"></param>
        public void ButtonBuildingRun(GameObject gameObject, int x, int y, string settings)
        {
            NAction action = Data.nAction[id];
            //check pref
            string pref = CheckPref(gameObject, x, y, action);
            if (pref != null)
            {
                OnMapUI.Get().buildingUI.SetPanelMessage(pref);
                return;
            }

            ButtonAction(PlayerMgmt.ActPlayer(), gameObject, x, y, settings);
        }

        protected WindowBuilderSplit CreateSplit()
        {
            NAction a = Data.nAction[id];
            return WindowBuilderSplit.Create(a.desc,a.name);
        }
    }
}