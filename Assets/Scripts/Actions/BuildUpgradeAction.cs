using System;
using System.Collections;
using System.Collections.Generic;
using Buildings;
using DataTypes;
using Game;
using Players;
using reqs;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Actions
{
    public class BuildUpgradeAction : BaseAction
    {
        protected override void ButtonAction(Player player, MapElementInfo gameObject, int x, int y, string settings)
        {
            string[] keys = settings.Split(',');
            
            WindowBuilderSplit b = CreateSplit();

            //load buildings
            foreach (string key in keys)
            {
                Building build = Data.building[key];
                Dictionary<string, string> reqs = build.GenBuildReq();
                if (ReqHelper.CheckOnlyFinal(player, reqs, gameObject, x, y))
                {
                    BuildSplitElement be = new BuildSplitElement(build, gameObject, x, y);
                    be.disabled = ReqHelper.Desc(player, reqs, gameObject, x, y);
                    b.AddElement(be);
                    //win.AddBuilding(build.id);
                }
                
            }
            
            //found nothing?
            if (b.ElementCount() == 0)
            {
                b.CloseWindow();
                if (gameObject.IsBuilding())
                    OnMapUI.Get().buildingUI.SetPanelMessageError("Not possible to upgrade. Maybe research something?");
                else
                {
                    OnMapUI.Get().unitUI.SetPanelMessageError("Not possible to upgrade. Maybe research something?");
                }
                
                return;
            }
            
            b.Finish();

            
        }

        protected override void ButtonAction(Player player, string settings)
        {
            Debug.LogWarning("Not implemented");
        }
        
        class BuildSplitElement : BuildAction.BuildSplitElement
        {
            public BuildSplitElement(Building build, MapElementInfo gameObject, int x, int y) : base(build, gameObject, x, y)
            {
            }

            public override void Perform()
            {
                BuildingMgmt.At(x,y).Upgrade(build.id);
            }
        }
    }

}