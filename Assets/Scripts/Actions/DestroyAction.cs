using System.Collections;
using System.Collections.Generic;
using Game;
using Players;
using UnityEngine;

namespace Actions
{
    public class DestroyAction : BaseAction
    {
        
        protected override void ButtonAction(Player player, GameObject gameObject, int x, int y, string settings)
        {
            //TODO give ress back
            GameHelper.GetMapElement(gameObject).Kill();

            OnMapUI.Get().UpdatePanelXY(x,y);
        }

        protected override void ButtonAction(Player player, string settings)
        {
            Debug.LogWarning("Not implemented");
        }
    }

}