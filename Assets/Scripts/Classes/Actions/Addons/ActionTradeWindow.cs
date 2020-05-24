using System;
using System.Collections.Generic;
using Game;
using Libraries;
using Libraries.Res;
using Towns;
using UI;
using UnityEngine.UI;

namespace Classes.Actions.Addons
{
    public class ActionTradeWindow
    {
        private string _sell;
        private string _buy;
        private readonly Town _town;

        private Button shop1, shop10, shop100, shop1000;
        
        public ActionTradeWindow(Town town, string sett)
        {
            _town = town;
        }
        
        private void BuildWindow(string settings)
        {
            WindowPanelBuilder w = WindowPanelBuilder.Create(L.b.actions["trade"].Name());

            //build ress list
            List<string> values = new List<string>();
            List<string> titles = new List<string>();

            //sell
            foreach (Resource r in L.b.res.Values())
            {
                if (settings!= null && settings.StartsWith("sell") && !settings.Contains(r.id))
                {
                    continue;
                }

                if (r.price > 0 && _town.GetRes(r.id) > 0)
                {
                    values.Add(r.id);
                    titles.Add(r.Text(_town.GetRes(r.id)));
                }
            }
 
            //found something?
            if (values.Count == 0)
            {
                w.panel.AddLabelT("tradeSellError");
                w.Finish();
                return;
            }

            _sell = values[0];
            w.panel.AddHeaderLabelT("tradeSell");
            w.panel.AddDropdown(values.ToArray(), values[0], titles.ToArray(), s =>
            {
                _sell = s;
                UpdateButton();
            });

            values = new List<string>();
            titles = new List<string>();

            //buy
            foreach (Resource r in L.b.res.Values())
            {
                if (settings!= null && settings.StartsWith("buy") && !settings.Contains(r.id))
                {
                    continue;
                }

                if (r.price > 0)
                {
                    values.Add(r.id);
                    titles.Add(r.Name());
                }
            }

            //found something?
            if (values.Count == 0)
            {
                w.panel.AddLabelT("tradeBuyError");
                w.Finish();
                return;
            }

            _buy = values[0];
            w.panel.AddHeaderLabelT("tradeBuy");
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
            var bT = L.b.res[_buy];
            float bP = L.b.res[_buy].price;
            var sT = L.b.res[_sell];
            float sP = L.b.res[_sell].price;
            
            //1
            //Debug.LogWarning($"Kosten {sT}:{sP}");
            //Debug.LogWarning($"Erhalten {bT}:{bP}");
            int v = Convert.ToInt32(Math.Ceiling(1d * bP / sP));
            UIHelper.UpdateButtonText(shop1,S.T("tradeAction",sT.Text(v),bT.Text(1)));
            shop1.gameObject.SetActive(_buy != _sell && _town.GetRes(_sell) >= v);
            //Debug.LogWarning($"Diff {sT} {town.GetRes(sell)}>={v}");
            
            //10
            v = Convert.ToInt32(Math.Ceiling(10d * bP / sP));
            UIHelper.UpdateButtonText(shop10,S.T("tradeAction",sT.Text(v),bT.Text(10)));
            shop10.gameObject.SetActive(_buy != _sell && _town.GetRes(_sell) >= v);
            
            //100
            v = Convert.ToInt32(Math.Ceiling(100d * bP / sP));
            UIHelper.UpdateButtonText(shop100,S.T("tradeAction",sT.Text(v),bT.Text(100)));
            shop100.gameObject.SetActive(_buy != _sell && _town.GetRes(_sell) >= v);
            
            //1000
            v = Convert.ToInt32(Math.Ceiling(1000d * bP / sP));
            UIHelper.UpdateButtonText(shop1000,S.T("tradeAction",sT.Text(v),bT.Text(1000)));
            shop1000.gameObject.SetActive(_buy != _sell && _town.GetRes(_sell) >= v);
        }
        
        private void Shop(int count)
        {
            int b = Convert.ToInt32(Math.Ceiling(count * 1d * L.b.res[_buy].price / L.b.res[_buy].price));
            _town.AddRes(_sell,-b, ResType.Trade);
            _town.AddRes(_buy,count, ResType.Trade);
        }
    }
}