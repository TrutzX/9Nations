using Buildings;
using Game;
using InputActions;
using Towns;
using UI;
using UI.Show;
using UnityEngine;

namespace Players.Kingdoms
{
    public class CameraBuildingSplitElement : SplitElement
    {
        private WindowBuilderSplit b;
        protected Town town;
        public CameraBuildingSplitElement(WindowBuilderSplit b, Town town = null) : base("Buildings", "build")
        {
            this.b = b;
            this.town = town;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            foreach (BuildingInfo info in GameMgmt.Get().building.GetByPlayer(PlayerMgmt.ActPlayer().id))
            {
                if (town != null && town.id != info.data.townId)
                    continue;
                    
                panel.AddImageTextButton(info.name, info.baseData.Sprite(), () =>
                {
                    S.CameraMove().MoveTo(info.Pos());
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