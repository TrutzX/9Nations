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
    public class GameButtonOldAction : BaseOldAction
    {
        protected override void ButtonAction(Player player, GameObject gameObject, int x, int y, string settings)
        {
            Debug.LogWarning("Not implemented");
        }

        protected override void ButtonAction(Player player, string settings)
        {
            GameButtonHelper.Call(settings, player);
        }
    }

}