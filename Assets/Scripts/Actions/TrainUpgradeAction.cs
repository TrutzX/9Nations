using System;
using System.Collections;
using System.Collections.Generic;
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
    public class TrainUpgradeAction : BaseAction
    {
        protected override void ButtonAction(Player player, GameObject gameObject, int x, int y, string settings)
        {
            string[] keys = settings.Split(',');
            
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
        
        class UnitSplitElement : TrainAction.UnitSplitElement
        {
            public UnitSplitElement(Unit unit, int x, int y) : base(unit, x,y)
            {
            }

            public override void Perform()
            {
                UnitMgmt.At(x,y).Kill();
                base.Perform();
            }
        }
    }

}