﻿using System;
using System.Linq;
using Audio;
using Buildings;
using Classes.Actions;
using Game;
using Libraries;
using Libraries.Buildings;
using Libraries.FActions;
using Libraries.GameButtons;
using Libraries.Inputs;
using MapElements;
using MapElements.Buildings;
using MapElements.Units;
using Players;
using Tools;
using UI;
using Units;
using UnityEngine;

namespace InputActions
{
    public class InputAction : MonoBehaviour
    {
        private UnitInfo _aUnit;
        private BuildingInfo _aBuilding;
        public MapElementInfo active;
        public Action<KeyCode> findKey;
        private int[] keyValues;
        private int _boundary;
        private int _width;
        private int _height;
        private float speed = 32;
        private Transform _trans;
        
        void Awake() {
            keyValues = (int[])System.Enum.GetValues(typeof(KeyCode));
            _width = Screen.width;
            _height = Screen.height;
            _boundary = 1;
            _trans = Camera.main.GetComponent<Transform>();
        }
        
        public void SetActive(UnitInfo unit)
        {
            _aUnit = unit;
            if (unit != null)
            {
                active = unit;
            }
            else
            {
                active = _aBuilding;
            }
        }

        public void SetActive(BuildingInfo building)
        {
            _aBuilding = building;
            if (building != null && _aUnit == null)
            {
                active = building;
            }
            else
            {
                active = _aUnit;
            }
        }
        
        // Update is called once per frame
        void Update()
        {
            //find key?
            if (findKey != null)
            {
                if (Input.anyKey)
                {
                    foreach (var key in keyValues)
                    {
                        if (key.ToString().StartsWith("mouse"))
                        {
                            continue;
                        }
                        
                        if (Input.GetKeyDown((KeyCode)key))
                            findKey((KeyCode)key);
                    }
                }
                return;
            }

            if (LSys.tem == null || LSys.tem.inputs == null)
            {
                Debug.Log($"LSys.tem is not loaded, can not perform key check. LSys.tem:${LSys.tem != null} LSys.tem.inputs:${LSys.tem != null && LSys.tem.inputs != null}");
                return;
            }
            
            //check all buttons
            foreach (InputKey key in LSys.tem.inputs.Values())
            {
                //active?
                if (key.Hidden)
                {
                    continue;
                }
                
                //pressed?
                if (!Input.GetKeyDown(key.KeyCode()))
                {
                    continue;
                }
                
                //req fullfilled?
                if (!key.req.Check(ActPlayer(),active, active?.Pos()))
                {
                    if (S.IsGame())
                        OnMapUI.Get().ShowPanelMessageError(key.req.Desc(ActPlayer(),active, active?.Pos()));
                    else
                    {
                        //TODO show error
                        NAudio.PlayBuzzer();
                    }
                    continue;
                }

                try
                {
                    if (key.IsGameButton())
                    {
                        PressGameButton(key);
                        return;
                    }
                
                    if (key.IsAction())
                    {
                        PressAction(key);
                        return;
                    }

                    PressHidden(key);
                    return;
                }
                catch (Exception e)
                {
                    ExceptionHelper.ShowException(e);
                }
                
            }
            
            MouseMoveMap();
        }

        private void MouseMoveMap()
        {
            //todo dynamic
            if (!S.Game() || WindowsMgmt.Get().AnyOpenWindow())
            {
                return;
            }
            
            //Source: http://coffeebreakcodes.com/camera-movement-with-mouse-unity3d-c/
            
            if (Input.GetMouseButton(2)) {
                if (Math.Abs(Input.GetAxis ("Mouse X")) > 0.01) {
                    var p = new Vector3 (Input.GetAxisRaw ("Mouse X") * Time.deltaTime * speed, 
                        Input.GetAxisRaw ("Mouse Y") * Time.deltaTime * speed, 0.0f);
                    //todo implement border check
                    _trans.position += p;
                }
                
            }
            
            if (Input.mousePosition.x > _width - _boundary && Input.mousePosition.x < _width + _boundary)
            {
                MoveCamera(1,0);
            }
		
            if (Input.mousePosition.x < 0 + _boundary && Input.mousePosition.x > 0 - _boundary)
            {
                MoveCamera(-1,0);
            }
		
            if (Input.mousePosition.y > _height - _boundary && Input.mousePosition.y < _height + _boundary)
            {
                MoveCamera(0,1);
            }
 
            if (Input.mousePosition.y < 0 + _boundary && Input.mousePosition.y > 0 - _boundary)
            {
                MoveCamera(0,-1);
            }
        }

        private void PressHidden(InputKey key)
        {
            switch (key.id)
            {
                case "moveUnitEast":
                    _aUnit.MoveBy(-1,0);
                    break;
                case "moveUnitSouth":
                    _aUnit.MoveBy(0,-1);
                    break;
                case "moveUnitWest":
                    _aUnit.MoveBy(1,0);
                    break;
                case "moveUnitNorth":
                    _aUnit.MoveBy(0,+1);
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
                case "moveCameraLevelTop":
                    GameMgmt.Get().newMap.view.ViewAdd(1);
                    break;
                case "moveCameraLevelDown":
                    GameMgmt.Get().newMap.view.ViewAdd(-1);
                    break;
                case "closeWindow":
                    WindowsMgmt.Get().GetAllOpenWindow().Last().DestroyWindow();
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
            //which has this action?
            if (_aUnit != null)
            {
                BaseDataBuildingUnit data = _aUnit.baseData;
                //unit contains action?
                if (data.action.Contains(key.id))
                {
                    //TODO check more settings
                    //can perform action?
                    ActionHolder action = data.action.Get(key.id);
                    OnMapUI.Get().unitUI.ShowPanelMessageError(data.action.Perform(action, ActionEvent.Direct, _aUnit.Player(), _aUnit, _aUnit.Pos()));
                    
                    return;
                }
            }
            
            if (_aBuilding != null)
            {
                BaseDataBuildingUnit data = _aBuilding.baseData;
                //unit contains action?
                if (data.action.Contains(key.id))
                {
                    //TODO check more settings
                    //can perform action?
                    ActionHolder action = data.action.Get(key.id);
                    OnMapUI.Get().unitUI.ShowPanelMessageError(data.action.Perform(action, ActionEvent.Direct, _aBuilding.Player(), _aBuilding, _aBuilding.Pos()));

                    return;
                }
            }
            
            //found nothing?
            OnMapUI.Get().unitUI.ShowPanelMessageError($"Action {L.b.actions[key.id].Name()} can not called, their is no unit or building, who can perform it.");
        }

        private Player ActPlayer()
        {
            return S.IsGame() ? S.ActPlayer() : null;
        }

        /// <summary>
        /// Press the game button
        /// </summary>
        /// <param name="key"></param>
        private void PressGameButton(InputKey key)
        {
            GameButton gameButton = L.b.gameButtons[key.id];
            
            //check if possible to call
            if (!gameButton.req.Check(ActPlayer()))
            {
                OnMapUI.Get().ShowPanelMessageError(gameButton.req.Desc(ActPlayer()));
                return;
            }
            
            //call it
            NAudio.Play(gameButton.Sound);
            gameButton.Call(ActPlayer());
        }

        void MoveCamera(int x, int y)
        {
            if (S.CameraMove().IsMoving())
                return;
            
            
            
            //valid pos?
            if (!S.Valid((int)_trans.position.x+x,(int)_trans.position.y+y))
            {
                OnMapUI.Get().ShowPanelMessageError("The camera position is to far outside of the map.");
                return;
            }
        
            S.CameraMove().MoveBy(x, y);
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
