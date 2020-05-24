using System.Collections.Generic;
using Buildings;
using Classes.Actions;
using Game;
using InputActions;
using Libraries;
using Libraries.FActions;
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
    
        /// <summary>
        /// Show this unit
        /// </summary>
        /// <param name="unit"></param>
        public override void UpdatePanel(UnitInfo unit)
        {
            S.InputAction().SetActive(unit);
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
                if (act.req.Check(S.ActPlayer(),active,active.Pos(), true))
                    AddNewActionButton(active.data.action, act, active, actions);
            }
        }
    }
}
