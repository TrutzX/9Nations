using System.Collections.Generic;
using Actions;
using Buildings;
using Classes.Actions;
using Libraries;
using Libraries.FActions;
using Players;
using reqs;
using UnityEngine;

namespace Units
{
    public class UnitUI : MapElementUI<UnitInfo>
    {

        // Start is called before the first frame update
        void Start()
        {
            UpdatePanel(null);
        }
    
        /// <summary>
        /// Show this unit
        /// </summary>
        /// <param name="unit"></param>
        public override void UpdatePanel(UnitInfo unit)
        {
            GameObject.Find("InputAction").GetComponent<InputAction.InputAction>().aUnit = unit;
            active = unit;
        
            //no unit?
            if (unit == null)
            {
                //hide it
                gameObject.SetActive(false);
                return;
            }
        
            UpdateInfoButton();
            AddButtons();
        }

        public override void AddAllActionButtons()
        {
            
            //add new actions
            foreach (var act in active.data.action.Is(ActionEvent.Direct))
            {
                if (act.req.Check(PlayerMgmt.ActPlayer(),active,active.Pos(), true))
                    AddNewActionButton(active.data.action, act, active, actions);
            }
        }
    }
}
