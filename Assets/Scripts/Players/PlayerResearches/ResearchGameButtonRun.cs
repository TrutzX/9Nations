using Libraries;
using Players;
using Players.PlayerResearches;
using UI;

namespace Classes.GameButtons
{
    public class ResearchGameButtonRun : BaseGameButtonRun
    {
        public ResearchGameButtonRun() : base ("research") { }

        protected override void Run(Player player)
        {
            WindowBuilderSplit b = WindowBuilderSplit.Create("Research window",null);
            b.AddElement(new ResearchStatusSplitElement());
            b.AddElement(new ResearchFinishSplitElement());
            LSys.tem.helps.AddHelp("research", b);
            b.Finish();
        }
    }
}