using Classes.GameButtons;
using Game;
using UI;

namespace Players.Quests
{
    public class QuestGameButtonRun : BaseGameButtonRun
    {
        public QuestGameButtonRun() : base ("quest") { }

        protected override void Run(Player player)
        {
            //load buildings
            WindowBuilderSplit b = WindowBuilderSplit.Create("Quest window",null);

            foreach (Quest q in S.ActPlayer().quests.quests)
            {
                if (q.IsFinish() || q.InProgress())
                    b.Add(new QuestSplitElement(q));
            }
            
            if (S.Debug())
                b.Add(new DebugQuestSplitElement());

            b.Finish();
        }
    }
}