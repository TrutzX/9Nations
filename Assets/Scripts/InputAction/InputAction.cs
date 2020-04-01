using System;
using Actions;
using Buildings;
using Classes.Actions;
using DataTypes;
using Game;
using GameButtons;
using Libraries;
using Libraries.Buildings;
using Libraries.FActions;
using Libraries.FActions.General;
using Libraries.GameButtons;
using Players;
using reqs;
using Tools;
using UI;
using Units;
using UnityEngine;

namespace InputAction
{
    public class InputAction : MonoBehaviour
    {
        public UnitInfo aUnit;
        public BuildingInfo aBuilding;
    

        // Update is called once per frame
        void Update()
        {
            //open window?
            if (WindowsMgmt.Get().AnyOpenWindow())
                return;
        
            //check all buttons
            foreach (InputKey key in Data.inputKey)
            {
                //active?
                if (key.hidden)
                {
                    continue;
                }
                
                //pressed?
                if (!Input.GetKeyDown(key.KeyCode()))
                {
                    continue;
                }

                try
                {
                    if (key.IsGameButton())
                    {
                        PressGameButton(key);
                        continue;
                    }
                
                    if (key.IsAction())
                    {
                        PressAction(key);
                        continue;
                    }

                    PressHidden(key);
                }
                catch (Exception e)
                {
                    ExceptionHelper.ShowException(e);
                }
                
            }
        }

        private void PressHidden(InputKey key)
        {
            //need a unit?
            if (key.active && aUnit == null)
            {
                OnMapUI.Get().ShowPanelMessageError($"{key.id} needs a selected unit.");
                return;
            }
            
            switch (key.id)
            {
                case "moveUnitEast":
                    aUnit.MoveBy(-1,0);
                    break;
                case "moveUnitSouth":
                    aUnit.MoveBy(0,-1);
                    break;
                case "moveUnitWest":
                    aUnit.MoveBy(1,0);
                    break;
                case "moveUnitNorth":
                    aUnit.MoveBy(0,+1);
                    break;
                case "moveCameraEast":
                    MoveCamera(-1,0);
                    break;
                case "moveCameraSouth":
                    MoveCamera(0,-1);
                    break;
                case "moveCameraWest":
                    MoveCamera(1,0);
                    break;
                case "moveCameraNorth":
                    MoveCamera(0,+1);
                    break;
                case "zoomCameraIn":
                    ZoomCamera(-1);
                    break;
                case "zoomCameraOut":
                    ZoomCamera(1);
                    break;
                case "moveLevelTop":
                    GameMgmt.Get().newMap.view.ViewAdd(1);
                    break;
                case "moveLevelDown":
                    GameMgmt.Get().newMap.view.ViewAdd(-1);
                    break;
                default:
                    OnMapUI.Get().ShowPanelMessageError($"{key.id} is not a valid call.");
                    break;
            }
            
        }

        /// <summary>
        /// Press the action
        /// </summary>
        /// <param name="key"></param>
        private void PressAction(InputKey key)
        {
            //has an unit or building?
            if (aUnit == null && aBuilding == null)
            {
                OnMapUI.Get().ShowPanelMessageError($"Action {L.b.actions[key.id].name} can not called, their is no unit or building, who can perform it.");
                return;
            }
            
            //which has this action?
            if (aUnit != null)
            {
                BaseDataBuildingUnit data = aUnit.baseData;
                //unit contains action?
                if (data.action.Contains(key.id))
                {
                    //TODO check more settings
                    //can perform action?
                    ActionHolder action = data.action.Get(key.id);
                    OnMapUI.Get().unitUI.ShowPanelMessageError(data.action.Perform(action, ActionEvent.Direct, aUnit.Player(), aUnit, aUnit.Pos()));
                    
                    return;
                }
            }
            
            if (aBuilding != null)
            {
                BaseDataBuildingUnit data = aBuilding.baseData;
                //unit contains action?
                if (data.action.Contains(key.id))
                {
                    //TODO check more settings
                    //can perform action?
                    ActionHolder action = data.action.Get(key.id);
                    OnMapUI.Get().unitUI.ShowPanelMessageError(data.action.Perform(action, ActionEvent.Direct, aBuilding.Player(), aBuilding, aBuilding.Pos()));

                    return;
                }
            }
            
            //found nothing?
            OnMapUI.Get().unitUI.ShowPanelMessageError($"Action {L.b.actions[key.id].name} can not called, their is no unit or building, who can perform it.");
        }

        /// <summary>
        /// Press the game button
        /// </summary>
        /// <param name="key"></param>
        private void PressGameButton(InputKey key)
        {
            GameButton gameButton = L.b.gameButtons[key.id];
            
            //check if possible to call
            if (!gameButton.req.Check(PlayerMgmt.ActPlayer()))
            {
                OnMapUI.Get().ShowPanelMessageError(gameButton.req.Desc(PlayerMgmt.ActPlayer()));
                return;
            }
            
            //call it
            NAudio.Play(gameButton.Sound);
            gameButton.Call(PlayerMgmt.ActPlayer());
        }

        void MoveCamera(int x, int y)
        {
            if (CameraMove.Get().IsMoving())
                return;
            
            Transform trans = Camera.main.GetComponent<Transform>();
            
            //valid pos?
            if (!GameHelper.Valid((int)trans.position.x+x,(int)trans.position.y+y))
            {
                OnMapUI.Get().ShowPanelMessageError("The camera position is to far outside of the map.");
                return;
            }
        
            CameraMove.Get().MoveBy(x, y);
        }

        void ZoomCamera(float zoom)
        {
            //valide?
            if (Camera.main.orthographicSize + zoom < 1)
            {
                OnMapUI.Get().ShowPanelMessageError("The camera zoom is to minimal");
                Camera.main.orthographicSize = 1;
                return;
            }
            
            //valide?
            if (Camera.main.orthographicSize + zoom > 20)
            {
                OnMapUI.Get().ShowPanelMessageError("The camera zoom is to maximal");
                Camera.main.orthographicSize = 20;
                return;
            }

            Camera.main.orthographicSize += zoom;
        }
    }
}
