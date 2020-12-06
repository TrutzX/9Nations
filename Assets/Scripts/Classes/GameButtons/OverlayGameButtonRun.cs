using Game;
using Libraries;
using LoadSave;
using Players;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Classes.GameButtons
{
    public class OverlayGameButtonRun : BaseGameButtonRun
    {
        public OverlayGameButtonRun() : base ("overlay") { }

        protected override void Run(Player player)
        {
            
            WindowPanelBuilder wpb = WindowPanelBuilder.Create(Data().Name());
            foreach (var ol in L.b.overlays.Values())
            {
                wpb.panel.AddImageTextButton(ol.Name(), ol.Icon, () =>
                {
                    player.overlay.ViewOverlay(ol.id);
                    wpb.Close();
                });
            }
            
            wpb.panel.AddButton(S.T("reset"), () =>
            {
                player.overlay.ViewOverlay(null);
                wpb.Close();
            });
            wpb.Finish();
            
        }
    }
}