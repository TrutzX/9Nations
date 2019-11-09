using System;
using System.Collections;
using System.Collections.Generic;
using Buildings;
using DataTypes;
using Game;
using Players;
using reqs;
using UI;
using Units;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Actions
{
    public class TrainAction : BaseAction
    {
        protected override void ButtonAction(Player player, MapElementInfo gameObject, int x, int y, string settings)
        {
            Debug.Log(gameObject);
            string[] keys;
            if (!String.IsNullOrEmpty(settings))
            {
                keys = settings.Split(',');
            }
            else
            {
                keys = UnitHelper.GetIDs();
            }
            
            //load buildings
            WindowBuilderSplit b = CreateSplit();

            foreach (string key in keys)
            {
                Unit unit = Data.unit[key];
                Dictionary<string, string> reqs = unit.GenBuildReq();
                if (ReqHelper.CheckOnlyFinal(player, reqs, null, x, y))
                {
                    UnitSplitElement be = new UnitSplitElement(unit, x, y);
                    be.disabled = ReqHelper.Desc(player, reqs, null, x, y);
                    b.AddElement(be);
                }
                
            }

            b.Finish();

            
        }

        protected override void ButtonAction(Player player, string settings)
        {
            Debug.LogWarning("Not implemented");
        }
        
        public class UnitSplitElement : SplitElement
        {
            protected Unit unit;
            protected int x, y;
            public UnitSplitElement(Unit unit, int x, int y) : base(unit.name, unit.GetIcon())
            {
                this.unit = unit;
                this.x = x;
                this.y = y;
            }

            public override void ShowDetail(PanelBuilder panel)
            {
                unit.ShowInfo(panel, null, x, y);
            }

            public override void Perform()
            {
                UnitMgmt.Get().Create(PlayerMgmt.ActPlayer().id, unit.id,x, y);
                OnMapUI.Get().UpdatePanelXY(x,y);
            }
        }
    }

}