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
    public class ActionSendResWindow
    {
        private string _sell;
        private readonly Town _town;
        private readonly Town _dest;

        private Button shop1, shop10, shop100, shop1000;
        
        public ActionSendResWindow(Town town, Town dest, string sett)
        {
            _town = town;
            _dest = dest;
            BuildWindow(sett);
        }
        
        private void BuildWindow(string settings)
        {
            WindowPanelBuilder w = WindowPanelBuilder.Create(S.T("sendResTo",_dest.name));

            //build res list
            List<string> values = new List<string>();
            List<string> titles = new List<string>();

            //sell
            foreach (Resource r in L.b.res.Values())
            {
                if (settings != null && settings.StartsWith("sell") && !settings.Contains(r.id))
                {
                    continue;
                }

                if (r.price > 0 && _town.GetRes(r.id) > 0)
                {
                    values.Add(r.id);
                    titles.Add(r.Text(_town.GetRes(r.id)));
                }
            }
            
            //todo sort sell

            //found something?
            if (values.Count == 0)
            {
                w.panel.AddLabelT("tradeSellError");
                w.Finish();
                return;
            }

            _sell = values[0];
            w.panel.AddHeaderLabel(L.b.actions["sendRes"].Name());
            w.panel.AddDropdown(values.ToArray(), values[0], titles.ToArray(), s =>
            {
                _sell = s;
                UpdateButton();
            });

            var sound = L.b.actions["trade"].sound;
            
            //add button
            shop1 = w.panel.AddButton("", () =>
            {
                Shop(1);
                w.Close();
            }, sound);

            //add button
            shop10 = w.panel.AddButton("", () =>
            {
                Shop(10);
                w.Close();
            }, sound);

            //add button
            shop100 = w.panel.AddButton("", () =>
            {
                Shop(100);
                w.Close();
            }, sound);

            //add button
            shop1000 = w.panel.AddButton("", () =>
            {
                Shop(1000);
                w.Close();
            }, sound);

            w.Finish();
            UpdateButton();
        }

        private void UpdateButton()
        {
            var sT = L.b.res[_sell];
            
            //1
            //Debug.LogWarning($"Kosten {sT}:{sP}");
            //Debug.LogWarning($"Erhalten {bT}:{bP}");
            UIHelper.UpdateButtonText(shop1,S.T("sendResCount",sT.Text(1)));
            shop1.gameObject.SetActive(_town.GetRes(_sell) >= 1);
            //Debug.LogWarning($"Diff {sT} {town.GetRes(sell)}>={v}");
            
            //10
            UIHelper.UpdateButtonText(shop10,S.T("sendResCount",sT.Text(10)));
            shop10.gameObject.SetActive(_town.GetRes(_sell) >= 10);
            
            //100
            UIHelper.UpdateButtonText(shop100,S.T("sendResCount",sT.Text(100)));
            shop100.gameObject.SetActive(_town.GetRes(_sell) >= 100);
            
            //1000
            UIHelper.UpdateButtonText(shop1000,S.T("sendResCount",sT.Text(1000)));
            shop1000.gameObject.SetActive(_town.GetRes(_sell) >= 1000);
        }
        
        private void Shop(int count)
        {
            _town.AddRes(_sell,-count, ResType.Trade);
            _dest.AddRes(_sell,count, ResType.Trade);
        }
    }
}