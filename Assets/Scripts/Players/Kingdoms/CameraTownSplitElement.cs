using Buildings;
using Game;
using InputActions;
using MapElements.Buildings;
using Towns;
using UI;

namespace Players.Kingdoms
{
    public class CameraTownSplitElement : TownGeneralSplitElement
    {
        private WindowBuilderSplit b;
            
        public CameraTownSplitElement(WindowBuilderSplit b, Town town) : base(town)
        {
            this.b = b;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            base.ShowDetail(panel);
            panel.AddButton($"Show details for {town.name}", (() => town.ShowDetails()));

            panel.AddHeaderLabel("Buildings");
            //add buildings
            foreach (BuildingInfo info in GameMgmt.Get().building.GetByTown(town.id))
            {
                panel.AddImageTextButton(info.name, info.baseData.Sprite(), () =>
                {
                    S.CameraMove().MoveTo(info.Pos());
                    OnMapUI.Get().UpdatePanel(info.Pos());
                    b.CloseWindow();
                });
            }
        }
    }
}