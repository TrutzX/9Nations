using System.Linq;
using Classes;
using Classes.Actions;
using Game;
using Libraries;
using Libraries.FActions;
using Libraries.Researches;
using Maps.TileMaps;
using Players;
using Towns;
using UI;
using UnityEngine;

namespace Debugs
{
    public class DebugHelper
    {
        public static void DebugMenu()
        {
            //create it
            WindowPanelBuilder p = WindowPanelBuilder.Create("Debug Window");
            p.panel.AddButton("Give 500 gold", () =>
            {
                Town t = S.Towns().GetByActPlayer().First();
            
                t.AddRes("gold",500, ResType.Gift);
            });
            p.panel.AddButton("Trade", () =>
            {
                ActionHolder ah = new ActionHolder();
                ah.data["trade"] = null;
                LClass.s.GetNewAction("trade").PerformCheck(ActionEvent.Direct, S.ActPlayer(), ah);
            });
            p.panel.AddButton("Give research", () =>
            {
                Town t = S.Towns().GetByActPlayer()[0];
            
                t.AddRes("research",100, ResType.Gift);
            });
            p.panel.AddButton("Finish all research", () =>
            {
                foreach (Research r in L.b.researches.Values())
                {
                    S.ActPlayer().research.Set(r.id, true);
                }
            });
            p.panel.AddButton("Unfinish all research", () =>
            {
                foreach (Research r in L.b.researches.Values())
                {
                    S.ActPlayer().research.Set(r.id, false);
                }
            });
            p.panel.AddButton("Switch Fog", () =>
            {
                foreach (TileMapConfig16 t in S.ActPlayer().fog.tileMap)
                {
                    t.gameObject.SetActive(!t.gameObject.activeSelf);
                }
            });
            p.panel.AddButton("Show player features", () =>
            {
                WindowPanelBuilder wp = WindowPanelBuilder.Create("features");
                foreach (var fp in L.b.playerOptions.Values())
                {
                    wp.panel.AddLabel(fp.Name()+": "+S.ActPlayer().GetFeature(fp.id)+" ("+fp.standard+")");
                }
                wp.Finish();
            });
            p.panel.AddButton("Show game features", () =>
            {
                WindowPanelBuilder wp = WindowPanelBuilder.Create("features");
                foreach (var fp in L.b.gameOptions.Values())
                {
                    wp.panel.AddLabel(fp.Name()+": "+fp.Value()+" ("+fp.standard+")");
                }
                wp.Finish();
            });
            p.panel.AddButton("Reload game library", () =>
            {
                GameMgmt.Get().ReloadGameLib();
                p.Close();
            });
            p.panel.AddButton("Reset round", () =>
            {
                S.Players().ResetRound();
            });
            p.panel.AddLabel("DPI: "+Screen.dpi);
            p.Finish();
        }
    }
}