using System;
using Actions;
using DataTypes;
using Game;
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
                OnMapUI.Get().SetMenuMessageError($"{key.id} needs a selected unit.");
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
                default:
                    OnMapUI.Get().SetMenuMessageError($"{key.id} is not a valid call.");
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
                return;
            }
            
            NAction action = Data.nAction[key.id];
            
            //which has this action?
            if (aUnit != null)
            {
                Unit dataUnit = aUnit.config;
                //unit contains action?
                if (dataUnit.GetActions().ContainsKey(key.id))
                {
                    //TODO check more settings
                    //can perform action?
                    string mess = NLib.GetAction(action.id).ButtonRun(aUnit, aUnit.X(), aUnit.Y(), dataUnit.GetActions()[key.id]);
                    if (mess != null)
                    {
                        OnMapUI.Get().unitUI.SetPanelMessageError(mess);
                    }
                    return;
                }
            }
            
            if (aBuilding != null)
            {
                Building dataUnit = aBuilding.config;
                //unit contains action?
                if (dataUnit.GetActions().ContainsKey(key.id))
                {
                    //TODO check more settings
                    //can perform action?
                    string mess = NLib.GetAction(action.id).ButtonRun(aBuilding, aBuilding.X(), aBuilding.Y(), dataUnit.GetActions()[key.id]);
                    if (mess != null)
                    {
                        OnMapUI.Get().buildingUI.SetPanelMessageError(mess);
                    }
                    return;
                }
            }
            
            //found nothing?
            OnMapUI.Get().unitUI.SetPanelMessageError($"Action {action.name} can not called, their is no unit or building, who can perform it.");
        }

        /// <summary>
        /// Press the game button
        /// </summary>
        /// <param name="key"></param>
        private void PressGameButton(InputKey key)
        {
            GameButton gameButton = Data.gameButton[key.id];
            
            //check if possible to call
            if (!gameButton.CheckReq(PlayerMgmt.ActPlayer()))
            {
                OnMapUI.Get().SetMenuMessageError(ReqHelper.Desc(PlayerMgmt.ActPlayer(),gameButton.GenReq()));
                return;
            }
            
            //call it
            NAudio.Play(gameButton.sound);
            GameButtonHelper.Call(gameButton.id, PlayerMgmt.ActPlayer());
        }

        void MoveCamera(int x, int y)
        {
            if (CameraMove.Get().IsMoving())
                return;
            
            Transform trans = Camera.main.GetComponent<Transform>();
            
            //valid pos?
            if (!GameHelper.Valide((int)trans.position.x+x,(int)trans.position.y+y))
            {
                OnMapUI.Get().SetMenuMessageError("The camera position is to far outside of the map.");
                return;
            }
        
            CameraMove.Get().MoveBy(x, y);
        }

        void ZoomCamera(float zoom)
        {
            //valide?
            if (Camera.main.orthographicSize + zoom < 1)
            {
                OnMapUI.Get().SetMenuMessageError("The camera zoom is to minimal");
                Camera.main.orthographicSize = 1;
                return;
            }
            
            //valide?
            if (Camera.main.orthographicSize + zoom > 20)
            {
                OnMapUI.Get().SetMenuMessageError("The camera zoom is to maximal");
                Camera.main.orthographicSize = 20;
                return;
            }

            Camera.main.orthographicSize += zoom;
        }
    }
}
