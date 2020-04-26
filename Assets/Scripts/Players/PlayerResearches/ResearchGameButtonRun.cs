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
            b.Add(new ResearchStatusSplitElement());
            b.Add(new ResearchFinishSplitElement());
            LSys.tem.helps.AddHelp("research", b);
            b.Finish();
        }
    }
}