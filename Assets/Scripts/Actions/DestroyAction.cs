﻿using System.Collections;
using System.Collections.Generic;
using Buildings;
using Game;
using Players;
using UI;
using UnityEngine;

namespace Actions
{
    public class DestroyAction : BaseAction
    {
        
        protected override void ButtonAction(Player player, MapElementInfo gameObject, int x, int y, string settings)
        {
            //TODO give ress back
            //TODO show dialog
            gameObject.Kill();

            OnMapUI.Get().UpdatePanelXY(x,y);
        }

        protected override void ButtonAction(Player player, string settings)
        {
            Debug.LogWarning("Not implemented");
        }
    }

}