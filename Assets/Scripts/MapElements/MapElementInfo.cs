using System;
using System.Collections.Generic;
using Buildings;
using Classes.Actions;
using Debugs;
using Game;
using Help;
using JetBrains.Annotations;
using Libraries;
using Libraries.Buildings;
using Libraries.FActions;
using Libraries.FActions.General;
using Libraries.Terrains;
using MapElements.Buildings;
using MapElements.Spells;
using Players;
using Players.Infos;
using Players.Kingdoms;
using Tools;
using Towns;
using UI;
using UnityEngine;

namespace MapElements
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

        public Player Player() => S.Player(data.playerId);

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
                return $"{gameObject.name} under construction ({(int) (GetComponent<Construction>().GetConstructionProcent()*100)}%) {data.info.LastInfo()}";
            }

            if (data.waiting != null)
            {
                ActionHolder a = data.action.actions[data.waiting.actionPos];
                FDataAction da = a.DataAction();
                text += $"Prepare {da.Name()}";

                if (data.waiting.endless)
                    text += $" (repeat unlimited).";
                else
                    text += $" ({TextHelper.Proc(data.waiting.ap, data.waiting.apMax)}).";
                
                
                if (S.Debug())
                    text += data.ap+"/"+data.waiting.ap + "/" + data.waiting.apMax;
            }

            //add hp?
            string hp = data.hp < data.hpMax ? $"HP:{data.hp}/{data.hpMax}, " : "";
            return $"{gameObject.name} {hp}AP:{data.ap}/{data.apMax}{text} {data.info.LastInfo()}";
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
            if (IsUnderConstruction() && GetComponent<Construction>().NextRound())
            {
                //AddNoti("IsUnderConstruction");
                return false;
            }
            
            //has a town?
            if (data.townId == -1)
            {
                //AddNoti("no town");
                return false;
            }
            
            //perform actions
            string erg = data.action.Performs(ActionEvent.NextRound, Player(), this, Pos());
            SetLastInfo(erg);
            
            //AddNoti("erg:"+erg?.Length);
            
            return String.IsNullOrEmpty(erg);
        }
        
        public virtual void FinishConstruct()
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            
            SetLastInfo($"Finish construction of {data.name}.");

            data.construction = null;
            Destroy(GetComponent<Construction>());
            //show it
            Player().fog.Clear(Pos(), L.b.modifiers["view"].CalcModiNotNull(baseData.visibilityRange,Player(),Pos()));
        
            //perform actions
            SetLastInfo(data.action.Performs(ActionEvent.FinishConstruct, Player(), this, Pos()));
        }
        
        public virtual WindowBuilderSplit ShowInfoWindow()
        {
            WindowBuilderSplit win =  WindowBuilderSplit.Create(gameObject.name,null);
            win.Add(new ActionDisplaySplitElement(this));
            win.Add(new TerrainSplitElement(GameMgmt.Get().newMap.Terrain(Pos()), Pos()));
            if (data.townId != -1)
                win.Add(new CameraTownSplitElement(win, Town()));
            if (S.Debug())
            {
                win.Add(new DebugMapElementSplitElement(this));
                win.Add(new DebugSpellSplitElement(this));
            }
                
            if (L.b.improvements.Has(Pos()))
                win.Add(new LexiconSplitElement(L.b.improvements.At(Pos())));
            
            win.Add(new InfosSplitElement(data.info));
            
            return win;
        }

        public void SetSprite(string sprite)
        {
            GetComponent<SpriteRenderer>().sprite = SpriteHelper.Load(sprite);
            data.sprite = sprite;
        }

        public Sprite Sprite()
        {
            return GetComponent<SpriteRenderer>().sprite;
        }

        public virtual void Load(BuildingUnitData data)
        {
            this.data = data;
            this.data.info.mapElementInfo = this;
        
            if (data.construction != null)
            {
                gameObject.AddComponent<Construction>();
                gameObject.GetComponent<Construction>().Load(data);
            }
        
        }

        [Obsolete]
        public void SetLastInfo(string mess)
        {
            AddNoti(mess, null);
        }

        public void AddNoti(string mess, string icon)
        {
            if (string.IsNullOrEmpty(mess))
            {
                return;
            }
            
            AddNoti(new Info(mess, icon ?? baseData.Icon, baseData.Icon));
        }

        public void AddNoti(Info info)
        {
            if (info.action == null)
            {
                info.AddAction("cameraMove",$"{Pos().level};{Pos().x};{Pos().y}");
            }
            
            data.info.Add(info);
        }
        
        public void AddHp(int hp) {
            data.hp += hp;
            data.hp = Math.Min(data.hp,data.hpMax);

            if (data.hp <= 0) {
                SetLastInfo($"{data.name} was destroyed.");
                Kill();
            }

        }

        public abstract void Upgrade(string type, Dictionary<string, int> cost);

        /// <summary>
        /// work on fog
        /// </summary>
        protected void Clear(NVector pos)
        {
            Player().fog.Clear(pos, CalcVisibleRange(pos));
        }

        public int CalcVisibleRange(NVector pos, int fac=1)
        {
            return L.b.modifiers["view"].CalcModiNotNull(baseData.visibilityRange*fac, Player(), pos);
        }

        public void SetActive()
        {
            FindObjectOfType<OnMapUI>().UpdatePanel(Pos());
            S.CameraMove().MoveTo(Pos());
        }

        public IMapUI UI()
        {
            if (IsBuilding())
                return OnMapUI.Get().buildingUI;
            return OnMapUI.Get().unitUI;
        }

        public void SetWaitingAction(ActionWaiting waiting)
        {
            data.waiting = waiting;
            if (waiting == null) return;
            
            //todo calc magic wait ap
            waiting.ap = data.ap;
            data.ap = 0;

            //Debug.Log(waiting.ap+"/"+waiting.apMax);
        }

        public void SetRepeatAction(ActionWaiting waiting)
        {
            data.waiting = waiting;
            if (waiting == null) return;

            waiting.endless = true;
            
            //perform first
            ActionHolder a = data.action.actions[waiting.actionPos];
            string erg = data.action.Perform(a, ActionEvent.NextRound, Player(), this, waiting.pos);
            if (!string.IsNullOrEmpty(erg))
                SetLastInfo($"Performs {a.DataAction().Name()}. {erg}");
        }

        public void StartPlayerRound()
        {
            //has a waiting round?
            if (data.waiting == null) return;
            
            ActionHolder a = data.action.actions[data.waiting.actionPos];
            FDataAction da = a.DataAction();

            //perform every turn?
            if (data.waiting.endless)
            {
                string erg2 = data.action.Perform(a, ActionEvent.NextRound, Player(), this, data.waiting.pos);
                if (!string.IsNullOrEmpty(erg2))
                    SetLastInfo($"Performs {da.Name()}. {erg2}");
                return;
            }

            //wait more?
            if (data.waiting.apMax > data.ap + data.waiting.ap)
            {
                data.waiting.ap += data.ap;
                data.ap = 0;
                return;
            }

            data.ap = data.waiting.apMax - data.waiting.ap;
            data.ap += data.waiting.ap;
            a.data["waiting"] = data.waiting.sett;
            string erg = data.action.Perform(a, data.waiting.evt, Player(), this, data.waiting.pos);
            a.data.Remove("waiting");
            data.ap = data.waiting.apMax - data.waiting.ap;
            SetLastInfo($"Performs {da.Name()}. {erg}");
            data.waiting = null;
        }

        protected void CalcUpgradeCost(Dictionary<string, int> buildCost, Dictionary<string, int> oldCost)
        {
            //calc new cost
            foreach (var cost in new Dictionary<string, int>(buildCost))
            {
                if (!oldCost.ContainsKey(cost.Key))
                {
                    continue;
                }

                if (cost.Value > oldCost[cost.Key])
                {
                    data.construction[cost.Key] -= oldCost[cost.Key];
                }
                else
                {
                    data.construction.Remove(cost.Key);
                }
            }
        }
    }
}