using System;
using System.Collections;
using System.Collections.Generic;
using Buildings;
using DataTypes;
using Game;
using Libraries;
using Players;
using Players.Infos;
using reqs;
using UI;
using Units;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Actions
{
    public class TownLevelAction : BaseAction
    {
        protected override void ButtonAction(Player player, MapElementInfo gameObject, int x, int y, string settings)
        {
            //add
            int level = string.IsNullOrEmpty(settings) ? 1 : Int32.Parse(settings);
            gameObject.Town().level += level;
            
            //inform
            gameObject.Player().info.Add(new Info($"Develop {gameObject.Town().name} to {gameObject.Town().GetTownLevelName()}","foundtown"));
        }

        protected override void ButtonAction(Player player, string settings)
        {
            Debug.LogWarning("Not implemented");
        }
        
    }

}