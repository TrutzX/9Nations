using Game;
using InputActions;
using Towns;
using UI;
using UI.Show;
using Units;
using UnityEngine;

namespace Players.Kingdoms
{
    public class CameraUnitSplitElement : SplitElement
    {
        private WindowBuilderSplit b;
        protected Town town;
        public CameraUnitSplitElement(WindowBuilderSplit b, Town town = null) : base("Units", "train")
        {
            this.b = b;
            this.town = town;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            foreach (UnitInfo info in GameMgmt.Get().unit.GetByPlayer(PlayerMgmt.ActPlayer().id))
            {
                if (town != null && town.id != info.data.townId)
                    continue;
                    
                panel.AddImageTextButton(info.name, info.baseData.Sprite(), () =>
                {
                    CameraMove.Get().MoveTo(info.Pos());
                    OnMapUI.Get().UpdatePanel(info.Pos());
                    b.CloseWindow();
                    info.ShowInfoWindow();
                });
            }
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}