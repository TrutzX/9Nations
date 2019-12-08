using DataTypes;
using Help;
using Libraries;
using Players.Infos;
using Towns;
using UI;
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
            b.AddElement(new CameraUnitSplitElement(b));
            
            //add all towns
            foreach (Town t in TownMgmt.Get().GetByActPlayer())
            {
                b.AddElement(new CameraTownSplitElement(b, t));
            }

            b.AddElement(new InfosSplitElement());
            
            b.AddElement(new LexiconSplitElement(PlayerMgmt.ActPlayer().Nation()));
            
            b.Finish();
        }
        
        public class CameraUnitSplitElement : SplitElement
        {
            private WindowBuilderSplit b;
            public CameraUnitSplitElement(WindowBuilderSplit b) : base("Units", "logo")
            {
                this.b = b;
            }

            public override void ShowDetail(PanelBuilder panel)
            {
                foreach (UnitInfo info in UnitMgmt.Get().GetUnitPlayer(PlayerMgmt.ActPlayer().id))
                {

                    panel.AddImageTextButton(info.name, info.config.GetIcon(), () =>
                    {
                        CameraMove.Get().MoveTo(info.X(), info.Y());
                        OnMapUI.Get().UpdatePanelXY(info.X(), info.Y());
                        b.CloseWindow();
                    });
                }
            }

            public override void Perform()
            {
                Debug.LogWarning("Not implemented");
            }
        }
        
        public class CameraTownSplitElement : TownSplitElement
        {
            private WindowBuilderSplit b;
            
            public CameraTownSplitElement(WindowBuilderSplit b, Town town) : base(town)
            {
                this.b = b;
            }

            public override void ShowDetail(PanelBuilder panel)
            {
                base.ShowDetail(panel);

                panel.AddHeaderLabel("Buildings");
                //add buildings
                foreach (BuildingInfo info in BuildingMgmt.Get().GetByTown(town.id))
                {
                    panel.AddImageTextButton(info.name, info.config.GetIcon(), () =>
                    {
                        CameraMove.Get().MoveTo(info.X(), info.Y());
                        OnMapUI.Get().UpdatePanelXY(info.X(), info.Y());
                        b.CloseWindow();
                    });
                }
            }
        }
    }
    
}