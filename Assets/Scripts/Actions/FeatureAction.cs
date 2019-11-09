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
    public class FeaturePlayerOldAction : BaseOldAction
    {
        protected override void ButtonAction(Player player, GameObject gameObject, int x, int y, string settings)
        {
            ButtonAction(player, settings);
        }

        protected override void ButtonAction(Player player, string settings)
        {
            string[] v = settings.Split(',');
            player.SetFeature(v[0],v[1]);
        }
    }

}