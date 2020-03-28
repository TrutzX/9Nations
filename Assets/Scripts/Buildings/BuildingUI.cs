﻿using System.Collections.Generic;
using Actions;
using Classes.Actions;
using Libraries.FActions;
using Players;
using reqs;
using UnityEngine;

namespace Buildings
{
    public class BuildingUI : MapElementUI<BuildingInfo>
    {
        // Start is called before the first frame update
        void Start()
        {
            UpdatePanel(null);
        }
    
        /// <summary>
        /// Show this unit
        /// </summary>
        /// <param name="building"></param>
        public override void UpdatePanel(BuildingInfo building)
        {
            GameObject.Find("InputAction").GetComponent<InputAction.InputAction>().aBuilding = building;
            active = building;
        
            //no unit?
            if (building == null)
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
                BasePerformAction ba = act.PerformAction();
                if (act.req.Check(PlayerMgmt.ActPlayer(),active,active.Pos(), true))
                    AddNewActionButton(active.data.action, act, active, actions);
            }
        }
    }
}
