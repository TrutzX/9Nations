using System;
using System.Collections;
using System.Collections.Generic;
using DataTypes;
using Game;
using Players;
using reqs;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Actions
{
    public class EndGameLoseAction : BaseAction
    {
        protected override void ButtonAction(Player player, GameObject gameObject, int x, int y, string settings)
        {
            ButtonAction(player, settings);
        }

        protected override void ButtonAction(Player player, string settings)
        {
            player.status = "lose";
            Debug.LogWarning(player.status);
        }
    }
    
    
    public class EndGameWinAction : BaseAction
    {
        protected override void ButtonAction(Player player, GameObject gameObject, int x, int y, string settings)
        {
            ButtonAction(player, settings);
        }

        protected override void ButtonAction(Player player, string settings)
        {
            player.status = "win";
            Debug.LogWarning(player.status);
        }
    }

}