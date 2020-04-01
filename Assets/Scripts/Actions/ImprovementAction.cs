using System;
using System.Collections;
using System.Collections.Generic;
using Buildings;
using DataTypes;
using Game;
using Libraries;
using Players;
using reqs;
using Tools;
using UI;
using Units;
using UnityEngine;
using UnityEngine.SceneManagement;
using MapElementInfo = Buildings.MapElementInfo;

namespace Actions
{
    public class ImprovementAction : BaseAction
    {
        protected override void ButtonAction(Player player, MapElementInfo gameObject, NVector pos, string settings)
        {
            string[] i = settings.Split(';');
            
            //set improvement
            L.b.improvements.Set(i[0],gameObject.Pos());
            
            //kill?
            if (i.Length >= 2 && i[1] == "kill")
            {
                gameObject.Kill();
            }
        }

        protected override void ButtonAction(Player player, string settings)
        {
            Debug.LogWarning("Not implemented");
        }
        
    }

}