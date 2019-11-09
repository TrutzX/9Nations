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
    public class CameraMoveOldAction : BaseOldAction
    {
        protected override void ButtonAction(Player player, GameObject gameObject, int x, int y, string settings)
        {
            Debug.LogWarning("Not implemented");
        }

        protected override void ButtonAction(Player player, string settings)
        {
            string[] sett = settings.Split(':');
            CameraMove.Get().MoveTo(Int32.Parse(sett[0]),Int32.Parse(sett[1]));
        }
    }

}