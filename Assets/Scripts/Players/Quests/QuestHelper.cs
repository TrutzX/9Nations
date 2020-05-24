using Game;
using Libraries;
using Libraries.FActions;
using Libraries.FActions.General;
using UI;

namespace Players.Quests
{
    public static class QuestHelper
    {

        public static Quest Lose()
        {
            return Action("endGameLose");
        }
        
        public static Quest Win()
        {
            return Action("endGameWin");
        }

        public static Quest Action(string id)
        {
            FDataAction da = L.b.actions[id];
            Quest q = new Quest(da.id, da.Name(), da.Icon).AddAction(id,"");
            return q;
        }

        public static void ShowQuestWindow()
        {
            //load buildings
            WindowBuilderSplit b = WindowBuilderSplit.Create("Quest window",null);

            foreach (Quest q in S.ActPlayer().quests.quests)
            {
                if (q.IsFinish() || q.InProgress())
                    b.Add(new QuestSplitElement(q));
            }
            b.Finish();
        }
    }
}