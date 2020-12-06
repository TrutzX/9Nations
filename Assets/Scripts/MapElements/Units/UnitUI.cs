using System.Collections.Generic;
using Audio;
using Buildings;
using Classes.Actions;
using Game;
using InputActions;
using Libraries;
using Libraries.FActions;
using MapElements.Buildings;
using MapElements.Units;
using Players;
using reqs;
using UnityEngine;

namespace Units
{
    public class UnitUI : MapElementUI<UnitInfo>, IMapUI
    {

        // Start is called before the first frame update
        void Start()
        {
            //UpdatePanel(null);
        }

        public override void ShowPanelMessageError(string text)
        {
            base.ShowPanelMessageError(text);
            
            //show animation?
            if (text == null || active == null)
            {
                return;
            }
            
            active.UnitAnimator().PlayIdleAnimation(UnitAnimatorType.No);
        }
    
        /// <summary>
        /// Show this unit
        /// </summary>
        /// <param name="unit"></param>
        public override void UpdatePanel(UnitInfo unit)
        {
            S.InputAction().SetActive(unit);
            active = unit;
            OnMapUI.Get().InfoUi.UpdatePanel();
        
            //no unit?
            if (unit == null)
            {
                //hide it
                gameObject.SetActive(false);
                return;
            }
        
            UpdateInfoButton(unit.dataUnit.AnimationSprite(UnitAnimatorType.Face));
            AddButtons();
        }
    }
}
