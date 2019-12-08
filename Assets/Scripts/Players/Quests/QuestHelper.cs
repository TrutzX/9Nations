using UI;
using UnityEngine;

namespace Players.Quests
{
    public static class QuestHelper
    {

        public static Quest Lose()
        {
            return new Quest("lose","Lose the game","base:destroy").AddAction("endGameLose","");
        }
        
        public static Quest Win()
        {
            return new Quest("win","Win the game","other:win").AddAction("endGameWin","");
        }

        public static void ShowQuestWindow()
        {
            //load buildings
            WindowBuilderSplit b = WindowBuilderSplit.Create("Quest window",null);

            foreach (Quest q in PlayerMgmt.ActPlayer().quests.quests)
            {
                if (q.IsFinish() || q.InProgress())
                    b.AddElement(new QuestSplitElement(q));
            }
            b.Finish();
        }
    }

    public class QuestSplitElement : SplitElement
    {
        protected Quest quest;
        public QuestSplitElement(Quest quest) : base(quest.name, SpriteHelper.Load(quest.icon))
        {
            this.quest = quest;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            quest.ShowInfo(panel);
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}