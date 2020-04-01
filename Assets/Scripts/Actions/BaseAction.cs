using System;
using System.Collections;
using System.Collections.Generic;
using Buildings;
using Classes.Actions.Addons;
using DataTypes;
using JetBrains.Annotations;
using Players;
using reqs;
using Tools;
using UI;
using Units;
using UnityEngine;
using UnityEngine.Assertions;
using MapElementInfo = Buildings.MapElementInfo;

namespace Actions
{
    [Obsolete]
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
    }
}