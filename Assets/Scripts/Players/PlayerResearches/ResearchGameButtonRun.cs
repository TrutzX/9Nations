using Classes.GameButtons;
using Libraries;
using Players.Infos;
using UI;

namespace Players.PlayerResearches
{
    public class ResearchGameButtonRun : BaseGameButtonRun
    {
        public ResearchGameButtonRun() : base ("research") { }

        protected override void Run(Player player)
        {
            WindowBuilderSplit b = WindowBuilderSplit.Create("Research window",null);
            b.Add(new ResearchStatusSplitElement());
            b.Add(new ResearchFinishSplitElement());
            b.Add(new InfosSplitElement(player.research.info));
            LSys.tem.helps.AddHelp("research", b);
            b.Finish();
        }
    }
}