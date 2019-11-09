using System;
using Game;
using JetBrains.Annotations;
using Players;
using Players.Infos;
using Towns;
using UI;
using UnityEngine;

namespace Buildings
{
    public abstract class MapElementInfo : MonoBehaviour
    {
        public BuildingUnitData data;
        
        public abstract void Kill();
        
        public abstract string UniversalImage();

        public abstract void FinishConstruct();

        /// <summary>
        /// Get the town
        /// </summary>
        /// <returns></returns>
        [CanBeNull]
        public Town Town()
        {
            return data.townId==-1?null:TownMgmt.Get(data.townId);
        }

        public Player Player() => PlayerMgmt.Get(data.playerId);

        /// <summary>
        /// Check if the player id is the owner
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Owner(int id) => id == data.playerId;

        public int X() => data.x;

        public int Y() => data.y;

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
            if (!Owner(playerId))
            {
                return $"{gameObject.name} belongs to {Player().name}";
            }
        
            if (IsUnderConstrution())
            {
                return $"{gameObject.name} under construction ({(int) (GetComponent<Construction>().GetConstructionProcent()*100)}%) {data.lastInfo}";
            }
            
            
            //add hp?
            string hp = data.hp < data.hpMax ? $"HP:{data.hp}/{data.hpMax}, " : "";
            return $"{gameObject.name} {hp}AP:{data.ap}/{data.apMax} {data.lastInfo}";
        }

        public bool IsUnderConstrution()
        {
            return GetComponent<Construction>() != null;
        }
        
        public virtual WindowBuilderSplit ShowInfoWindow()
        {
            return WindowBuilderSplit.Create(gameObject.name,null);
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
            data.lastInfo = mess;
            Player().info.Add(new Info(mess,UniversalImage()).AddAction("cameraMove",X()+":"+Y()));
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
    }
}