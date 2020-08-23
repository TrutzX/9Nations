using System;
using System.Collections.Generic;
using System.Linq;
using Game;
using Libraries;
using Libraries.Buildings;
using Tools;
using Towns;
using UI;
using UnityEngine;

namespace Classes.Actions.Addons
{
    public class MaterialWindow
    {
        public static void ShowBuildMaterialWindow(BaseDataBuildingUnit build, NVector pos, Action<Dictionary<string, int>> perform)
        {
            string need = null;
            Town town = S.Towns().NearestTown(S.ActPlayer(), pos, false);
            
            //check if material need to replaced?
            foreach (var c in build.cost)
            {
                var res = L.b.res[c.Key];
                if (!string.IsNullOrEmpty(res.combine))
                {
                    need = res.combine;
                    break;
                }
            }
            
            //how much exist in this town?
            if (string.IsNullOrEmpty(need))
            {
                //nothing found? Build it directly
                perform.Invoke(build.cost);
                return;
            }
            
            //how much types is known?
            var found = L.b.res.GetAllByCategory(need).Where(r => town.KnowRes(r.id)).ToList();

            //found one? Build it directly
            if (found.Count == 1)
            {
                Dictionary<string, int> cost = new Dictionary<string, int>(build.cost);
                cost[found[0].id] = (cost.ContainsKey(found[0].id) ? cost[found[0].id] : 0)+cost[need];
                cost.Remove(need);
                perform.Invoke(cost);
                return;
            }
            
            WindowBuilderSplit wbs = WindowBuilderSplit.Create(S.T("constructionMaterial"),S.T("constructionMaterialUse"));

            foreach (var res in L.b.res.GetAllByCategory(need))
            {
                //have it?
                if (!town.KnowRes(res.id))
                {
                    continue;
                }
                
                Dictionary<string, int> cost = new Dictionary<string, int>(build.cost);
                cost[res.id] = (cost.ContainsKey(res.id) ? cost[res.id] : 0)+cost[need];
                cost.Remove(need);
                
                wbs.Add(new MaterialSplitElement(res, build, pos, cost, perform));
            }
            
            //add self
            wbs.Add(new MaterialSelfSplitElement(L.b.res[need], build, town, pos, new Dictionary<string, int>(build.cost), perform));
            LSys.tem.helps.AddHelp("material", wbs);
            wbs.Finish();
        }
    }
}