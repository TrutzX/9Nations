using System;
using System.Collections.Generic;
using System.Security.Principal;
using Buildings;
using Game;
using Libraries;
using Libraries.FActions;
using Libraries.FActions.General;
using Libraries.Res;
using MapElements;
using Players;
using Players.Infos;
using Tools;
using Towns;
using UI;
using Units;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Classes.Actions
{
    public class ActionChestOpen : BasePerformAction
    {
        public ActionChestOpen(string id) : base(id){}
        public ActionChestOpen() : base("chestOpen"){}

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            var unit = S.Unit().At(pos);
            var town = unit.Town();
            
            if (!CreateBonus(player, info, pos, town)) return;

            //get some wood
            if (town != null)
            {
                int a = Random.Range(2, 5);
                var i = L.b.res["wood"];
                player.info.Add(new Info(S.T("chestGenWood",a),i.Icon).CameraMove(pos));
                town.AddRes(i.id,a,ResType.Gift);
            }
            
            info.Kill();
            OnMapUI.Get().UpdatePanel(pos);
        }

        protected bool CreateBonus(Player player, MapElementInfo info, NVector pos, Town town)
        {
            //check it
            List<string> erg = new List<string>();

            if (player.GetFeature("research") == "true" && player.research.AvailableResearch().Count > 0)
            {
                erg.Add("research");
            }

            if (town != null)
            {
                erg.Add("res");
                erg.Add("item");
            }

            if (!player.fog.noFog)
            {
                erg.Add("fog");
            }

            if (erg.Count == 0)
            {
                player.info.Add(new Info(S.T("chestGenNo"), info.baseData.Icon).CameraMove(pos));
                return false;
            }

            switch (erg[Random.Range(0, erg.Count - 1)])
            {
                case "research":
                    GenResearch(player, pos);
                    break;
                case "res":
                    GenRes(town, player, pos);
                    break;
                case "item":
                    GenItem(town, player, pos);
                    break;
                case "fog":
                    GenFog(player, pos);
                    break;
            }

            return true;
        }

        private void GenResearch(Player p, NVector pos)
        {
            var r = p.research.AvailableResearch();
            var f = r[Random.Range(0, r.Count - 1)];
            p.research.Set(f.id,true);
            p.info.Add(new Info(S.T("chestGenResearch",f.Name()),f.Icon).CameraMove(pos));
        }

        private void GenItem(Town t, Player p, NVector pos)
        {
            var i = L.b.items.Random();
            t.AddRes(i.id, 1, ResType.Gift);
            p.info.Add(new Info(S.T("chestGenItem",i.Name()),i.Icon).CameraMove(pos));
        }

        private void GenRes(Town t, Player p, NVector pos)
        {
            Resource r;
            do
            {
               r = L.b.res.Random();
            } while (r.special || L.b.items.ContainsKey(r.id));
            int a = Random.Range(5, 15);
            t.AddRes(r.id, a, ResType.Gift);
            p.info.Add(new Info(S.T("chestGenRes",a, r.Name()),r.Icon).CameraMove(pos));
        }

        private void GenFog(Player p, NVector pos)
        {
            int a = Random.Range(5, 15);
            p.fog.Clear(pos, a);
            p.info.Add(new Info(S.T("chestGenField"),"view").CameraMove(pos));
        }
        
        protected override void Perform(ActionEvent evt, Player player, ActionHolder holder)
        {
            throw new System.NotImplementedException();
        }

        public override ActionHolder Create(string setting)
        {
            ActionHolder conf = base.Create(setting);
            conf.trigger = ActionEvent.Direct;
            return conf;
        }
    }
}