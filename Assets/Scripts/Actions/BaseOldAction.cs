using System.Collections;
using System.Collections.Generic;
using Buildings;
using DataTypes;
using Players;
using reqs;
using UI;
using Units;
using UnityEngine;

namespace Actions
{
    public abstract class BaseOldAction : BaseAction
    {
        protected abstract void ButtonAction(Player player, GameObject gameObject, int x, int y, string settings);
        
        protected override void ButtonAction(Player player, MapElementInfo gameObject, int x, int y, string settings)
        {
            ButtonAction(player, gameObject.gameObject, x, y, settings);
        }
    }
}