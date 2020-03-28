using Buildings;
using DataTypes;
using Game;
using Help;
using Libraries;
using Players.Infos;
using Tools;
using Towns;
using UI;
using UI.Show;
using Units;
using UnityEngine;

namespace Players
{
    public class KingdomOverview
    {
        public static void ShowHelpWindow()
        {
            
            //load buildings
            WindowBuilderSplit b = WindowBuilderSplit.Create("Kingdom overview",null);
            b.AddElement(new PlayerInfoSplitElement(PlayerMgmt.ActPlayer()));
            b.AddElement(new CameraUnitSplitElement(b));
            
            //add all towns
            foreach (Town t in S.Towns().GetByActPlayer())
            {
                b.AddElement(new CameraTownSplitElement(b, t));
            }
            b.AddElement(new HelpSplitElement("town"));

            b.AddElement(new InfosSplitElement());
            b.AddElement(new LexiconSplitElement(PlayerMgmt.ActPlayer().Nation()));
            b.AddElement(new LexiconSplitElement(GameMgmt.Get().gameRound.GetRound()));
            
            
            b.Finish();
        }
        
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
                panel.AddButton($"Show details for {town.name}", (() => TownWindow.Show(town)));

                panel.AddHeaderLabel("Buildings");
                //add buildings
                foreach (BuildingInfo info in GameMgmt.Get().building.GetByTown(town.id))
                {
                    panel.AddImageTextButton(info.name, info.baseData.Sprite(), () =>
                    {
                        CameraMove.Get().MoveTo(info.Pos());
                        OnMapUI.Get().UpdatePanel(info.Pos());
                        b.CloseWindow();
                    });
                }
            }
        }
    }
    
}