using System.Collections.Generic;

using Classes.Actions;
using Game;
using InputActions;
using Libraries.FActions;
using MapElements.Buildings;
using Players;
using reqs;
using UnityEngine;

namespace Buildings
{
    public class BuildingUI : MapElementUI<BuildingInfo>, IMapUI
    {
        // Start is called before the first frame update
        void Start()
        {
            //UpdatePanel(null);
        }
    
        /// <summary>
        /// Show this unit
        /// </summary>
        /// <param name="building"></param>
        public override void UpdatePanel(BuildingInfo building)
        {
            S.InputAction().SetActive(building);
            active = building;
            OnMapUI.Get().InfoUi.UpdatePanel();
        
            //no unit?
            if (building == null)
            {
                //hide it
                gameObject.SetActive(false);
                return;
            }

            UpdateInfoButton(active.Sprite());
            AddButtons();
        }
    }
}
