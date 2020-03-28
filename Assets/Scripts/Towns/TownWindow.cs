using Help;
using Libraries.Res;
using Players;
using Players.Infos;
using UI;

namespace Towns
{
    public class TownWindow
    {
        public static void Show(Town town)
        {
            WindowBuilderSplit wbs = WindowBuilderSplit.Create($"Details about {town.name}",null);
            wbs.AddElement(new TownGeneralSplitElement(town));
            if (Data.features.debug.Bool())
            {
                wbs.AddElement(new DebugTownSplitElement(town));
            }
            
            wbs.AddElement(new TownResSplitElement(town));
            wbs.AddElement(new KingdomOverview.CameraUnitSplitElement(wbs,town));
            wbs.AddElement(new KingdomOverview.CameraBuildingSplitElement(wbs,town));
            wbs.AddElement(new HelpSplitElement("town"));
            wbs.Finish();
        }
    }
}