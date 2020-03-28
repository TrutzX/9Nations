using Help;
using UI;

namespace Players.PlayerResearches
{
    public static class ResearchWindow
    {

        public static void ShowResearchWindow()
        {
            //load buildings
            WindowBuilderSplit b = WindowBuilderSplit.Create("Research window",null);

            
            b.AddElement(new ResearchStatusSplitElement());
            b.AddElement(new ResearchFinishSplitElement());
            b.AddElement(new HelpSplitElement("research"));
            b.Finish();
        }
    }
}