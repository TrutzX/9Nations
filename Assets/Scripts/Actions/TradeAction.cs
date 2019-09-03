using System;
using System.Collections;
using System.Collections.Generic;
using DataTypes;
using Game;
using Players;
using reqs;
using Towns;
using UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Actions
{
    public class TradeAction : BaseAction
    {
        private string sell;
        private string buy;
        private Town town;

        private GameObject shop1, shop10, shop100, shop1000;
        protected override void ButtonAction(Player player, GameObject gameObject, int x, int y, string settings)
        {
            town = GameHelper.GetMapElement(gameObject).GetTown();
            
            WindowPanelBuilder w = WindowPanelBuilder.Create(Data.nAction[id].desc);

            //build ress list
            List<string> values = new List<string>();
            List<string> titles = new List<string>();

            //sell
            foreach (Ress r in Data.ress)
            {
                if (settings.StartsWith("sell") && !settings.Contains(r.id))
                {
                    continue;
                }
                
                if (r.market > 0 && town.GetRess(r.id) > 0)
                {
                    values.Add(r.id);
                    titles.Add($"{town.GetRess(r.id)}x {r.name}");
                }
            }
            
            //found something?
            if (values.Count == 0)
            {
                w.panel.AddLabel("No Resources to sell found.");
                w.Finish();
                return;
            }

            sell = values[0];
            w.panel.AddHeaderLabel("Sell resources");
            w.panel.AddDropdown(values.ToArray(), values[0], titles.ToArray(), s => { sell = s;UpdateButton();});

            values = new List<string>();
            titles = new List<string>();
            
            //buy
            foreach (Ress r in Data.ress)
            {
                if (settings.StartsWith("buy") && !settings.Contains(r.id))
                {
                    continue;
                }
                
                if (r.market > 0)
                {
                    values.Add(r.id);
                    titles.Add(r.name);
                }
            }
            
            //found something?
            if (values.Count == 0)
            {
                w.panel.AddLabel("No Resources to sell found.");
                w.Finish();
                return;
            }

            buy = values[0];
            w.panel.AddHeaderLabel("Buy resources");
            w.panel.AddDropdown(values.ToArray(), values[0], titles.ToArray(), s => { buy = s;
                UpdateButton();
            });
            
            //add button
            shop1 = w.panel.AddButton("",()=>
            {
                Shop(1);
                w.Close();
            });
            
            //add button
            shop10 = w.panel.AddButton("",()=>
            {
                Shop(10);
                w.Close();
            });
            
            //add button
            shop100 = w.panel.AddButton("",()=>
            {
                Shop(100);
                w.Close();
            });
            
            //add button
            shop1000 = w.panel.AddButton("",()=>
            {
                Shop(1000);
                w.Close();
            });
            
            w.Finish();
            UpdateButton();
        }

        private void UpdateButton()
        {
            string bT = Data.ress[buy].name;
            int bP = Data.ress[buy].market;
            string sT = Data.ress[sell].name;
            int sP = Data.ress[sell].market;
            
            //1
            //Debug.LogWarning($"Kosten {sT}:{sP}");
            //Debug.LogWarning($"Erhalten {bT}:{bP}");
            int v = Convert.ToInt32(Math.Ceiling(1d * bP / sP));
            UIHelper.UpdateButtonText(shop1,$"{v}x {sT} for 1x {bT}");
            shop1.SetActive(bT != sT && town.GetRess(sell) >= v);
            Debug.LogWarning($"Diff {sT} {town.GetRess(sell)}>={v}");
            
            //10
            v = Convert.ToInt32(Math.Ceiling(10d * bP / sP));
            UIHelper.UpdateButtonText(shop10,$"{v}x {sT} for 10x {bT}");
            shop10.SetActive(bT != sT && town.GetRess(sell) >= v);
            
            //100
            v = Convert.ToInt32(Math.Ceiling(100d * bP / sP));
            UIHelper.UpdateButtonText(shop100,$"{v}x {sT} for 100x {bT}");
            shop100.SetActive(bT != sT && town.GetRess(sell) >= v);
            
            //1000
            v = Convert.ToInt32(Math.Ceiling(1000d * bP / sP));
            UIHelper.UpdateButtonText(shop1000,$"{v}x {sT} for 1000x {bT}");
            shop1000.SetActive(bT != sT && town.GetRess(sell) >= v);
        }
        
        private void Shop(int count)
        {
            int b = Convert.ToInt32(Math.Ceiling(count * 1d * Data.ress[buy].market / Data.ress[buy].market));
            town.AddRess(sell,-b);
            town.AddRess(buy,count);
        }

        protected override void ButtonAction(Player player, string settings)
        {
            Debug.LogWarning("Not implemented");
        }
    }

}