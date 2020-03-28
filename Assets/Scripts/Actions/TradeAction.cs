using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Buildings;
using DataTypes;
using Game;
using Players;
using reqs;
using Tools;
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
        private string _sell;
        private string _buy;
        private Town _town;

        private Button shop1, shop10, shop100, shop1000;
        protected override void ButtonAction(Player player, MapElementInfo info, NVector pos, string settings)
        {
            _town = info.Town();
            BuildWindow(settings);
        }

        private void BuildWindow(string settings)
        {
            WindowPanelBuilder w = WindowPanelBuilder.Create(Data.nAction[id].desc);

            //build ress list
            List<string> values = new List<string>();
            List<string> titles = new List<string>();

            //sell
            foreach (Ress r in Data.ress)
            {
                if (settings!= null && settings.StartsWith("sell") && !settings.Contains(r.id))
                {
                    continue;
                }

                if (r.market > 0 && _town.GetRes(r.id) > 0)
                {
                    values.Add(r.id);
                    titles.Add($"{_town.GetRes(r.id)}x {r.name}");
                }
            }

            //found something?
            if (values.Count == 0)
            {
                w.panel.AddLabel("No Resources to sell found.");
                w.Finish();
                return;
            }

            _sell = values[0];
            w.panel.AddHeaderLabel("Sell resources");
            w.panel.AddDropdown(values.ToArray(), values[0], titles.ToArray(), s =>
            {
                _sell = s;
                UpdateButton();
            });

            values = new List<string>();
            titles = new List<string>();

            //buy
            foreach (Ress r in Data.ress)
            {
                if (settings!= null && settings.StartsWith("buy") && !settings.Contains(r.id))
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

            _buy = values[0];
            w.panel.AddHeaderLabel("Buy resources");
            w.panel.AddDropdown(values.ToArray(), values[0], titles.ToArray(), s =>
            {
                _buy = s;
                UpdateButton();
            });

            //add button
            shop1 = w.panel.AddButton("", () =>
            {
                Shop(1);
                w.Close();
            });

            //add button
            shop10 = w.panel.AddButton("", () =>
            {
                Shop(10);
                w.Close();
            });

            //add button
            shop100 = w.panel.AddButton("", () =>
            {
                Shop(100);
                w.Close();
            });

            //add button
            shop1000 = w.panel.AddButton("", () =>
            {
                Shop(1000);
                w.Close();
            });

            w.Finish();
            UpdateButton();
        }

        private void UpdateButton()
        {
            string bT = Data.ress[_buy].name;
            int bP = Data.ress[_buy].market;
            string sT = Data.ress[_sell].name;
            int sP = Data.ress[_sell].market;
            
            //1
            //Debug.LogWarning($"Kosten {sT}:{sP}");
            //Debug.LogWarning($"Erhalten {bT}:{bP}");
            int v = Convert.ToInt32(Math.Ceiling(1d * bP / sP));
            UIHelper.UpdateButtonText(shop1,$"{v}x {sT} for 1x {bT}");
            shop1.gameObject.SetActive(bT != sT && _town.GetRes(_sell) >= v);
            //Debug.LogWarning($"Diff {sT} {town.GetRes(sell)}>={v}");
            
            //10
            v = Convert.ToInt32(Math.Ceiling(10d * bP / sP));
            UIHelper.UpdateButtonText(shop10,$"{v}x {sT} for 10x {bT}");
            shop10.gameObject.SetActive(bT != sT && _town.GetRes(_sell) >= v);
            
            //100
            v = Convert.ToInt32(Math.Ceiling(100d * bP / sP));
            UIHelper.UpdateButtonText(shop100,$"{v}x {sT} for 100x {bT}");
            shop100.gameObject.SetActive(bT != sT && _town.GetRes(_sell) >= v);
            
            //1000
            v = Convert.ToInt32(Math.Ceiling(1000d * bP / sP));
            UIHelper.UpdateButtonText(shop1000,$"{v}x {sT} for 1000x {bT}");
            shop1000.gameObject.SetActive(bT != sT && _town.GetRes(_sell) >= v);
        }
        
        private void Shop(int count)
        {
            int b = Convert.ToInt32(Math.Ceiling(count * 1d * Data.ress[_buy].market / Data.ress[_buy].market));
            _town.AddRes(_sell,-b, ResType.Trade);
            _town.AddRes(_buy,count, ResType.Trade);
        }

        protected override void ButtonAction(Player player, string settings)
        {
            _town = S.Towns().GetByPlayer(player.id).First();
            BuildWindow(settings);
        }
    }

}