using System.Linq;
using DataTypes;
using Players;
using Towns;

namespace DefaultNamespace
{
    public class DebugHelper
    {
        public static void DebugMenu()
        {
            //create it
            WindowPanelBuilder p = WindowPanelBuilder.Create("Debug Window");
            p.panel.AddButton("Give 500 gold", () =>
            {
                Town t = TownMgmt.Get().GetByActPlayer().First();
            
                t.AddRes("gold",500);
            });
            p.panel.AddButton("Trade", () =>
            {
                Town t = TownMgmt.Get().GetByActPlayer()[0];

                NLib.GetAction("trade").QuestRun(PlayerMgmt.ActPlayer(), null);
            });
            p.panel.AddButton("Give research", () =>
            {
                Town t = TownMgmt.Get().GetByActPlayer()[0];
            
                t.AddRes("research",100);
            });
            p.panel.AddButton("Finish all research", () =>
            {
                foreach (Research r in Data.research)
                {
                    PlayerMgmt.ActPlayer().research.Set(r.id, true);
                }
            });
            p.panel.AddButton("Unfinish all research", () =>
            {
                foreach (Research r in Data.research)
                {
                    PlayerMgmt.ActPlayer().research.Set(r.id, false);
                }
            });
            p.panel.AddButton("Switch Fog", () =>
            {
                PlayerMgmt.ActPlayer().fog.tilemap.gameObject.SetActive(!PlayerMgmt.ActPlayer().fog.tilemap.gameObject.activeSelf);
            });
            p.panel.AddButton("Player features", () =>
            {
                WindowPanelBuilder wp = WindowPanelBuilder.Create("features");
                foreach (FeaturePlayer fp in Data.featurePlayer)
                {
                    wp.panel.AddLabel(fp.name+": "+PlayerMgmt.ActPlayer().GetFeature(fp.id)+" ("+fp.standard+")");
                }
                wp.Finish();
            });
            p.panel.AddButton("Reset round", () =>
            {
                PlayerMgmt.Get().ResetRound();
            });
            p.Finish();
        }
    }
}