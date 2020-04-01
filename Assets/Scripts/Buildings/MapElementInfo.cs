using System;
using System.Collections.Generic;
using Classes.Actions;
using Game;
using GameButtons;
using Help;
using JetBrains.Annotations;
using Libraries;
using Libraries.Buildings;
using Libraries.FActions;
using Libraries.FActions.General;
using Libraries.Terrains;
using Players;
using Players.Infos;
using reqs;
using Tools;
using Towns;
using UI;
using UnityEngine;

namespace Buildings
{
    public abstract class MapElementInfo : MonoBehaviour
    {
        public BuildingUnitData data; //save data
        public BaseDataBuildingUnit baseData;
        
        public abstract void Kill();

        /// <summary>
        /// Get the town
        /// </summary>
        /// <returns></returns>
        [CanBeNull]
        public Town Town()
        {
            return data.townId==-1?null:S.Towns().Get(data.townId);
        }

        public Player Player() => PlayerMgmt.Get(data.playerId);

        /// <summary>
        /// Check if the player id is the owner
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Owner(int id) => id == data.playerId;
        
        public NVector Pos() => data.pos;

        /// <summary>
        /// check if it a building or a unit
        /// </summary>
        /// <returns></returns>
        public bool IsBuilding()
        {
            return (this is BuildingInfo);
        }
        
        public string Status(int playerId)
        {
            string text = " ";
            if (!Owner(playerId))
            {
                return $"{gameObject.name} belongs to {Player().name}";
            }
        
            if (IsUnderConstruction())
            {
                return $"{gameObject.name} under construction ({(int) (GetComponent<Construction>().GetConstructionProcent()*100)}%) {data.lastInfo}";
            }

            if (data.actionWaitingActionPos != -1)
            {
                ActionHolder a = data.action.actions[data.actionWaitingActionPos];
                FDataAction da = a.DataAction();
                text += $"Prepare {da.name} ({TextHelper.Proc(data.ActionWaitingAp, da.cost)}).";
            }

            //add hp?
            string hp = data.hp < data.hpMax ? $"HP:{data.hp}/{data.hpMax}, " : "";
            return $"{gameObject.name} {hp}AP:{data.ap}/{data.apMax}{text} {data.lastInfo}";
        }

        public bool IsUnderConstruction()
        {
            return GetComponent<Construction>() != null;
        }

        /// <summary>
        /// Perform the duties
        /// </summary>
        /// <returns>true > finish normal, false > has a problem</returns>
        public virtual bool NextRound()
        {
            //under construction?
            if (IsUnderConstruction() && GetComponent<Construction>().RoundConstruct())
            {
                return false;
            }
            
            //has a town?
            if (data.townId == -1)
            {
                return false;
            }
            
            //perform actions
            SetLastInfo(data.action.Performs(ActionEvent.NextRound, Player(), this, Pos()));

            return true;
        }
        
        public virtual void FinishConstruct()
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            
            SetLastInfo($"Finish construction of {data.name}.");
            
            Destroy(GetComponent<Construction>());
            //show it
            Player().fog.Clear(Pos(), L.b.modifiers["view"].CalcModiNotNull(baseData.visibilityRange,Player(),Pos()));
        
            //perform actions
            SetLastInfo(data.action.Performs(ActionEvent.FinishConstruct, Player(), this, Pos()));
        }
        
        public virtual WindowBuilderSplit ShowInfoWindow()
        {
            WindowBuilderSplit win =  WindowBuilderSplit.Create(gameObject.name,null);
            win.AddElement(new ActionDisplaySplitElement(this));
            win.AddElement(new TerrainSplitElement(GameMgmt.Get().newMap.Terrain(Pos()), Pos()));
            if (data.townId != -1)
                win.AddElement(new KingdomOverview.CameraTownSplitElement(win, Town()));
            if (Data.features.debug.Bool())
                win.AddElement(new DebugMapElementSplitElement(this));
            if (L.b.improvements.Has(Pos()))
                win.AddElement(new LexiconSplitElement(L.b.improvements.At(Pos())));
            return win;
        }

        public void SetSprite(string sprite)
        {
            GetComponent<SpriteRenderer>().sprite = SpriteHelper.Load(sprite);
            data.sprite = sprite;
        }

        public virtual void Load(BuildingUnitData data)
        {
            this.data = data;
        
            if (data.construction != null)
            {
                gameObject.AddComponent<Construction>();
                gameObject.GetComponent<Construction>().Load(data);
            }
        
        }

        public void SetLastInfo(string mess)
        {
            if (string.IsNullOrEmpty(mess))
            {
                return;
            }
            
            data.lastInfo = mess;
            Player().info.Add(new Info(mess,baseData.Icon).AddAction("cameraMove",$"{Pos().level};{Pos().x};{Pos().y}"));
        }
        
        public void AddHp(int hp) {
            data.hp += hp;
            data.hp = Math.Min(data.hp,data.hpMax);

            if (data.hp <= 0) {
                SetLastInfo($"{data.name} was destroyed.");
                Kill();
            }

        }

        public abstract void Upgrade(string type);

        /// <summary>
        /// work on fog
        /// </summary>
        protected void Clear(NVector pos)
        {
            Player().fog.Clear(pos, L.b.modifiers["view"].CalcModiNotNull(baseData.visibilityRange,Player(),pos));
        }

        public void SetActive()
        {
            FindObjectOfType<OnMapUI>().UpdatePanel(Pos());
            CameraMove.Get().MoveTo(Pos());
        }

        public IMapUI UI()
        {
            if (IsBuilding())
                return OnMapUI.Get().buildingUI;
            return OnMapUI.Get().unitUI;
        }

        public void SetWaitingAction(int actionPos, NVector pos)
        {
            data.actionWaitingActionPos = actionPos;
            if (actionPos == -1) return;
            
            data.ActionWaitingAp = data.ap;
            data.ap = 0;
            data.actionWaitingPos = pos;

        }

        public void StartPlayerRound()
        {
            //has a waiting round?
            if (data.actionWaitingActionPos == -1) return;

            ActionHolder a = data.action.actions[data.actionWaitingActionPos];
            FDataAction da = a.DataAction();
            if (da.cost > data.ap + data.ActionWaitingAp)
            {
                data.ActionWaitingAp += data.ap;
                data.ap = 0;
                return;
            }

            data.ap = da.cost - data.ActionWaitingAp;
            data.actionWaitingActionPos = -1;
            data.ap += data.ActionWaitingAp;
            string erg = data.action.Perform(a, ActionEvent.Direct, Player(), this, data.actionWaitingPos);
            data.ap = da.cost - data.ActionWaitingAp;
            SetLastInfo($"Performs {da.name}. {erg}");
        }
    }
}