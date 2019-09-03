using System;
using System.Collections;
using System.Collections.Generic;
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
        protected override void ButtonAction(Player player, GameObject gameObject, int x, int y, string settings)
        {
            string[] keys = settings.Split(',');
            
            WindowBuilderSplit b = CreateSplit();

            //load buildings
            foreach (string key in keys)
            {
                Building build = Data.building[key];
                Dictionary<string, string> reqs = build.GenBuildReq();
                if (ReqHelper.CheckOnlyFinal(player, reqs, null, x, y))
                {
                    BuildSplitElement be = new BuildSplitElement(build, x, y);
                    be.disabled = ReqHelper.Desc(player, reqs, null, x, y);
                    b.AddElement(be);
                    //win.AddBuilding(build.id);
                }
                
            }
            
            b.Finish();

            
        }

        protected override void ButtonAction(Player player, string settings)
        {
            Debug.LogWarning("Not implemented");
        }
        
        class BuildSplitElement : BuildAction.BuildSplitElement
        {
            public BuildSplitElement(Building build, int x, int y) : base(build, x, y)
            {
            }

            public override void Perform()
            {
                BuildingMgmt.At(x,y).Kill();
                base.Perform();
            }
        }
    }

}