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
    public class BuildAction : BaseAction
    {
        protected override void ButtonAction(Player player, GameObject gameObject, int x, int y, string settings)
        {
            string[] keys;
            if (!String.IsNullOrEmpty(settings))
            {
                keys = settings.Split(',');
            }
            else
            {
                keys = BuildingHelper.GetIDs();
            }
            
            //load buildings
            WindowBuilderSplit b = CreateSplit();

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

        public class BuildSplitElement : WindowBuilderSplit.SplitElement
        {
            protected Building build;
            protected int x, y;
            public BuildSplitElement(Building build, int x, int y) : base(build.name, build.GetIcon())
            {
                this.build = build;
                this.x = x;
                this.y = y;
            }

            public override void ShowDetail(PanelBuilder panel)
            {
                build.ShowInfo(panel, null, x, y);
            }

            public override void Perform()
            {
                BuildingMgmt.Get().Create(TownMgmt.Get().NearstTown(PlayerMgmt.ActPlayer(),x,y,false).id, build.id,x, y);
                OnMapUI.Get().UpdatePanelXY(x,y);
            }
        }
    }

}