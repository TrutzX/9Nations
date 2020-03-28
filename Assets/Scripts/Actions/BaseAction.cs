using System;
using System.Collections;
using System.Collections.Generic;
using Buildings;
using DataTypes;
using JetBrains.Annotations;
using Players;
using reqs;
using Tools;
using UI;
using Units;
using UnityEngine;
using UnityEngine.Assertions;

namespace Actions
{
    public abstract class BaseAction : ScriptableObject
    {
        public string id;

        /// <summary>
        /// Run on a object
        /// </summary>
        /// <param name="player"></param>
        /// <param name="info"></param>
        /// <param name="pos"></param>
        /// <param name="settings"></param>
        protected abstract void ButtonAction(Player player, [CanBeNull] MapElementInfo info, NVector pos, string settings);
        
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
        /// <param name="pos"></param>
        /// <param name="settings"></param>
        public string ButtonRun(MapElementInfo info, NVector pos, string settings)
        {
            return ActionRun(PlayerMgmt.ActPlayer(), info, pos, settings);
        }

        /// <summary>
        /// Perform the action for the unit
        /// </summary>
        /// <param name="info"></param>
        /// <param name="pos"></param>
        /// <param name="settings"></param>
        public void BackgroundRun(MapElementInfo info, NVector pos, string settings)
        {
            string mess = ActionRun(info.Player(),info, pos, settings);
            if (mess != null)
            {
                info.SetLastInfo(mess);
            }
        }

        /// <summary>
        /// Perform the action for the unit
        /// </summary>
        /// <param name="player"></param>
        /// <param name="info"></param>
        /// <param name="pos"></param>
        /// <param name="settings"></param>
        private string ActionRun(Player player, [CanBeNull] MapElementInfo info, NVector pos, string settings)
        {
            NAction action = Data.nAction[id];
            
            Assert.IsNotNull(action,$"Action {id} is missing.");
            Assert.IsNotNull(info,$"MapElementInfo is missing.");
            
            //has ap?
            if (action.cost > info.data.ap)
            {
                return $"Action {action.name} need {action.cost - info.data.ap} AP more. Please wait a round to refill your AP.";
            }

            //check pref
            Debug.Log($"call {action.id}");
            //can use?
            if (!ReqHelper.Check(player, ActionHelper.GenReq(action), info, pos))
            {
                return ReqHelper.Desc(player, ActionHelper.GenReq(action), info, pos);
            }

            info.data.ap -= action.cost;
            ButtonAction(player, info, pos, settings);
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